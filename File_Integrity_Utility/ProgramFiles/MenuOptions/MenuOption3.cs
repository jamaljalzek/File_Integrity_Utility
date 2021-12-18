namespace File_Integrity_Utility.ProgramFiles.MenuOptions
{
    class MenuOption3
    {
        public static void DisplayIfBothFilesAreEquivalent()
        {
            string fullPathOfFirstFile = ObtainFullFilePathFromUser("first");
            Console.WriteLine();
            string fullPathOfSecondFile = ObtainFullFilePathFromUser("second");
            Console.WriteLine();
            Console.WriteLine("Generating hashes for both files...");
            string fileHashOfFirstFile = ObtainHashOfUserChosenFile(fullPathOfFirstFile);
            string fileHashOfSecondFile = ObtainHashOfUserChosenFile(fullPathOfSecondFile);
            Console.WriteLine("All file hashes generated.");
            Console.WriteLine();
            Console.WriteLine("VERDICT:");
            if (!fileHashOfFirstFile.Equals(fileHashOfSecondFile))
            {
                ConsoleTools.WriteLineToConsoleInGivenColor("NOT EQUIVALENT", ConsoleColor.Red);
            }
            else
            {
                ConsoleTools.WriteLineToConsoleInGivenColor("EQUIVALENT", ConsoleColor.Green);
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
                ConsoleTools.WriteLineToConsoleInGivenColor("ERROR, no file found with the provided path.", ConsoleColor.Red);
                Console.WriteLine();
                userInput = ConsoleTools.PromptForUserInput("Please enter the full path of the " + firstOrSecond + " file to analyze: ");
            }
            return userInput;
        }
    }
}