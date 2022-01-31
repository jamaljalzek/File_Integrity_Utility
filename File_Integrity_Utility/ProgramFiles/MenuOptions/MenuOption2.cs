namespace File_Integrity_Utility.ProgramFiles.MenuOptions
{
    public class MenuOption2
    {
        public static void DisplayIfFileHashMatchesProvidedHash()
        {
            string pathOfFile = ConsoleTools.ObtainFilePathFromUser();
            Console.WriteLine("\n" + "Generating hash for given file...");
            string fileHashString = HashingTools.ObtainFileHash(pathOfFile);
            Console.WriteLine("File hash complete." + "\n");
            string userProvidedHash = ConsoleTools.PromptForUserInput("Please enter the full SHA 256 hash (case-insensitive) to compare against the given file: ");
            Console.WriteLine("\n" + "Verdict:");
            if (fileHashString.Equals(userProvidedHash, StringComparison.CurrentCultureIgnoreCase))
            {
                ConsoleTools.WriteLineToConsoleInColor("Equivalent", ConsoleColor.Green);
            }
            else
            {
                ConsoleTools.WriteLineToConsoleInColor("Not equivalent", ConsoleColor.Red);
            }
        }
    }
}