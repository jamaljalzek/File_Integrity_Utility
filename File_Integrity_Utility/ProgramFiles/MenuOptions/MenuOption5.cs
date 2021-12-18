namespace File_Integrity_Utility.ProgramFiles.MenuOptions
{
    class MenuOption5
    {
        public static void DisplayIfBothFoldersContainTheSameFilesAndStructure()
        {
            string fullPathOfFirstFolder = ObtainFullFolderPathFromUser("first");
            Console.WriteLine();
            string fullPathOfSecondFolder = ObtainFullFolderPathFromUser("second");
            // The format of the below list is that of [file1Name, file1Hash, file2Name, file2Hash, etc.]:
            List<string> firstFolderFileNamesFollowedByTheirHashes = HashingTools.GetListOfFileFullPathsFollowedByTheirHashes(fullPathOfFirstFolder, SearchOption.AllDirectories);
            List<string> secondFolderFileNamesFollowedByTheirHashes = HashingTools.GetListOfFileFullPathsFollowedByTheirHashes(fullPathOfSecondFolder, SearchOption.AllDirectories);
            DisplayIfBothListsAreIdentical(firstFolderFileNamesFollowedByTheirHashes, secondFolderFileNamesFollowedByTheirHashes);
        }


        private static string ObtainFullFolderPathFromUser(string firstOrSecond)
        {
            ConsoleTools.SetConsoleEncoding();
            string userInput = ConsoleTools.PromptForUserInput("Please enter the full path of the " + firstOrSecond + " folder to analyze: ");
            while (!Directory.Exists(userInput))
            {
                ConsoleTools.WriteLineToConsoleInGivenColor("ERROR, no folder found with the provided path.", ConsoleColor.Red);
                Console.WriteLine();
                userInput = ConsoleTools.PromptForUserInput("Please enter the full path of the " + firstOrSecond + " folder to analyze: ");
            }
            return userInput;
        }


        private static void DisplayIfBothListsAreIdentical(List<string> firstFolderFileNamesFollowedByTheirHashes, List<string> secondFolderFileNamesFollowedByTheirHashes)
        {
            Console.WriteLine();
            Console.WriteLine("VERDICT:");
            if (firstFolderFileNamesFollowedByTheirHashes.Count != secondFolderFileNamesFollowedByTheirHashes.Count)
            {
                ConsoleTools.WriteLineToConsoleInGivenColor("NOT EQUIVALENT", ConsoleColor.Red);
                ConsoleTools.WriteLineToConsoleInGivenColor(firstFolderFileNamesFollowedByTheirHashes.Count + " items != " + secondFolderFileNamesFollowedByTheirHashes.Count + " items", ConsoleColor.Red);
                return;
            }
            DisplayIfFolderContentsAreIdentical(firstFolderFileNamesFollowedByTheirHashes, secondFolderFileNamesFollowedByTheirHashes);
        }


        private static void DisplayIfFolderContentsAreIdentical(List<string> firstFolderFileNamesFollowedByTheirHashes, List<string> secondFolderFileNamesFollowedByTheirHashes)
        {
            for (int currentIndex = 1; currentIndex < firstFolderFileNamesFollowedByTheirHashes.Count; currentIndex += 2)
            {
                string currentFirstFolderItemHash = firstFolderFileNamesFollowedByTheirHashes[currentIndex];
                string currentSecondFolderItemHash = secondFolderFileNamesFollowedByTheirHashes[currentIndex];
                if (!currentFirstFolderItemHash.Equals(currentSecondFolderItemHash))
                {
                    string currentFirstFolderItemName = firstFolderFileNamesFollowedByTheirHashes[currentIndex - 1];
                    string currentSecondFolderItemName = secondFolderFileNamesFollowedByTheirHashes[currentIndex - 1];
                    ConsoleTools.WriteLineToConsoleInGivenColor("NOT EQUIVALENT\n" + currentFirstFolderItemName + " != " + currentSecondFolderItemName, ConsoleColor.Red);
                    return;
                }
            }
            ConsoleTools.WriteLineToConsoleInGivenColor("EQUIVALENT", ConsoleColor.Green);
        }
    }
}