namespace File_Integrity_Utility.ProgramFiles.MenuOptions
{
    class MenuOption3
    {
        public static void DisplayIfBothFilesAreIdentical()
        {
            string fullPathOfFirstFile = ObtainFilePathFromUser("first");
            string fullPathOfSecondFile = ObtainFilePathFromUser("second");
            Console.WriteLine("Generating hashes for both files...");
            string hashOfFirstFile = ObtainHashOfUserChosenFile(fullPathOfFirstFile);
            string hashOfSecondFile = ObtainHashOfUserChosenFile(fullPathOfSecondFile);
            Console.WriteLine("All file hashes generated.");
            Console.WriteLine("\n" + "VERDICT:");
            if (!hashOfFirstFile.Equals(hashOfSecondFile))
            {
                ConsoleTools.WriteLineToConsoleInGivenColor("NOT EQUIVALENT", ConsoleColor.Red);
            }
            else
            {
                ConsoleTools.WriteLineToConsoleInGivenColor("EQUIVALENT", ConsoleColor.Green);
            }
        }


        private static string ObtainHashOfUserChosenFile(string pathOfFile)
        {
            Console.Write(pathOfFile + "... ");
            string hashOfFile = HashingTools.ObtainFileHash(pathOfFile);
            Console.WriteLine("DONE");
            return hashOfFile;
        }


        private static string ObtainFilePathFromUser(string firstOrSecond)
        {
            string userInput = ConsoleTools.PromptForUserInput("Please enter the full path of the " + firstOrSecond + " file to analyze: ");
            while (!File.Exists(userInput))
            {
                ConsoleTools.WriteLineToConsoleInGivenColor("ERROR, no file found with the provided path.", ConsoleColor.Red);
                userInput = ConsoleTools.PromptForUserInput("\n" + "Please enter the full path of the " + firstOrSecond + " file to analyze: ");
            }
            Console.WriteLine();
            return userInput;
        }
    }
}