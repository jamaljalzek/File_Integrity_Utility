namespace File_Integrity_Utility.ProgramFiles.MenuOptions
{
    class MenuOption4
    {
        public static void RenameAllTopLevelFilesInGivenFolderAsTheirHash()
        {
            string pathOfFolder = ConsoleTools.ObtainFolderPathFromUser();
            Console.WriteLine('\n' + "Renaming all top level files in " + pathOfFolder + "...");
            string[] allTopLevelFilePathsInFolder = Directory.GetFiles(pathOfFolder);
            foreach (string currentFilePath in allTopLevelFilePathsInFolder)
            {
                RenameFileAsItsHash(currentFilePath);
            }
            ConsoleTools.WriteLineToConsoleInColor('\n' + "Renaming files complete.", ConsoleColor.Cyan);
        }


        private static void RenameFileAsItsHash(string filePath)
        {
            string fileNewNameWithExtension = ObtainNewFileName(filePath);
            // We know filePath will not be null, but specify "string?" to suppress warnings:
            string? fileFolderPath = Path.GetDirectoryName(filePath);
            string fileNewPath = fileFolderPath + Path.DirectorySeparatorChar + fileNewNameWithExtension;
            // Because we are not changing the destination folder, the current file will not be physically moved on the disk that it is on.
            // Instead, it will simply be renamed:
            File.Move(filePath, fileNewPath);
            Console.WriteLine(fileNewNameWithExtension);
        }


        private static string ObtainNewFileName(string filePath)
        {
            string fileOriginalNameWithExtension = Path.GetFileName(filePath);
            Console.Write(fileOriginalNameWithExtension + " -> ");
            string fileHash = HashingTools.ObtainFileHash(filePath);
            string fileExtension = Path.GetExtension(filePath);
            return fileHash + fileExtension;
        }
    }
}