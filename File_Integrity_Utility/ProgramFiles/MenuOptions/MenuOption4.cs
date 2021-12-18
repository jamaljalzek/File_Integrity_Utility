namespace File_Integrity_Utility.ProgramFiles.MenuOptions
{
    class MenuOption4
    {
        public static void GenerateTextFileListingFileNamesFollowedByTheirHashes()
        {
            string fullPathOfFolder = ConsoleTools.ObtainFullFolderPathFromUser();
            string fullPathOfTextFileToBeCreated = fullPathOfFolder + "\\File SHA 256 Hashes.txt";
            // We do not need to hash any already existing "File SHA 256 Hashes.txt" file, which we will already be replacing:
            File.Delete(fullPathOfTextFileToBeCreated);
            // The format of the below list is that of [file1Name, file1Hash, file2Name, file2Hash, etc.]:
            List<string> fileNamesFollowedByTheirHashes = HashingTools.GetListOfFileFullPathsFollowedByTheirHashes(fullPathOfFolder, SearchOption.TopDirectoryOnly);
            WriteListOfFileNamesFollowedByTheirHashesToNewTextFile(fileNamesFollowedByTheirHashes, fullPathOfTextFileToBeCreated);
        }


        private static void WriteListOfFileNamesFollowedByTheirHashesToNewTextFile(List<string> fileNamesFollowedByTheirHashes, string fullPathOfTextFile)
        {
            Console.WriteLine();
            Console.WriteLine("Writing file hashes to file...");
            StreamWriter textFileStreamWriter = File.CreateText(fullPathOfTextFile);
            int indexOfLastFileName = fileNamesFollowedByTheirHashes.Count - 2;
            for (int currentIndex = 0; currentIndex < indexOfLastFileName; currentIndex += 2)
            {
                AddCurrentFileNameAndItsHashToTextFile(fileNamesFollowedByTheirHashes, currentIndex, textFileStreamWriter);
                textFileStreamWriter.Write("\n\n");
            }
            AddCurrentFileNameAndItsHashToTextFile(fileNamesFollowedByTheirHashes, indexOfLastFileName, textFileStreamWriter);
            textFileStreamWriter.Close();
            ConsoleTools.WriteLineToConsoleInGivenColor("All file hashes written to file.", ConsoleColor.Cyan);
        }


        private static void AddCurrentFileNameAndItsHashToTextFile(List<string> fileNamesFollowedByTheirHashes, int currentIndex, StreamWriter textFileStreamWriter)
        {
            string currentFileFullPath = fileNamesFollowedByTheirHashes[currentIndex];
            string currentFileName = Path.GetFileName(currentFileFullPath);
            textFileStreamWriter.WriteLine(currentFileName);
            string currentFileHash = fileNamesFollowedByTheirHashes[currentIndex + 1];
            textFileStreamWriter.Write(currentFileHash);
        }
    }
}