namespace File_Integrity_Utility.ProgramFiles.MenuOptions
{
    class MenuOption7
    {
        public static void DisplayIfBothFoldersContainTheSameFilesAndStructure()
        {
            List<string[]> firstFolderFilePathsToHashes = ObtainListOfFilePathsToHashesInUserChosenFolder("first");
            List<string[]> secondFolderFilePathsToHashes = ObtainListOfFilePathsToHashesInUserChosenFolder("second");
            bool areFoldersIdentical = FolderContentsComparator.AreFoldersIdentical(firstFolderFilePathsToHashes, secondFolderFilePathsToHashes);
            DisplayResultOfComparison(areFoldersIdentical);
        }


        private static List<string[]> ObtainListOfFilePathsToHashesInUserChosenFolder(string firstOrSecond)
        {
            string pathOfFolder = ObtainFolderPathFromUser(firstOrSecond);
            Console.WriteLine("Analyzing " + firstOrSecond + " folder...");
            List<string[]> filePathsToRecentHashes = HashingTools.GetListOfFilePathsToHashes(pathOfFolder, SearchOption.AllDirectories);
            Console.WriteLine("Analyzing complete.");
            return filePathsToRecentHashes;
        }


        private static string ObtainFolderPathFromUser(string firstOrSecond)
        {
            string userInput = ConsoleTools.PromptForUserInput("Please enter the full path of the " + firstOrSecond + " folder to analyze: ");
            while (!Directory.Exists(userInput))
            {
                ConsoleTools.WriteLineToConsoleInColor("Error, no folder found with the provided path.", ConsoleColor.Red);
                userInput = ConsoleTools.PromptForUserInput("\n" + "Please enter the full path of the " + firstOrSecond + " folder to analyze: ");
            }
            Console.WriteLine();
            return userInput;
        }


        private static void DisplayResultOfComparison(bool areFoldersIdentical)
        {
            if (areFoldersIdentical)
            {
                ConsoleTools.WriteLineToConsoleInColor("Equivalent", ConsoleColor.Green);
            }
            else
            {
                ConsoleTools.WriteLineToConsoleInColor("Not equivalent", ConsoleColor.Red);
            }
        }
    }


    class FolderContentsComparator
    {
        public static bool AreFoldersIdentical(List<string[]> firstFolderFilePathsToHashes, List<string[]> secondFolderFilePathsToHashes)
        {
            if (firstFolderFilePathsToHashes.Count != secondFolderFilePathsToHashes.Count)
            {
                ConsoleTools.WriteLineToConsoleInColor(firstFolderFilePathsToHashes.Count + " items != " + secondFolderFilePathsToHashes.Count + " items", ConsoleColor.Red);
                return false;
            }
            return DoListsContainSameContents(firstFolderFilePathsToHashes, secondFolderFilePathsToHashes);
        }


        private static bool DoListsContainSameContents(List<string[]> firstFolderFilePathsToHashes, List<string[]> secondFolderFilePathsToHashes)
        {
            Dictionary<string, string> secondFolderFileNamesToHashes = GetMapOfFileNamesToHashes(secondFolderFilePathsToHashes);
            foreach (string[] currentFirstFolderFilePathAndHash in firstFolderFilePathsToHashes)
            {
                if (!DoBothFoldersContainSameFileNameToHash(currentFirstFolderFilePathAndHash, secondFolderFileNamesToHashes))
                {
                    return false;
                }
            }
            return true;
        }


        private static Dictionary<string, string> GetMapOfFileNamesToHashes(List<string[]> filePathsToHashes)
        {
            Dictionary<string, string> fileNamesToHashes = new Dictionary<string, string>(filePathsToHashes.Count);
            foreach (string[] currentFilePathAndHash in filePathsToHashes)
            {
                string currentFilePath = currentFilePathAndHash[0];
                string currentFileName = Path.GetFileName(currentFilePath);
                string currentFileHash = currentFilePathAndHash[1];
                fileNamesToHashes[currentFileName] = currentFileHash;
            }
            return fileNamesToHashes;
        }


        private static bool DoBothFoldersContainSameFileNameToHash(string[] currentFirstFolderFilePathAndHash, Dictionary<string, string> secondFolderFileNamesToHashes)
        {
            string currentFirstFolderFilePath = currentFirstFolderFilePathAndHash[0];
            string currentFirstFolderFileName = Path.GetFileName(currentFirstFolderFilePath);
            if (!secondFolderFileNamesToHashes.ContainsKey(currentFirstFolderFileName))
            {
                ConsoleTools.WriteLineToConsoleInColor(currentFirstFolderFileName + " was not found in the second folder", ConsoleColor.Red);
                return false;
            }
            string currentFirstFolderFileHash = currentFirstFolderFilePathAndHash[1];
            string currentSecondFolderFileHash = secondFolderFileNamesToHashes[currentFirstFolderFileName];
            if (!currentFirstFolderFileHash.Equals(currentSecondFolderFileHash))
            {
                ConsoleTools.WriteLineToConsoleInColor(currentFirstFolderFileHash + " != " + currentSecondFolderFileHash, ConsoleColor.Red);
                return false;
            }
            return true;
        }
    }
}