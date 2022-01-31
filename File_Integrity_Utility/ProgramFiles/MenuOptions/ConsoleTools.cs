using System.Text;

namespace File_Integrity_Utility.ProgramFiles.MenuOptions
{
    class ConsoleTools
    {
        public static void SetConsoleEncoding()
        {
            // In order to support a range of folder and file names, such as ones containing emojis (ex. ☺), we must parse the input as Unicode (UTF-16). UTF-8 does not work:
            Console.InputEncoding = Encoding.Unicode;
        }


        public static string PromptForUserInput(string promptMessage)
        {
            WriteToConsoleInColor(promptMessage, ConsoleColor.Yellow);
            string userInput = Console.ReadLine();
            // On Windows 11, you can obtain the path of a folder or file easily:
            // 1. Right click the folder or file.
            // 2. Select "Copy as path".
            // The path will be copied to the clipboard. However, it will be surrounded by double quotes, so we must remove any:
            return userInput.Trim('\"');
        }


        public static void WriteToConsoleInColor(string lineToWrite, ConsoleColor colorToWriteIn)
        {
            Console.ForegroundColor = colorToWriteIn;
            Console.Write(lineToWrite);
            Console.ResetColor();
        }


        public static string ObtainFilePathFromUser()
        {
            string userInput = PromptForUserInput("Please enter the full path of the file to analyze: ");
            while (!File.Exists(userInput))
            {
                WriteLineToConsoleInColor("Error, no file found with the provided path.", ConsoleColor.Red);
                userInput = PromptForUserInput("\n" + "Please enter the full path of the file to analyze: ");
            }
            return userInput;
        }


        public static void WriteLineToConsoleInColor(string lineToWrite, ConsoleColor colorToWriteLineIn)
        {
            WriteToConsoleInColor(lineToWrite, colorToWriteLineIn);
            Console.WriteLine();
        }


        public static string ObtainFolderPathFromUser()
        {
            string userInput = PromptForUserInput("Please enter the full path of the folder to analyze: ");
            while (!Directory.Exists(userInput))
            {
                WriteLineToConsoleInColor("Error, no folder found with the provided path.", ConsoleColor.Red);
                userInput = PromptForUserInput("\n" + "Please enter the full path of the folder to analyze: ");
            }
            return userInput;
        }
    }
}