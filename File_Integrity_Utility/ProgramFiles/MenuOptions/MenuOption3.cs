namespace FileIntegrityUtility.ProgramFiles.MenuOptions
{
    class MenuOption3
    {
        public static void DisplayIfBothFilesAreEquivalent()
        {
            string fullPathOfFirstFile = ObtainFullFilePathFromUser("first");
            string fullPathOfSecondFile = ObtainFullFilePathFromUser("second");
            Console.WriteLine();
            Console.WriteLine("Generating hashes for both files...");
            string fileHashOfFirstFile = ObtainHashOfUserChosenFile(fullPathOfFirstFile);
            string fileHashOfSecondFile = ObtainHashOfUserChosenFile(fullPathOfSecondFile);
            Console.WriteLine("All file hashes generated.");
            Console.WriteLine();
            if (!fileHashOfFirstFile.Equals(fileHashOfSecondFile))
            {
                Console.WriteLine("VERDICT: NOT EQUIVALENT.");
            }
            else
            {
                Console.WriteLine("VERDICT: EQUIVALENT.");
            }
        }


        private static string ObtainHashOfUserChosenFile(string fullPathOfFile)
        {
            Console.Write(fullPathOfFile + "... ");
            string hashOfFile = HashingTools.ObtainFileHash(fullPathOfFile);
            Console.WriteLine("DONE");
            return hashOfFile;
        }


        private static string ObtainFullFilePathFromUser(string firstOrSecond)
        {
            ConsoleTools.SetConsoleEncoding();
            string userInput = ConsoleTools.PromptForUserInput("Please enter the full path of the " + firstOrSecond + " file to analyze: ");
            while (!File.Exists(userInput))
            {
                Console.WriteLine("ERROR, no file found with the provided path.");
                userInput = ConsoleTools.PromptForUserInput("Please enter the full path of the " + firstOrSecond + " file to analyze: ");
            }
            return userInput;
        }
    }
}