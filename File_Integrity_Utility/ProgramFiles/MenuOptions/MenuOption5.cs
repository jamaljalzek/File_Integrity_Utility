namespace File_Integrity_Utility.ProgramFiles.MenuOptions
{
    public class MenuOption5
    {
        public static void CompareNameOfEachTopLevelFileInGivenFolderToItsHash()
        {
            string pathOfFolder = ConsoleTools.ObtainFolderPathFromUser();
            string[] allTopLevelFilePathsInFolder = Directory.GetFiles(pathOfFolder);
            bool wasInconsistencyFound = VerifyEachFileNameMatchesItsHash(allTopLevelFilePathsInFolder, pathOfFolder);
            DisplayIfAllFilesAreConsistentOrNot(wasInconsistencyFound);
        }


        private static bool VerifyEachFileNameMatchesItsHash(string[] allTopLevelFilePathsInFolder, string pathOfFolder)
        {
            Console.WriteLine('\n' + "Comparing the name of each top level file in " + pathOfFolder + "to its hash...");
            bool wasInconsistencyFound = CompareEachFileNameToItsHash(allTopLevelFilePathsInFolder);
            ConsoleTools.WriteLineToConsoleInColor('\n' + "Comparing files complete.", ConsoleColor.Cyan);
            return wasInconsistencyFound;
        }


        private static bool CompareEachFileNameToItsHash(string[] allTopLevelFilePathsInFolder)
        {
            bool wasInconsistencyFound = false;
            foreach (string currentFilePath in allTopLevelFilePathsInFolder)
            {
                bool isCurrentFileConsistent = CompareFileNameToItsHash(currentFilePath);
                if (!isCurrentFileConsistent)
                {
                    wasInconsistencyFound = true;
                }
            }
            return wasInconsistencyFound;
        }


        private static bool CompareFileNameToItsHash(string filePath)
        {
            string fileNameWithExtension = Path.GetFileName(filePath);
            string fileHashWithExtension = HashingTools.ObtainFileHash(filePath) + Path.GetExtension(filePath);
            bool isFileNameConsistentWithHash = fileNameWithExtension.Equals(fileHashWithExtension);
            if (isFileNameConsistentWithHash)
            {
                Console.WriteLine(fileNameWithExtension + " = " + fileHashWithExtension);
            }
            else
            {
                ConsoleTools.WriteLineToConsoleInColor(fileNameWithExtension + " != " + fileHashWithExtension, ConsoleColor.Red);
            }
            return isFileNameConsistentWithHash;
        }


        private static void DisplayIfAllFilesAreConsistentOrNot(bool wasAnyInconsistencyFound)
        {
            Console.WriteLine('\n' + "Verdict:");
            if (wasAnyInconsistencyFound)
            {
                ConsoleTools.WriteLineToConsoleInColor("Inconsistency(s) found", ConsoleColor.Red);
            }
            else
            {
                ConsoleTools.WriteLineToConsoleInColor("No inconsistency(s) found", ConsoleColor.Green);
            }
        }
    }
}