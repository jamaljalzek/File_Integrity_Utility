using File_Integrity_Utility.ProgramFiles.MenuOptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace File_Integrity_Utility.ProgramFiles.MenuOptions_Tests
{
    [TestClass()]
    public class MenuOption1_Tests
    {
        [TestMethod()]
        [Timeout(5000)]
        public void DisplayFileNameFollowedByItsHash_ForExistingFile_DisplayCorrectFileHash()
        {
            // Set up:
            string nameOfTestFile = "File_Integrity_Utility_Test_File.txt";
            string pathOfTestFile = Path.GetTempPath() + Path.DirectorySeparatorChar + nameOfTestFile;
            CreateNewTestFile(pathOfTestFile);
            string correctHashOfTestFile = HashingTools.ObtainFileHash(pathOfTestFile);
            StringWriter consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Test:
            // The user is prompted to enter into the console the path of the file to hash.
            // We pre-input this path into the console so that it will be detected immediately after the prompt:
            StringReader consoleInput = new StringReader(pathOfTestFile);
            Console.SetIn(consoleInput);
            MenuOption1.DisplayFileNameFollowedByItsHash();

            // Check:
            string expectedDisplayedOutput = "Please enter the full path of the file to analyze: \n" +
                                    "Generating hash for given file...\r\n" +
                                    "File hash complete.\n\r\n" +
                                    nameOfTestFile + "\r\n" +
                                    correctHashOfTestFile;
            string actualDisplayedOutput = consoleOutput.ToString().Trim();
            Assert.AreEqual(expectedDisplayedOutput, actualDisplayedOutput);

            // Clean up:
            File.Delete(pathOfTestFile);
        }


        private void CreateNewTestFile(string pathOfFileToCreate)
        {
            Random random = new Random();
            StreamWriter textFileToWriteTo = File.CreateText(pathOfFileToCreate);
            for (int currentCharNumber = 0; currentCharNumber < 1024; ++currentCharNumber)
            {
                textFileToWriteTo.Write(random.Next(10));
            }
            textFileToWriteTo.Close();
        }


        [TestMethod()]
        public void DisplayFileNameFollowedByItsHash_ForNonExistingFile_DisplayErrorMessage()
        {
            // The way that the method is designed, it will not attempt to hash any file that has not already been proven to exist via "ConsoleTools.ObtainFilePathFromUser()".
            // The latter method is tested elsewhere.
        }
    }
}