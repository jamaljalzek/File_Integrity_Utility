namespace File_Integrity_Utility.ProgramFiles.MenuOptions
{
    class MenuOption7
    {
        public static void DisplayIfBothFoldersContainTheSameFilesAndStructure()
        {
            string pathOfFirstFolder = ObtainFolderPathFromUser("first");
            string pathOfSecondFolder = ObtainFolderPathFromUser("second");
            List<string[]> listOfFirstFolderFilePathsToHashes = HashingTools.GetListOfFilePathsToHashes(pathOfFirstFolder, SearchOption.AllDirectories);
            List<string[]> listOfSecondFolderFilePathsToHashes = HashingTools.GetListOfFilePathsToHashes(pathOfSecondFolder, SearchOption.AllDirectories);
            DisplayIfBothListsAreIdentical(listOfFirstFolderFilePathsToHashes, listOfSecondFolderFilePathsToHashes);
        }


        private static string ObtainFolderPathFromUser(string firstOrSecond)
        {
            string userInput = ConsoleTools.PromptForUserInput("Please enter the full path of the " + firstOrSecond + " folder to analyze: ");
            while (!Directory.Exists(userInput))
            {
                ConsoleTools.WriteLineToConsoleInGivenColor("ERROR, no folder found with the provided path.", ConsoleColor.Red);
                userInput = ConsoleTools.PromptForUserInput("\n" + "Please enter the full path of the " + firstOrSecond + " folder to analyze: ");
            }
            Console.WriteLine();
            return userInput;
        }


        private static void DisplayIfBothListsAreIdentical(List<string[]> listOfFirstFolderFilePathsToHashes, List<string[]> listOfSecondFolderFilePathsToHashes)
        {
            Console.WriteLine("\n" + "VERDICT:");
            if (listOfFirstFolderFilePathsToHashes.Count != listOfSecondFolderFilePathsToHashes.Count)
            {
                ConsoleTools.WriteLineToConsoleInGivenColor("NOT EQUIVALENT", ConsoleColor.Red);
                ConsoleTools.WriteLineToConsoleInGivenColor(listOfFirstFolderFilePathsToHashes.Count + " items != " + listOfSecondFolderFilePathsToHashes.Count + " items", ConsoleColor.Red);
                return;
            }
            DisplayIfListsAreIdentical(listOfFirstFolderFilePathsToHashes, listOfSecondFolderFilePathsToHashes);
        }


        private static void DisplayIfListsAreIdentical(List<string[]> listOfFirstFolderFilePathsToHashes, List<string[]> listOfSecondFolderFilePathsToHashes)
        {
            for (int currentIndex = 0; currentIndex < listOfFirstFolderFilePathsToHashes.Count; ++currentIndex)
            {
                string[] currentFirstFolderFileNameAndHash = listOfFirstFolderFilePathsToHashes[currentIndex];
                string[] currentSecondFolderFileNameAndHash = listOfSecondFolderFilePathsToHashes[currentIndex];
                string currentFirstFolderFileHash = currentFirstFolderFileNameAndHash[1];
                string currentSecondFolderFileHash = currentSecondFolderFileNameAndHash[1];
                if (!currentFirstFolderFileHash.Equals(currentSecondFolderFileHash))
                {
                    string currentFirstFolderFileName = currentFirstFolderFileNameAndHash[0];
                    string currentSecondFolderFileName = currentSecondFolderFileNameAndHash[0];
                    ConsoleTools.WriteLineToConsoleInGivenColor("NOT EQUIVALENT:" + "\n" + currentFirstFolderFileName + " != " + currentSecondFolderFileName, ConsoleColor.Red);
                    return;
                }
            }
            ConsoleTools.WriteLineToConsoleInGivenColor("EQUIVALENT", ConsoleColor.Green);
        }
    }
}