namespace File_Integrity_Utility.ProgramFiles.MenuOptions
{
    class MenuOption4
    {
        public static void RenameAllTopLevelFilesInGivenFolderAsTheirHash()
        {
            string pathOfGivenFolder = ConsoleTools.ObtainFolderPathFromUser();
            string[] allTopLevelFilePathsInGivenFolder = Directory.GetFiles(pathOfGivenFolder);
            foreach (string currentFilePath in allTopLevelFilePathsInGivenFolder)
            {
                RenameGivenFileAsItsHash(currentFilePath);
            }
        }


        private static void RenameGivenFileAsItsHash(string currentFilePath)
        {
            // We know currentFilePath will not be null, so we suppress the warning below:
            #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string currentFileFolderPath = Path.GetDirectoryName(currentFilePath);
            #pragma warning restore CS8600
            string currentFileHash = HashingTools.ObtainFileHash(currentFilePath);
            string currentFileExtension = Path.GetExtension(currentFilePath);
            string currentFileNewPath = currentFileFolderPath + Path.DirectorySeparatorChar + currentFileHash + currentFileExtension;
            // Because we are not changing the destination folder, the current file will not be physically moved on the disk that it is on. Instead, it will simply be renamed:
            File.Move(currentFilePath, currentFileNewPath);
        }
    }
}