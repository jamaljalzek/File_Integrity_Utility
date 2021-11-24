namespace FileIntegrityUtility.ProgramFiles.MenuOptions
{
    class MenuOption4
    {
        public static void GenerateTextFileListingFileNamesFollowedByTheirHashes()
        {
            string fullPathOfFolder = ObtainFullFolderPathFromUser();
            string fullPathOfTextFileToBeCreated = fullPathOfFolder + "\\File SHA 256 Hashes.txt";
            // We do not need to hash any already existing "File SHA 256 Hashes.txt" file, which we will already be replacing:
            File.Delete(fullPathOfTextFileToBeCreated);
            // The format of the below list is that of [file1Name, file1Hash, file2Name, file2Hash, etc.]:
            List<string> fileNamesFollowedByTheirHashes = HashingTools.GetListOfFileNamesFollowedByTheirHashes(fullPathOfFolder, SearchOption.TopDirectoryOnly);
            WriteListOfFileNamesFollowedByTheirHashesToNewTextFile(fileNamesFollowedByTheirHashes, fullPathOfTextFileToBeCreated);
        }


        private static string ObtainFullFolderPathFromUser()
        {
            ConsoleTools.SetConsoleEncoding();
            string userInput = ConsoleTools.PromptForUserInput("Please enter the full path of the folder to analyze: ");
            while (!Directory.Exists(userInput))
            {
                Console.WriteLine("ERROR, no folder found with the provided path.");
                userInput = ConsoleTools.PromptForUserInput("Please enter the full path of the folder to analyze: ");
            }
            return userInput;
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
            Console.WriteLine("All file hashes written to file.");
        }


        private static void AddCurrentFileNameAndItsHashToTextFile(List<string> fileNamesFollowedByTheirHashes, int currentIndex, StreamWriter textFileStreamWriter)
        {
            string currentFileName = fileNamesFollowedByTheirHashes[currentIndex];
            textFileStreamWriter.WriteLine(currentFileName);
            string currentFileHash = fileNamesFollowedByTheirHashes[currentIndex + 1];
            textFileStreamWriter.Write(currentFileHash);
        }
    }
}