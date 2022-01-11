namespace File_Integrity_Utility.ProgramFiles.MenuOptions
{
    class MenuOption5
    {
        public static void GenerateTextFileListingFileNamesToHashes()
        {
            string pathOfFolder = ConsoleTools.ObtainFolderPathFromUser();
            string pathOfTextFileToBeCreated = pathOfFolder + "\\File SHA 256 Hashes.txt";
            // We do not need to hash any already existing "File SHA 256 Hashes.txt" file, which we will already be replacing:
            File.Delete(pathOfTextFileToBeCreated);
            List<string[]> listOfFilePathsToHashes = HashingTools.GetListOfFilePathsToHashes(pathOfFolder, SearchOption.TopDirectoryOnly);
            WriteListOfFileNamesToHashesToNewTextFile(listOfFilePathsToHashes, pathOfTextFileToBeCreated);
        }


        private static void WriteListOfFileNamesToHashesToNewTextFile(List<string[]> listOfFilePathsToHashes, string pathOfTextFile)
        {
            Console.WriteLine("\n" + "Writing file hashes to file...");
            StreamWriter textFileStreamWriter = File.CreateText(pathOfTextFile);
            int lastIndex = listOfFilePathsToHashes.Count - 1;
            for (int currentIndex = 0; currentIndex < lastIndex; ++currentIndex)
            {
                string[] currentFilePathAndFileHash = listOfFilePathsToHashes[currentIndex];
                AddCurrentFileNameAndItsFileHashToTextFile(currentFilePathAndFileHash, textFileStreamWriter);
                textFileStreamWriter.Write("\n\n");
            }
            string[] lastFilesPathAndHash = listOfFilePathsToHashes[lastIndex];
            AddCurrentFileNameAndItsFileHashToTextFile(lastFilesPathAndHash, textFileStreamWriter);
            textFileStreamWriter.Close();
            ConsoleTools.WriteLineToConsoleInGivenColor("All file hashes written to file.", ConsoleColor.Cyan);
        }


        private static void AddCurrentFileNameAndItsFileHashToTextFile(string[] currentFilePathAndFileHash, StreamWriter textFileStreamWriter)
        {
            string currentFilePath = currentFilePathAndFileHash[0];
            string currentFileName = Path.GetFileName(currentFilePath);
            textFileStreamWriter.WriteLine(currentFileName);
            string currentFileHash = currentFilePathAndFileHash[1];
            textFileStreamWriter.Write(currentFileHash);
        }
    }
}