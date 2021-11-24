using FileIntegrityUtility.ProgramFiles.MenuOptions;

namespace FileIntegrityUtility.ProgramFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            int userChosenMenuOption = ObtainMenuOptionFromUser();
            while (userChosenMenuOption != 0)
            {
                Console.WriteLine();
                if (userChosenMenuOption == 1)
                {
                    MenuOption1.DisplayFileNameFollowedByItsHashCode();
                }
                else if (userChosenMenuOption == 2)
                {
                    MenuOption2.DisplayIfFileHashMatchesProvidedHash();
                }
                else if (userChosenMenuOption == 3)
                {
                    MenuOption3.DisplayIfBothFilesAreEquivalent();
                }
                else if (userChosenMenuOption == 4)
                {
                    MenuOption4.GenerateTextFileListingFileNamesFollowedByTheirHashes();
                }
                else // (userChosenMenuOption == 5)
                {
                    MenuOption5.DisplayIfBothFoldersContainTheSameFilesAndStructure();
                }
                Console.WriteLine();
                userChosenMenuOption = ObtainMenuOptionFromUser();
            }
        }


        private static int ObtainMenuOptionFromUser()
        {
            DisplayMainMenu();
            Console.WriteLine();
            Console.Write("Please select an option from the main menu: ");
            int userInputAsInt = AttemptToReadIntFromUser();
            while (userInputAsInt < 0 || userInputAsInt > 5)
            {
                Console.WriteLine("ERROR, no valid menu option chosen.");
                Console.Write("Please select an option from the main menu: ");
                userInputAsInt = AttemptToReadIntFromUser();
            }
            return userInputAsInt;
        }


        private static void DisplayMainMenu()
        {
            Console.WriteLine("MAIN MENU:");
            Console.WriteLine();
            Console.WriteLine("0: Exit this program.");
            Console.WriteLine("1: Given a file, generate its SHA 256 hash and display it.");
            Console.WriteLine("2: Given a file and its SHA 256 hash, determine if the file's hash matches the provided hash.");
            Console.WriteLine("3: Given two files, determine if both are identical (both have matching hashes).");
            Console.WriteLine("4: Given a folder, generate a text file of SHA 256 hashes for all of its top level files.");
            Console.WriteLine("This text file will be placed within the given folder.");
            Console.WriteLine("5: Given two folders, determine if both are identical:");
            Console.WriteLine("    a. Both have the same number of files.");
            Console.WriteLine("    b. Both have the same folder/file structure.");
            Console.WriteLine("    c. Each pair of equivalent files have the same name.");
            Console.WriteLine("    d. Each pair of equivalent files have the same contents.");
        }


        private static int AttemptToReadIntFromUser()
        {
            string userInput = Console.ReadLine();
            if (userInput.Length == 0)
            {
                return -1;
            }
            foreach (char currentChar in userInput)
            {
                if (!char.IsDigit(currentChar))
                {
                    return -1;
                }
            }
            return Convert.ToInt32(userInput);
        }
    }
}