namespace File_Integrity_Utility.ProgramFiles.MenuOptions
{
    class MenuOption6
    {
        public static void CompareHashesOfFilesInFolderToRecordedHashesInTextFile()
        {
            string pathOfFolder = ConsoleTools.ObtainFolderPathFromUser();
            string pathOfTextFile = pathOfFolder + "\\File SHA 256 Hashes.txt";
            if (!File.Exists(pathOfTextFile))
            {
                ConsoleTools.WriteLineToConsoleInColor("\n" + "Error: 'File SHA 256 Hashes.txt' was not found in the given folder.", ConsoleColor.Red);
                return;
            }
            Dictionary<string, string> fileNamesToRecordedHashes = GetMapOfFileNamesToRecordedHashes(pathOfTextFile);
            List<string[]> filePathsToRecentHashes = HashingTools.GetListOfFilePathsToHashes(pathOfFolder, SearchOption.TopDirectoryOnly);
            // There is no need to consider the .txt file containing hashes itself, so we remove its path and hash from our list:
            RemoveTextFilePathAndHashFromList(pathOfTextFile, filePathsToRecentHashes);
            bool wasInconsistencyFound = FileHashComparator.CompareRecentHashesToRecordedHashesForAllFiles(filePathsToRecentHashes, fileNamesToRecordedHashes);
            DisplayIfAllFilesAreConsistentOrNot(wasInconsistencyFound);
        }


        private static Dictionary<string, string> GetMapOfFileNamesToRecordedHashes(string pathOfTextFile)
        {
            // Given the text file containing file names to hashes, a few issues can occur:
            //     a. The groupings of file names and their hashes may be in any arbitrary order, rather than alphabetical.
            //     One solution is to first read this list, and then sort it.
            //     Keep in mind that Merge Sort takes O(n * log(n)) time and O(n) space.
            //     b. A (newer) file that is present in the given folder may not even be recorded in the text file.
            //     How do we efficiently detect this?
            // Using a Hash Map, which has O(1) insertion and removal time as well as O(n) space seems to work pretty well here.
            Dictionary<string, string> fileNamesToRecordedHashes = new Dictionary<string, string>();
            StreamReader textFileReader = File.OpenText(pathOfTextFile);
            while (!textFileReader.EndOfStream)
            {
                string? currentFileName = textFileReader.ReadLine();
                string? currentFileHash = textFileReader.ReadLine();
                if (currentFileName != null && currentFileHash != null)
                {
                    fileNamesToRecordedHashes[currentFileName] = currentFileHash;
                }
                // Each [file name, file hash] entry in the text file is spaced out with an empty line, so we must skip it:
                textFileReader.ReadLine();
            }
            textFileReader.Close();
            return fileNamesToRecordedHashes;
        }


        private static void RemoveTextFilePathAndHashFromList(string pathOfTextFile, List<string[]> filePathsToRecentHashes)
        {
            for (int currentIndex = 0; currentIndex < filePathsToRecentHashes.Count; ++currentIndex)
            {
                string[] currentFilePathAndHash = filePathsToRecentHashes[currentIndex];
                string currentFilePath = currentFilePathAndHash[0];
                if (currentFilePath.Equals(pathOfTextFile))
                {
                    filePathsToRecentHashes.RemoveAt(currentIndex);
                    return;
                }
            }
        }


        private static void DisplayIfAllFilesAreConsistentOrNot(bool wasAnyInconsistencyFound)
        {
            Console.WriteLine('\n' + "Verdict:");
            if (wasAnyInconsistencyFound)
            {
                ConsoleTools.WriteLineToConsoleInColor("Inconsistency(s) found", ConsoleColor.Red);
            }
            else
            {
                ConsoleTools.WriteLineToConsoleInColor("No inconsistency(s) found", ConsoleColor.Green);
            }
        }
    }


    class FileHashComparator
    {
        public static bool CompareRecentHashesToRecordedHashesForAllFiles(List<string[]> filePathsToRecentHashes, Dictionary<string, string> fileNamesToRecordedHashes)
        {
            Console.WriteLine("\n" + "Comparing file names and hashes with those in text file...");
            bool wasInconsistencyFound = false;
            foreach (string[] currentFilePathAndHash in filePathsToRecentHashes)
            {
                bool isCurrentFileConsistent = DoesFileRecentHashMatchAnyRecordedHash(currentFilePathAndHash, fileNamesToRecordedHashes);
                if (!isCurrentFileConsistent)
                {
                    wasInconsistencyFound = true;
                }
            }
            Console.WriteLine("Comparing complete.");
            return wasInconsistencyFound;
        }


        private static bool DoesFileRecentHashMatchAnyRecordedHash(string[] filePathAndHash, Dictionary<string, string> fileNamesToRecordedHashes)
        {
            string filePath = filePathAndHash[0];
            string fileName = Path.GetFileName(filePath);
            if (!DoesRecordedHashExistForFile(fileName, fileNamesToRecordedHashes))
            {
                return false;
            }
            string fileRecentHash = filePathAndHash[1];
            return DoesFileRecentHashMatchRecordedHash(fileName, fileRecentHash, fileNamesToRecordedHashes);
        }


        private static bool DoesRecordedHashExistForFile(string fileName, Dictionary<string, string> fileNamesToRecordedHashes)
        {
            if (!fileNamesToRecordedHashes.ContainsKey(fileName))
            {
                ConsoleTools.WriteLineToConsoleInColor("Error: " + fileName + " was not found in File SHA 256 Hashes.txt.", ConsoleColor.Red);
                return false;
            }
            return true;
        }


        private static bool DoesFileRecentHashMatchRecordedHash(string fileName, string fileRecentHash, Dictionary<string, string> fileNamesToRecordedHashes)
        {
            string fileRecordedHash = fileNamesToRecordedHashes[fileName];
            if (!fileRecentHash.Equals(fileRecordedHash))
            {
                ConsoleTools.WriteLineToConsoleInColor("Error: hash for " + fileName + " does NOT match associated hash in File SHA 256 Hashes.txt:", ConsoleColor.Red);
                ConsoleTools.WriteLineToConsoleInColor("Recent file hash: " + fileRecentHash, ConsoleColor.Red);
                ConsoleTools.WriteLineToConsoleInColor("File SHA 256 Hashes.txt hash: " + fileRecordedHash, ConsoleColor.Red);
                return false;
            }
            return true;
        }
    }
}