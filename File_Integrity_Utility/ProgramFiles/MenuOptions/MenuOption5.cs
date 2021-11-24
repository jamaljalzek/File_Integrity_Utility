namespace FileIntegrityUtility.ProgramFiles.MenuOptions
{
    class MenuOption5
    {
        public static void DisplayIfBothFoldersContainTheSameFilesAndStructure()
        {
            string fullPathOfFirstFolder = ObtainFullFolderPathFromUser("first");
            string fullPathOfSecondFolder = ObtainFullFolderPathFromUser("second");
            // The format of the below list is that of [file1Name, file1Hash, file2Name, file2Hash, etc.]:
            List<string> firstFolderFileNamesFollowedByTheirHashes = HashingTools.GetListOfFileNamesFollowedByTheirHashes(fullPathOfFirstFolder, SearchOption.AllDirectories);
            List<string> secondFolderFileNamesFollowedByTheirHashes = HashingTools.GetListOfFileNamesFollowedByTheirHashes(fullPathOfSecondFolder, SearchOption.AllDirectories);
            string areBothFoldersIdentical = DetermineIfBothListsAreIdentical(firstFolderFileNamesFollowedByTheirHashes, secondFolderFileNamesFollowedByTheirHashes);
            Console.WriteLine();
            Console.WriteLine("VERDICT: " + areBothFoldersIdentical);
        }


        private static string ObtainFullFolderPathFromUser(string firstOrSecond)
        {
            ConsoleTools.SetConsoleEncoding();
            string userInput = ConsoleTools.PromptForUserInput("Please enter the full path of the " + firstOrSecond + " folder to analyze: ");
            while (!Directory.Exists(userInput))
            {
                Console.WriteLine("ERROR, no folder found with the provided path.");
                userInput = ConsoleTools.PromptForUserInput("Please enter the full path of the " + firstOrSecond + " folder to analyze: ");
            }
            return userInput;
        }


        private static string DetermineIfBothListsAreIdentical(List<string> firstFolderFileNamesFollowedByTheirHashes, List<string> secondFolderFileNamesFollowedByTheirHashes)
        {
            if (firstFolderFileNamesFollowedByTheirHashes.Count != secondFolderFileNamesFollowedByTheirHashes.Count)
            {
                return "NOT EQUIVALENT: both folders have a different number of items.";
            }
            for (int currentIndex = 0; currentIndex < firstFolderFileNamesFollowedByTheirHashes.Count; ++currentIndex)
            {
                string currentFirstFolderItem = firstFolderFileNamesFollowedByTheirHashes[currentIndex];
                string currentSecondFolderItem = secondFolderFileNamesFollowedByTheirHashes[currentIndex];
                if (!currentFirstFolderItem.Equals(currentSecondFolderItem))
                {
                    return "NOT EQUIVALENT: " + currentFirstFolderItem + " != " + currentSecondFolderItem + ".";
                }
            }
            return "EQUIVALENT.";
        }
    }
}