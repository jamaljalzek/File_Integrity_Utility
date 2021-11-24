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
            string userProvidedHash = ConsoleTools.PromptForUserInput("Please enter the full SHA 256 hash (case-insensitive) to compare against the given file: ");
            Console.WriteLine();
            if (!fileHashString.Equals(userProvidedHash, StringComparison.CurrentCultureIgnoreCase))
            {
                Console.WriteLine("VERDICT: NOT EQUIVALENT.");
            }
            else
            {
                Console.WriteLine("VERDICT: EQUIVALENT.");
            }
        }
    }
}