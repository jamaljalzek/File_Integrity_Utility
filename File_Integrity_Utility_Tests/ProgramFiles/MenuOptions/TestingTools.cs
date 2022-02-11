using System;
using System.IO;

namespace File_Integrity_Utility_Tests.ProgramFiles.MenuOptions
{
    internal class TestingTools
    {
        public static StringWriter RerouteConsoleOutput()
        {
            StringWriter consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);
            return consoleOutput;
        }


        public static void PreloadInputToConsole(string input)
        {
            StringReader consoleInput = new StringReader(input);
            Console.SetIn(consoleInput);
        }


        public static string CreateNewTestFile(string pathOfFolder, string nameOfTestFile)
        {
            string pathOfTestFile = pathOfFolder + Path.DirectorySeparatorChar + nameOfTestFile;
            StreamWriter textFileToWriteTo = File.CreateText(pathOfTestFile);
            Random random = new Random();
            for (int currentCharNumber = 0; currentCharNumber < 1024; ++currentCharNumber)
            {
                textFileToWriteTo.Write(random.Next(10));
            }
            textFileToWriteTo.Close();
            return pathOfTestFile;
        }


        public static string CreateTestFolder(string nameOfTestFolder)
        {
            string pathOfTestFolder = Path.GetTempPath() + Path.DirectorySeparatorChar + nameOfTestFolder;
            Directory.CreateDirectory(pathOfTestFolder);
            return pathOfTestFolder;
        }
    }
}
