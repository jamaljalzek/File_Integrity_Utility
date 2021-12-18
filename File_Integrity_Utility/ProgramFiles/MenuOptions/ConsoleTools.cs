using System.Text;

namespace File_Integrity_Utility.ProgramFiles.MenuOptions
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
            WriteToConsoleInGivenColor(promptMessage, ConsoleColor.Yellow);
            string userInput = Console.ReadLine();
            // On Windows 11, you can obtain the path of a folder or file easily:
            // 1. Right click the folder or file.
            // 2. Select "Copy as path".
            // The path will be copied to the clipboard. However, it will be surrounded by double quotes, so we must remove any:
            return userInput.Trim('\"');
        }


        public static void WriteToConsoleInGivenColor(string lineToWrite, ConsoleColor colorToWriteLineIn)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(lineToWrite);
            Console.ResetColor();
        }


        public static string ObtainFullFilePathFromUser()
        {
            SetConsoleEncoding();
            string userInput = PromptForUserInput("Please enter the full path of the file to analyze: ");
            while (!File.Exists(userInput))
            {
                WriteLineToConsoleInGivenColor("ERROR, no file found with the provided path.", ConsoleColor.Red);
                Console.WriteLine();
                userInput = PromptForUserInput("Please enter the full path of the file to analyze: ");
            }
            return userInput;
        }


        public static void WriteLineToConsoleInGivenColor(string lineToWrite, ConsoleColor colorToWriteLineIn)
        {
            Console.ForegroundColor = colorToWriteLineIn;
            Console.WriteLine(lineToWrite);
            Console.ResetColor();
        }


        public static string ObtainFullFolderPathFromUser()
        {
            ConsoleTools.SetConsoleEncoding();
            string userInput = ConsoleTools.PromptForUserInput("Please enter the full path of the folder to analyze: ");
            while (!Directory.Exists(userInput))
            {
                ConsoleTools.WriteLineToConsoleInGivenColor("ERROR, no folder found with the provided path.", ConsoleColor.Red);
                Console.WriteLine();
                userInput = ConsoleTools.PromptForUserInput("Please enter the full path of the folder to analyze: ");
            }
            return userInput;
        }
    }
}