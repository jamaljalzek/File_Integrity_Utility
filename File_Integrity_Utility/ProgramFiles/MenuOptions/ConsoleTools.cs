﻿using System.Text;

namespace FileIntegrityUtility.ProgramFiles.MenuOptions
{
    class ConsoleTools
    {
        public static void SetConsoleEncoding()
        {
            // In order to support a range of folder and file names, such as ones containing emojis (ex. ☺), we must parse the input as Unicode (UTF-16).
            // UTF-8 does not work:
            Console.InputEncoding = Encoding.Unicode;
        }


        public static string PromptForUserInput(string promptMessage)
        {
            Console.Write(promptMessage);
            string userInput = Console.ReadLine();
            // On Windows 11, you can obtain the path of a folder or file easily:
            // 1. Right click the folder or file.
            // 2. Select "Copy as path".
            // The path will be copied to the clipboard. However, it will be surrounded by double quotes, so we must remove any:
            return userInput.Trim('\"');
        }


        public static string ObtainFullFilePathFromUser()
        {
            SetConsoleEncoding();
            string userInput = PromptForUserInput("Please enter the full path of the file to analyze: ");
            while (!File.Exists(userInput))
            {
                Console.WriteLine("ERROR, no file found with the provided path.");
                userInput = PromptForUserInput("Please enter the full path of the file to analyze: ");
            }
            return userInput;
        }
    }
}