using File_Integrity_Utility.ProgramFiles;
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


        public static string[] CreateMultipleTestFilesWithSameContents(string pathOfTestFolder, int numberOfTestFilesToCreate)
        {
            string pathOfOriginalTestFile = CreateNewTestFile(pathOfTestFolder, "File_Integrity_Utility_Test_File.txt");
            string[] listOfTestFileOriginalNames = new string[numberOfTestFilesToCreate];
            for (int currentTestFileNumber = 0; currentTestFileNumber < numberOfTestFilesToCreate; ++currentTestFileNumber)
            {
                string nameOfCurrentTestFile = "File_Integrity_Utility_Test_File" + currentTestFileNumber + ".txt";
                string pathOfCurrentTestFile = pathOfTestFolder + Path.DirectorySeparatorChar + nameOfCurrentTestFile;
                File.Copy(pathOfOriginalTestFile, pathOfCurrentTestFile, true);
                listOfTestFileOriginalNames[currentTestFileNumber] = nameOfCurrentTestFile;
            }
            File.Delete(pathOfOriginalTestFile);
            return listOfTestFileOriginalNames;
        }


        public static string[] GetListOfTestFileExpectedNewNames(string pathOfTestFolder, string[] listOfTestFileOriginalNames)
        {
            string[] listOfTestFileNewNames = new string[listOfTestFileOriginalNames.Length];
            for (int currentTestFileNumber = 0; currentTestFileNumber < listOfTestFileOriginalNames.Length; ++currentTestFileNumber)
            {
                string nameOfCurrentTestFile = listOfTestFileOriginalNames[currentTestFileNumber];
                string pathOfCurrentTestFile = pathOfTestFolder + Path.DirectorySeparatorChar + nameOfCurrentTestFile;
                string currentTestFileExpectedNewName = HashingTools.ObtainFileHash(pathOfCurrentTestFile) + ".txt";
                listOfTestFileNewNames[currentTestFileNumber] = currentTestFileExpectedNewName;
            }
            return listOfTestFileNewNames;
        }


        public static string[] CreateMultipleTestFilesWithDifferentContents(string pathOfTestFolder, int numberOfTestFilesToCreate)
        {
            string[] listOfTestFileOriginalNames = new string[numberOfTestFilesToCreate];
            for (int currentTestFileNumber = 0; currentTestFileNumber < numberOfTestFilesToCreate; ++currentTestFileNumber)
            {
                string nameOfCurrentTestFile = "File_Integrity_Utility_Test_File" + currentTestFileNumber + ".txt";
                CreateNewTestFile(pathOfTestFolder, nameOfCurrentTestFile);
                listOfTestFileOriginalNames[currentTestFileNumber] = nameOfCurrentTestFile;
            }
            return listOfTestFileOriginalNames;
        }
    }
}