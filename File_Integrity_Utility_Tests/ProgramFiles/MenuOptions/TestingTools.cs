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


        public static void CreateNewTestFile(string pathOfFileToCreate)
        {
            Random random = new Random();
            StreamWriter textFileToWriteTo = File.CreateText(pathOfFileToCreate);
            for (int currentCharNumber = 0; currentCharNumber < 1024; ++currentCharNumber)
            {
                textFileToWriteTo.Write(random.Next(10));
            }
            textFileToWriteTo.Close();
        }
    }
}
