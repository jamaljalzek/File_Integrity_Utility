namespace File_Integrity_Utility.ProgramFiles.MenuOptions
{
    class MenuOption6
    {
        public static void CompareHashesOfFilesInGivenFolderToRecordedHashesInTextFile()
        {
            string pathOfFolder = ConsoleTools.ObtainFolderPathFromUser();
            string pathOfTextFileContainingFileNamesAndHashes = pathOfFolder + "\\File SHA 256 Hashes.txt";
            if (!File.Exists(pathOfTextFileContainingFileNamesAndHashes))
            {
                ConsoleTools.WriteLineToConsoleInGivenColor("\n" + "ERROR: 'File SHA 256 Hashes.txt' was not found in the given folder.", ConsoleColor.Red);
                return;
            }
            List<string[]> listOfFilePathsToHashes = HashingTools.GetListOfFilePathsToHashes(pathOfFolder, SearchOption.TopDirectoryOnly);
            // There is no need to consider the .txt file containing hashes itself, so we remove its full path and hash from our list:
            RemoveSha256HashesTextFileNameAndHashFromList(pathOfTextFileContainingFileNamesAndHashes, listOfFilePathsToHashes);
            Dictionary<string, string> recordedFileNamesToHashes = GetMapOfRecordedFilePathsToHashes(pathOfTextFileContainingFileNamesAndHashes);
            CompareListOfFileNamesAndHashesToRecordedFileNamesAndHashes(listOfFilePathsToHashes, recordedFileNamesToHashes);
        }


        private static void RemoveSha256HashesTextFileNameAndHashFromList(string pathOfTextFileContainingFileNamesAndTheirHashes, List<string[]> listOfFilePathsToHashes)
        {
            for (int currentIndex = 0; currentIndex < listOfFilePathsToHashes.Count; ++currentIndex)
            {
                string[] currentFilePathAndItsHash = listOfFilePathsToHashes[currentIndex];
                string currentFilePath = currentFilePathAndItsHash[0];
                if (currentFilePath.Equals(pathOfTextFileContainingFileNamesAndTheirHashes))
                {
                    listOfFilePathsToHashes.RemoveAt(currentIndex);
                    return;
                }
            }
        }


        private static Dictionary<string, string> GetMapOfRecordedFilePathsToHashes(string fullPathOfTextFileContainingFileNamesAndTheirHashes)
        {
            // Given the "File SHA 256 Hashes.txt", a few issues can occur:
            //     a. The groupings of file names and their hashes may be in any arbitrary order (rather than say, alphabetical).
            //     This happens if the user manually adds entries to the file, rather than relying on this program.
            //     One solution is to first read this list, and then sort it.
            //     Keep in mind that Merge Sort takes O(n * log(n)) time and O(n) space.
            //     b. A file that we recently hashed (which is present in the given folder) may not even be recorded in the above .txt file.
            //     How do we efficiently detect this?
            // Thus, using a Hash Map, which has O(1) insertion and removal time as well as O(n) space seems to work pretty well here.
            Dictionary<string, string> recordedFileNamesToHashes = new Dictionary<string, string>();
            StreamReader textFileStreamReader = File.OpenText(fullPathOfTextFileContainingFileNamesAndTheirHashes);
            while (!textFileStreamReader.EndOfStream)
            {
                string currentFileNameRecordedInTextFile = textFileStreamReader.ReadLine();
                string currentFileHashRecordedInTextFile = textFileStreamReader.ReadLine();
                recordedFileNamesToHashes[currentFileNameRecordedInTextFile] = currentFileHashRecordedInTextFile;
                // Each (file name, file hash) entry in the .txt file is spaced out with a new line, so we must skip it:
                textFileStreamReader.ReadLine();
            }
            textFileStreamReader.Close();
            return recordedFileNamesToHashes;
        }


        private static void CompareListOfFileNamesAndHashesToRecordedFileNamesAndHashes(List<string[]> listOfFilePathsToHashes, Dictionary<string, string> recordedFileNamesToHashes)
        {
            Console.WriteLine("\n" + "Comparing file names and hashes with those in text file...");
            foreach (string[] currentFilePathAndItsHash in listOfFilePathsToHashes)
            {
                CompareCurrentFileNameAndHashToRecordedFileNameAndHash(currentFilePathAndItsHash, recordedFileNamesToHashes);
            }
            ConsoleTools.WriteLineToConsoleInGivenColor("Comparing complete.", ConsoleColor.Cyan);
        }


        private static void CompareCurrentFileNameAndHashToRecordedFileNameAndHash(string[] currentFilePathAndHash, Dictionary<string, string> recordedFileNamesToHashes)
        {
            string currentFilePath = currentFilePathAndHash[0];
            string currentFileName = Path.GetFileName(currentFilePath);
            if (!recordedFileNamesToHashes.ContainsKey(currentFileName))
            {
                ConsoleTools.WriteLineToConsoleInGivenColor("ERROR: " + currentFileName + " was not found in File SHA 256 Hashes.txt.", ConsoleColor.Red);
                return;
            }
            string currentFileRecentHash = currentFilePathAndHash[1];
            string currentRecordedFileHash = recordedFileNamesToHashes[currentFileName];
            if (!currentFileRecentHash.Equals(currentRecordedFileHash))
            {
                ConsoleTools.WriteLineToConsoleInGivenColor("ERROR: hash for " + currentFileName + " does NOT match associated hash in File SHA 256 Hashes.txt:", ConsoleColor.Red);
                ConsoleTools.WriteLineToConsoleInGivenColor("Recent file hash: " + currentFileRecentHash, ConsoleColor.Red);
                ConsoleTools.WriteLineToConsoleInGivenColor("File SHA 256 Hashes.txt hash: " + currentRecordedFileHash, ConsoleColor.Red);
            }
        }
    }
}