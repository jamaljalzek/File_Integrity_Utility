namespace File_Integrity_Utility.ProgramFiles.MenuOptions
{
    class MenuOption1
    {
        public static void DisplayFileNameFollowedByItsHash()
        {
            string pathOfFile = ConsoleTools.ObtainFilePathFromUser();
            Console.WriteLine("\n" + "Generating hash for given file...");
            string fileHashString = HashingTools.ObtainFileHash(pathOfFile);
            Console.WriteLine("File hash complete." + "\n");
            string currentFileNameWithExtension = Path.GetFileName(pathOfFile);
            ConsoleTools.WriteLineToConsoleInGivenColor(currentFileNameWithExtension, ConsoleColor.Cyan);
            ConsoleTools.WriteLineToConsoleInGivenColor(fileHashString, ConsoleColor.Cyan);
        }
    }
}