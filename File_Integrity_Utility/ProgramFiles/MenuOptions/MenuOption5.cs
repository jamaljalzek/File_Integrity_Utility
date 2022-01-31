namespace File_Integrity_Utility.ProgramFiles.MenuOptions
{
    class MenuOption5
    {
        public static void GenerateTextFileListingFileNamesToHashes()
        {
            string pathOfFolder = ConsoleTools.ObtainFolderPathFromUser();
            List<string[]> listOfFilePathsToHashes = HashingTools.GetListOfFilePathsToHashes(pathOfFolder, SearchOption.TopDirectoryOnly);
            string pathOfTextFileToBeCreated = pathOfFolder + Path.DirectorySeparatorChar + "File SHA 256 Hashes.txt";
            // We will be replacing any already existing hashes text file with an up to date one.
            // Also, we must delete it now (if any) to prevent unnecessarily hashing it:
            File.Delete(pathOfTextFileToBeCreated);
            StreamWriter textFileToWriteTo = File.CreateText(pathOfTextFileToBeCreated);
            WriteListOfFileNamesToHashesToTextFile(listOfFilePathsToHashes, textFileToWriteTo);
            textFileToWriteTo.Close();
        }


        private static void WriteListOfFileNamesToHashesToTextFile(List<string[]> listOfFilePathsToHashes, StreamWriter textFileWriter)
        {
            Console.WriteLine("\n" + "Writing file hashes to text file...");
            int lastIndex = listOfFilePathsToHashes.Count - 1;
            for (int currentIndex = 0; currentIndex < lastIndex; ++currentIndex)
            {
                string[] currentFilePathAndHash = listOfFilePathsToHashes[currentIndex];
                AddFileNameAndHashToTextFile(currentFilePathAndHash, textFileWriter);
                textFileWriter.Write("\n\n");
            }
            string[] lastFilePathAndHash = listOfFilePathsToHashes[lastIndex];
            AddFileNameAndHashToTextFile(lastFilePathAndHash, textFileWriter);
            ConsoleTools.WriteLineToConsoleInColor("All file hashes written to text file.", ConsoleColor.Cyan);
        }


        private static void AddFileNameAndHashToTextFile(string[] filePathAndHash, StreamWriter textFileToWriteTo)
        {
            string filePath = filePathAndHash[0];
            string fileName = Path.GetFileName(filePath);
            textFileToWriteTo.WriteLine(fileName);
            string fileHash = filePathAndHash[1];
            textFileToWriteTo.Write(fileHash);
        }
    }
}