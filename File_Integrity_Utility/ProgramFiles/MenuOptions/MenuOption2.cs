namespace FileIntegrityUtility.ProgramFiles.MenuOptions
{
    public class MenuOption2
    {
        public static void DisplayIfFileHashMatchesProvidedHash()
        {
            string fullPathOfFile = ConsoleTools.ObtainFullFilePathFromUser();
            Console.WriteLine();
            Console.WriteLine("Generating hash for given file...");
            string fileHashString = HashingTools.ObtainFileHash(fullPathOfFile);
            Console.WriteLine("File hash complete.");
            Console.WriteLine();
            string userProvidedHash = ConsoleTools.PromptForUserInput("Please enter the full SHA 256 hash (case-insensitive) to compare against the given file: ");
            Console.WriteLine();
            Console.WriteLine("VERDICT:");
            if (!fileHashString.Equals(userProvidedHash, StringComparison.CurrentCultureIgnoreCase))
            {
                ConsoleTools.WriteLineToConsoleInGivenColor("NOT EQUIVALENT", ConsoleColor.Red);
            }
            else
            {
                ConsoleTools.WriteLineToConsoleInGivenColor("EQUIVALENT", ConsoleColor.Green);
            }
        }
    }
}