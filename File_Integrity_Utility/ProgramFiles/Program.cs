﻿using File_Integrity_Utility.ProgramFiles.MenuOptions;

namespace File_Integrity_Utility.ProgramFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleTools.SetConsoleEncoding();
            int userChosenMenuOption = ObtainMenuOptionFromUser();
            while (userChosenMenuOption != 0)
            {
                ConsoleTools.WriteLineToConsoleInColor("\n" + "Menu option " + userChosenMenuOption + ":" + "\n", ConsoleColor.Magenta);
                if (userChosenMenuOption == 1)
                {
                    MenuOption1.DisplayFileNameFollowedByItsHash();
                }
                else if (userChosenMenuOption == 2)
                {
                    MenuOption2.DisplayIfFileHashMatchesProvidedHash();
                }
                else if (userChosenMenuOption == 3)
                {
                    MenuOption3.DisplayIfBothFilesAreIdentical();
                }
                else if (userChosenMenuOption == 4)
                {
                    MenuOption4.RenameAllTopLevelFilesInGivenFolderAsTheirHash();
                }
                else if (userChosenMenuOption == 5)
                {
                    MenuOption5.GenerateTextFileListingFileNamesToHashes();
                }
                else if (userChosenMenuOption == 6)
                {
                    MenuOption6.CompareHashesOfFilesInGivenFolderToRecordedHashesInTextFile();
                }
                else // (userChosenMenuOption == 7)
                {
                    MenuOption7.DisplayIfBothFoldersContainTheSameFilesAndStructure();
                }
                Console.WriteLine();
                userChosenMenuOption = ObtainMenuOptionFromUser();
            }
        }


        private static int ObtainMenuOptionFromUser()
        {
            DisplayMainMenu();
            ConsoleTools.WriteToConsoleInColor("\n" + "Please select an option from the main menu: ", ConsoleColor.Yellow); ;
            int userInputAsInt = AttemptToReadIntFromUser();
            while (userInputAsInt < 0 || userInputAsInt > 7)
            {
                ConsoleTools.WriteLineToConsoleInColor("Error, no valid menu option chosen.", ConsoleColor.Red);
                ConsoleTools.WriteToConsoleInColor("\n" + "Please select an option from the main menu: ", ConsoleColor.Yellow);
                userInputAsInt = AttemptToReadIntFromUser();
            }
            return userInputAsInt;
        }


        private static void DisplayMainMenu()
        {
            Console.WriteLine("MAIN MENU:" + "\n");
            Console.WriteLine("0: Exit this program.");
            Console.WriteLine("1: Given a file, generate its SHA 256 hash and display it.");
            Console.WriteLine("2: Given a file and its SHA 256 hash, determine if the file's hash matches the provided hash.");
            Console.WriteLine("3: Given two files, determine if both are identical (both have matching hashes).");
            Console.WriteLine("4: Given a folder, rename each of its top level files as their SHA 256 hash.");
            Console.WriteLine("5: Given a folder, generate a text file of SHA 256 hashes for all of its top level files.");
            Console.WriteLine("This text file will be placed within the given folder.");
            Console.WriteLine("6: Given a folder, which already contains a text file of SHA 256 hashes for all of its top level files (that was generated by this program), determine:");
            Console.WriteLine("    a. That all files listed in the text file are present.");
            Console.WriteLine("    b. That the hashes of all files present match those in the text file.");
            Console.WriteLine("7: Given two folders, determine if both are identical:");
            Console.WriteLine("    a. Both have the same number of files.");
            Console.WriteLine("    b. Both have the same folder/file structure.");
            Console.WriteLine("    c. Each pair of equivalent files have the same name.");
            Console.WriteLine("    d. Each pair of equivalent files have the same contents.");
        }


        private static int AttemptToReadIntFromUser()
        {
            string userInput = Console.ReadLine();
            if (userInput == null || userInput.Length == 0)
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