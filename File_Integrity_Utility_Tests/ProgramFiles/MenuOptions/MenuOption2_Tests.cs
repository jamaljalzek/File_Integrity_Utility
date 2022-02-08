﻿using File_Integrity_Utility.ProgramFiles.MenuOptions;
using File_Integrity_Utility_Tests.ProgramFiles.MenuOptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace File_Integrity_Utility.ProgramFiles.MenuOptions_Tests
{
    [TestClass()]
    public class MenuOption2_Tests
    {
        [TestMethod()]
        [Timeout(5000)]
        public void DisplayIfFileHashMatchesProvidedHash_ForExistingFileAndCorrectHash_DisplayEquivalent()
        {
            // Set up:
            string nameOfTestFile = "File_Integrity_Utility_Test_File.txt";
            string pathOfTestFile = Path.GetTempPath() + Path.DirectorySeparatorChar + nameOfTestFile;
            TestingTools.CreateNewTestFile(pathOfTestFile);
            string correctHashOfTestFile = HashingTools.ObtainFileHash(pathOfTestFile);
            StringWriter consoleOutput = TestingTools.RerouteConsoleOutput();

            // Test:
            // The user is prompted to enter into the console the path of the file to hash.
            // Then, after entering in a valid path, the user is prompted to enter in a hash to compare against.
            // We pre-input these into the console so that it will be detected immediately after each prompt:
            TestingTools.PreloadInputToConsole(pathOfTestFile + "\n" + correctHashOfTestFile);
            MenuOption2.DisplayIfFileHashMatchesProvidedHash();

            // Check:
            string expectedDisplayedOutput = "Please enter the full path of the file to analyze: \n" +
                                             "Generating hash for given file...\r\n" +
                                             "File hash complete.\n\r\n" +
                                             "Please enter the full SHA 256 hash (case-insensitive) to compare against the given file: \n" +
                                             "Verdict:\r\n" +
                                             "Equivalent";
            string actualDisplayedOutput = consoleOutput.ToString().Trim();
            Assert.AreEqual(expectedDisplayedOutput, actualDisplayedOutput);

            // Clean up:
            File.Delete(pathOfTestFile);
        }


        [TestMethod()]
        [Timeout(5000)]
        public void DisplayIfFileHashMatchesProvidedHash_ForExistingFileAndIncorrectHash_DisplayNotEquivalent()
        {
            // Set up:
            string nameOfTestFile = "File_Integrity_Utility_Test_File.txt";
            string pathOfTestFile = Path.GetTempPath() + Path.DirectorySeparatorChar + nameOfTestFile;
            TestingTools.CreateNewTestFile(pathOfTestFile);
            string incorrectHashOfTestFile = HashingTools.ObtainFileHash(pathOfTestFile) + "blah";
            StringWriter consoleOutput = TestingTools.RerouteConsoleOutput();

            // Test:
            // The user is prompted to enter into the console the path of the file to hash.
            // Then, after entering in a valid path, the user is prompted to enter in a hash to compare against.
            // We pre-input these into the console so that it will be detected immediately after each prompt:
            TestingTools.PreloadInputToConsole(pathOfTestFile + "\n" + incorrectHashOfTestFile);
            MenuOption2.DisplayIfFileHashMatchesProvidedHash();

            // Check:
            string expectedDisplayedOutput = "Please enter the full path of the file to analyze: \n" +
                                             "Generating hash for given file...\r\n" +
                                             "File hash complete.\n\r\n" +
                                             "Please enter the full SHA 256 hash (case-insensitive) to compare against the given file: \n" +
                                             "Verdict:\r\n" +
                                             "Not equivalent";
            string actualDisplayedOutput = consoleOutput.ToString().Trim();
            Assert.AreEqual(expectedDisplayedOutput, actualDisplayedOutput);

            // Clean up:
            File.Delete(pathOfTestFile);
        }


        [TestMethod()]
        public void DisplayIfFileHashMatchesProvidedHash_ForNonExistingFile_DisplayErrorMessage()
        {
            // The way that the method is designed, it will not attempt to hash any file that has not already been proven to exist via "ConsoleTools.ObtainFilePathFromUser()".
            // The latter method is tested elsewhere.
        }
    }
}