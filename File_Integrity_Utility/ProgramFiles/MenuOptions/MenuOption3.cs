namespace File_Integrity_Utility.ProgramFiles.MenuOptions
{
    class MenuOption3
    {
        public static void DisplayIfBothFilesAreIdentical()
        {
            string hashOfFirstFile = ObtainHashOfUserChosenFile("first");
            string hashOfSecondFile = ObtainHashOfUserChosenFile("second");
            Console.WriteLine("\n" + "Verdict:");
            if (hashOfFirstFile.Equals(hashOfSecondFile))
            {
                ConsoleTools.WriteLineToConsoleInColor("Equivalent", ConsoleColor.Green);
            }
            else
            {
                ConsoleTools.WriteLineToConsoleInColor("Not equivalent", ConsoleColor.Red);
            }
        }


        private static string ObtainHashOfUserChosenFile(string firstOrSecond)
        {
            string pathOfFile = ObtainFilePathFromUser(firstOrSecond);
            Console.WriteLine("\n" + "Generating hash for given file...");
            string fileHashString = HashingTools.ObtainFileHash(pathOfFile);
            Console.WriteLine("File hash complete." + "\n");
            return fileHashString;
        }


        private static string ObtainFilePathFromUser(string firstOrSecond)
        {
            string userInput = ConsoleTools.PromptForUserInput("Please enter the full path of the " + firstOrSecond + " file to analyze: ");
            while (!File.Exists(userInput))
            {
                ConsoleTools.WriteLineToConsoleInColor("Error, no file found with the provided path.", ConsoleColor.Red);
                userInput = ConsoleTools.PromptForUserInput("\n" + "Please enter the full path of the " + firstOrSecond + " file to analyze: ");
            }
            return userInput;
        }
    }
}