﻿using File_Integrity_Utility.ProgramFiles.MenuOptions;
using File_Integrity_Utility_Tests.ProgramFiles.MenuOptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace File_Integrity_Utility.ProgramFiles.MenuOptions_Tests
{
    [TestClass()]
    public class MenuOption4_Tests
    {
        [TestMethod()]
        [Timeout(5000)]
        public void RenameAllTopLevelFilesInGivenFolderAsTheirHash_GivenValidEmptyFolder_DisplayRenamingFilesComplete()
        {
            // Set up:
            string pathOfTestFolder = TestingTools.CreateTestFolder("File_Integrity_Utility_Test_Folder");
            StringWriter consoleOutput = TestingTools.RerouteConsoleOutput();

            // Test:
            LoadConsoleInputAndRunMethod(pathOfTestFolder);

            // Assert:
            string expectedDisplayedOutput = "Please enter the full path of the folder to analyze: \n" +
                                             "Renaming all top level files in " + pathOfTestFolder + "...\r\n\n" +
                                             "Renaming files complete.";
            string actualDisplayedOutput = consoleOutput.ToString().Trim();
            Assert.AreEqual(expectedDisplayedOutput, actualDisplayedOutput);

            // Tear down:
            Directory.Delete(pathOfTestFolder, true);
        }


        private void LoadConsoleInputAndRunMethod(string pathOfTestFolder)
        {
            // The user is prompted to enter into the console the path of the folder.
            // We pre-input this into the console so that it will be detected immediately after the prompt:
            TestingTools.PreloadInputToConsole(pathOfTestFolder);
            MenuOption4.RenameAllTopLevelFilesInGivenFolderAsTheirHash();
        }


        [TestMethod()]
        [Timeout(5000)]
        public void RenameAllTopLevelFilesInGivenFolderAsTheirHash_GivenValidFolderContainingOneFile_DisplayRenamingFilesComplete()
        {
            // Set up:
            string pathOfTestFolder = TestingTools.CreateTestFolder("File_Integrity_Utility_Test_Folder");
            string pathOfTestFile = TestingTools.CreateNewTestFile(pathOfTestFolder, "File_Integrity_Utility_Test_File.txt");
            string expectedNewNameOfTestFile = HashingTools.ObtainFileHash(pathOfTestFile) + ".txt";
            StringWriter consoleOutput = TestingTools.RerouteConsoleOutput();

            // Test:
            LoadConsoleInputAndRunMethod(pathOfTestFolder);

            // Assert:
            string expectedDisplayedOutput = "Please enter the full path of the folder to analyze: \n" +
                                             "Renaming all top level files in " + pathOfTestFolder + "...\r\n" +
                                             Path.GetFileName(pathOfTestFile) + " -> " + expectedNewNameOfTestFile + "\r\n\n" +
                                             "Renaming files complete.";
            string actualDisplayedOutput = consoleOutput.ToString().Trim();
            Assert.AreEqual(expectedDisplayedOutput, actualDisplayedOutput);

            // Tear down:
            Directory.Delete(pathOfTestFolder, true);
        }


        [TestMethod()]
        [Timeout(5000)]
        public void RenameAllTopLevelFilesInGivenFolderAsTheirHash_GivenValidFolderContainingOneFile_FileRenamed()
        {
            // Set up:
            string pathOfTestFolder = TestingTools.CreateTestFolder("File_Integrity_Utility_Test_Folder");
            string pathOfTestFile = TestingTools.CreateNewTestFile(pathOfTestFolder, "File_Integrity_Utility_Test_File.txt");
            string expectedNewNameOfTestFile = HashingTools.ObtainFileHash(pathOfTestFile) + ".txt";

            // Test:
            LoadConsoleInputAndRunMethod(pathOfTestFolder);

            // Assert:
            string expectedNewPathOfTestFile = pathOfTestFolder + Path.DirectorySeparatorChar + expectedNewNameOfTestFile;
            AssertThatFileExistsAtFirstPathAndNotAtSecondPath(expectedNewPathOfTestFile, pathOfTestFile);

            // Tear down:
            Directory.Delete(pathOfTestFolder, true);
        }


        private void AssertThatFileExistsAtFirstPathAndNotAtSecondPath(string firstFilePath, string secondFilePath)
        {
            bool doesFileExistAtFirstPath = File.Exists(firstFilePath);
            Assert.IsTrue(doesFileExistAtFirstPath);
            bool doesFileExistAtSecondPath = File.Exists(secondFilePath);
            Assert.IsFalse(doesFileExistAtSecondPath);
        }


        [TestMethod()]
        [Timeout(5000)]
        public void RenameAllTopLevelFilesInGivenFolderAsTheirHash_GivenValidFolderContainingTwoFilesWithSameHash_DisplayRenamingFilesComplete()
        {
            // Set up:
            string pathOfTestFolder = TestingTools.CreateTestFolder("File_Integrity_Utility_Test_Folder");
            string[] listOfTestFileOriginalNames = CreateMultipleTestFilesWithSameContents(pathOfTestFolder, 2);
            string[] listOfTestFileNewNames = GetListOfTestFileExpectedNewNames(pathOfTestFolder, listOfTestFileOriginalNames);
            StringWriter consoleOutput = TestingTools.RerouteConsoleOutput();

            // Test:
            LoadConsoleInputAndRunMethod(pathOfTestFolder);

            // Assert:
            string expectedDisplayedOutput = "Please enter the full path of the folder to analyze: \n" +
                                             "Renaming all top level files in " + pathOfTestFolder + "...\r\n" +
                                             listOfTestFileOriginalNames[0] + " -> " + listOfTestFileNewNames[0] + "\r\n" +
                                             listOfTestFileOriginalNames[1] + " -> " + "File not renamed, because another file already has this name: " + listOfTestFileNewNames[1] + "\r\n\n" +
                                             "Renaming files complete.";
            string actualDisplayedOutput = consoleOutput.ToString().Trim();
            Assert.AreEqual(expectedDisplayedOutput, actualDisplayedOutput);

            // Tear down:
            Directory.Delete(pathOfTestFolder, true);
        }


        private string[] CreateMultipleTestFilesWithSameContents(string pathOfTestFolder, int numberOfTestFilesToCreate)
        {
            string pathOfOriginalTestFile = TestingTools.CreateNewTestFile(pathOfTestFolder, "File_Integrity_Utility_Test_File.txt");
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


        private string[] GetListOfTestFileExpectedNewNames(string pathOfTestFolder, string[] listOfTestFileOriginalNames)
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


        [TestMethod()]
        [Timeout(5000)]
        public void RenameAllTopLevelFilesInGivenFolderAsTheirHash_GivenValidFolderContainingTwoFilesWithSameHash_FirstFileRenamedSecondFileOriginalName()
        {
            // Set up:
            string pathOfTestFolder = TestingTools.CreateTestFolder("File_Integrity_Utility_Test_Folder");
            string[] listOfTestFileOriginalNames = CreateMultipleTestFilesWithSameContents(pathOfTestFolder, 2);
            string[] listOfTestFileNewNames = GetListOfTestFileExpectedNewNames(pathOfTestFolder, listOfTestFileOriginalNames);

            // Test:
            LoadConsoleInputAndRunMethod(pathOfTestFolder);

            // Assert:
            string originalPathOfFirstTestFile = pathOfTestFolder + Path.DirectorySeparatorChar + listOfTestFileOriginalNames[0];
            string expectedNewPathOfTestFile = pathOfTestFolder + Path.DirectorySeparatorChar + listOfTestFileNewNames[1];
            AssertThatFileExistsAtFirstPathAndNotAtSecondPath(expectedNewPathOfTestFile, originalPathOfFirstTestFile);
            string originalPathOfSecondTestFile = pathOfTestFolder + Path.DirectorySeparatorChar + listOfTestFileOriginalNames[1];
            bool doesSecondTestFileExistAtOriginalPath = File.Exists(originalPathOfSecondTestFile);
            Assert.IsTrue(doesSecondTestFileExistAtOriginalPath);

            // Tear down:
            Directory.Delete(pathOfTestFolder, true);
        }


        [TestMethod()]
        [Timeout(5000)]
        public void RenameAllTopLevelFilesInGivenFolderAsTheirHash_GivenValidFolderContainingMultipleFilesWithDifferentHashes_DisplayRenamingFilesComplete()
        {
            // Set up:
            string pathOfTestFolder = TestingTools.CreateTestFolder("File_Integrity_Utility_Test_Folder");
            string[] listOfTestFileOriginalNames = CreateMultipleTestFilesWithDifferentContents(pathOfTestFolder, 5);
            string[] listOfTestFileNewNames = GetListOfTestFileExpectedNewNames(pathOfTestFolder, listOfTestFileOriginalNames);
            StringWriter consoleOutput = TestingTools.RerouteConsoleOutput();

            // Test:
            LoadConsoleInputAndRunMethod(pathOfTestFolder);

            // Assert:
            string expectedDisplayedOutput = "Please enter the full path of the folder to analyze: \n" +
                                             "Renaming all top level files in " + pathOfTestFolder + "...\r\n";
            for (int currentFileNumber = 0; currentFileNumber < listOfTestFileOriginalNames.Length; ++currentFileNumber)
            {
                expectedDisplayedOutput += listOfTestFileOriginalNames[currentFileNumber] + " -> " + listOfTestFileNewNames[currentFileNumber] + "\r\n";
            }
            expectedDisplayedOutput += "\nRenaming files complete.";
            string actualDisplayedOutput = consoleOutput.ToString().Trim();
            Assert.AreEqual(expectedDisplayedOutput, actualDisplayedOutput);

            // Tear down:
            Directory.Delete(pathOfTestFolder, true);
        }


        private string[] CreateMultipleTestFilesWithDifferentContents(string pathOfTestFolder, int numberOfTestFilesToCreate)
        {
            string[] listOfTestFileOriginalNames = new string[numberOfTestFilesToCreate];
            for (int currentTestFileNumber = 0; currentTestFileNumber < numberOfTestFilesToCreate; ++currentTestFileNumber)
            {
                string nameOfCurrentTestFile = "File_Integrity_Utility_Test_File" + currentTestFileNumber + ".txt";
                TestingTools.CreateNewTestFile(pathOfTestFolder, nameOfCurrentTestFile);
                listOfTestFileOriginalNames[currentTestFileNumber] = nameOfCurrentTestFile;
            }
            return listOfTestFileOriginalNames;
        }


        [TestMethod()]
        [Timeout(5000)]
        public void RenameAllTopLevelFilesInGivenFolderAsTheirHash_GivenValidFolderContainingMultipleFilesWithDifferentHashes_AllFilesRenamed()
        {
            // Set up:
            string pathOfTestFolder = TestingTools.CreateTestFolder("File_Integrity_Utility_Test_Folder");
            string[] listOfTestFileOriginalNames = CreateMultipleTestFilesWithDifferentContents(pathOfTestFolder, 5);
            string[] listOfTestFileNewNames = GetListOfTestFileExpectedNewNames(pathOfTestFolder, listOfTestFileOriginalNames);

            // Test:
            LoadConsoleInputAndRunMethod(pathOfTestFolder);

            // Assert:
            for (int currentFileNumber = 0; currentFileNumber < listOfTestFileOriginalNames.Length; ++currentFileNumber)
            {
                string currentFileOriginalPath = pathOfTestFolder + Path.DirectorySeparatorChar + listOfTestFileOriginalNames[currentFileNumber];
                string currentFileNewPath = pathOfTestFolder + Path.DirectorySeparatorChar + listOfTestFileNewNames[currentFileNumber];
                AssertThatFileExistsAtFirstPathAndNotAtSecondPath(currentFileNewPath, currentFileOriginalPath);
            }

            // Tear down:
            Directory.Delete(pathOfTestFolder, true);
        }
    }
}