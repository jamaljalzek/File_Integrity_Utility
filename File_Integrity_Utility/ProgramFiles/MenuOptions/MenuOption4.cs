namespace File_Integrity_Utility.ProgramFiles.MenuOptions
{
    class MenuOption4
    {
        public static void RenameAllTopLevelFilesInGivenFolderAsTheirHash()
        {
            string pathOfGivenFolder = ConsoleTools.ObtainFolderPathFromUser();
            Console.WriteLine('\n' + "Renaming all top level files in " + pathOfGivenFolder + "...");
            string[] allTopLevelFilePathsInGivenFolder = Directory.GetFiles(pathOfGivenFolder);
            foreach (string currentFilePath in allTopLevelFilePathsInGivenFolder)
            {
                RenameGivenFileAsItsHash(currentFilePath);
            }
            ConsoleTools.WriteLineToConsoleInGivenColor('\n' + "Renaming files complete.", ConsoleColor.Cyan);
        }


        private static void RenameGivenFileAsItsHash(string currentFilePath)
        {
            string currentFileOriginalNameWithExtension = Path.GetFileName(currentFilePath);
            Console.Write(currentFileOriginalNameWithExtension + " -> ");
            // We know currentFilePath will not be null, so we suppress the warning below:
            #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string currentFileFolderPath = Path.GetDirectoryName(currentFilePath);
            #pragma warning restore CS8600
            string currentFileHash = HashingTools.ObtainFileHash(currentFilePath);
            string currentFileExtension = Path.GetExtension(currentFilePath);
            string currentFileNewNameWithExtension = currentFileHash + currentFileExtension;
            string currentFileNewPath = currentFileFolderPath + Path.DirectorySeparatorChar + currentFileNewNameWithExtension;
            // Because we are not changing the destination folder, the current file will not be physically moved on the disk that it is on. Instead, it will simply be renamed:
            File.Move(currentFilePath, currentFileNewPath);
            Console.WriteLine(currentFileNewNameWithExtension);
        }
    }
}