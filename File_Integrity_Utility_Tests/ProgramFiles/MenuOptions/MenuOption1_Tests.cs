using File_Integrity_Utility.ProgramFiles.MenuOptions;
using File_Integrity_Utility_Tests.ProgramFiles.MenuOptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace File_Integrity_Utility.ProgramFiles.MenuOptions_Tests
{
    public class MenuOption1_Class_Tests
    {
        [TestClass()]
        public class DisplayFileNameFollowedByItsHash_Method_Tests
        {
            [TestMethod()]
            [Timeout(TestingTools.ONE_SECOND)]
            public void GivenExistingFile_DisplayCorrectFileHash()
            {
                // Set up:
                string pathOfTestFile = TestingTools.CreateNewTestFile(Path.GetTempPath(), "File_Integrity_Utility_Test_File.txt");
                StringWriter consoleOutput = TestingTools.RerouteConsoleOutput();

                // Execute:
                LoadConsoleInputAndRunMethod(pathOfTestFile);

                // Assert:
                string correctHashOfTestFile = HashingTools.ObtainFileHash(pathOfTestFile);
                string expectedDisplayedOutput = ReturnEnterFilePathPrompt();
                expectedDisplayedOutput += Path.GetFileName(pathOfTestFile) + "\r\n" +
                                           correctHashOfTestFile;
                string actualDisplayedOutput = consoleOutput.ToString().Trim();
                Assert.AreEqual(expectedDisplayedOutput, actualDisplayedOutput);

                // Tear down:
                File.Delete(pathOfTestFile);
            }


            private void LoadConsoleInputAndRunMethod(string consoleInput)
            {
                // The user is prompted to enter into the console the path of the file to hash.
                // We pre-input this path into the console so that it will be detected immediately after the prompt:
                TestingTools.PreloadInputToConsole(consoleInput);
                MenuOption1.DisplayFileNameFollowedByItsHash();
            }


            private string ReturnEnterFilePathPrompt()
            {
                return "Please enter the full path of the file to analyze: \n" +
                       "Generating hash for given file...\r\n" +
                       "File hash complete.\n\r\n";
            }
        }
    }
}