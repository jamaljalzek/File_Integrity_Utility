namespace File_Integrity_Utility.ProgramFiles.MenuOptions
{
    public class MenuOption1
    {
        public static void DisplayFileNameFollowedByItsHash()
        {
            string pathOfFile = ConsoleTools.ObtainFilePathFromUser();
            Console.WriteLine("\n" + "Generating hash for given file...");
            string fileHashString = HashingTools.ObtainFileHash(pathOfFile);
            Console.WriteLine("File hash complete." + "\n");
            string fileNameWithExtension = Path.GetFileName(pathOfFile);
            ConsoleTools.WriteLineToConsoleInColor(fileNameWithExtension, ConsoleColor.Cyan);
            ConsoleTools.WriteLineToConsoleInColor(fileHashString, ConsoleColor.Cyan);
        }
    }
}