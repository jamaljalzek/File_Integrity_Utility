using File_Integrity_Utility.ProgramFiles.MenuOptions;
using File_Integrity_Utility_Tests.ProgramFiles.MenuOptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace File_Integrity_Utility.ProgramFiles.MenuOptions_Tests
{
    [TestClass()]
    public class MenuOption3_Tests
    {
        [TestMethod()]
        [Timeout(5000)]
        public void DisplayIfBothFilesAreIdentical_ComparingAFileAgainstItself_DisplayEquivalent()
        {
            // Set up:
            string nameOfTestFile = "File_Integrity_Utility_Test_File.txt";
            string pathOfTestFile = Path.GetTempPath() + Path.DirectorySeparatorChar + nameOfTestFile;
            TestingTools.CreateNewTestFile(pathOfTestFile);
            StringWriter consoleOutput = TestingTools.RerouteConsoleOutput();

            // Test:
            // First, the user is prompted to enter into the console the path of the first file.
            // Then, after entering in a valid path, the user is prompted to enter into the console the path of the second file.
            // We pre-input these into the console so that it will be detected immediately after each prompt:
            TestingTools.PreloadInputToConsole(pathOfTestFile + "\n" + pathOfTestFile);
            MenuOption3.DisplayIfBothFilesAreIdentical();

            // Check:
            string expectedDisplayedOutput = "Please enter the full path of the first file to analyze: \n" +
                                             "Generating hash for given file...\r\n" +
                                             "File hash complete.\n\r\n" +
                                             "Please enter the full path of the second file to analyze: \n" +
                                             "Generating hash for given file...\r\n" +
                                             "File hash complete.\n\r\n\n" +
                                             "Verdict:\r\n" +
                                             "Equivalent";
            string actualDisplayedOutput = consoleOutput.ToString().Trim();
            Assert.AreEqual(expectedDisplayedOutput, actualDisplayedOutput);

            // Clean up:
            File.Delete(pathOfTestFile);
        }


        [TestMethod()]
        [Timeout(5000)]
        public void DisplayIfBothFilesAreIdentical_ComparingAFileAgainstItsCopy_DisplayEquivalent()
        {
            // Set up:
            string nameOfOriginalTestFile = "File_Integrity_Utility_Original_Test_File.txt";
            string pathOfOriginalTestFile = Path.GetTempPath() + Path.DirectorySeparatorChar + nameOfOriginalTestFile;
            TestingTools.CreateNewTestFile(pathOfOriginalTestFile);
            string nameOfCopyTestFile = "File_Integrity_Utility_Copy_Test_File.txt";
            string pathOfCopyTestFile = Path.GetTempPath() + Path.DirectorySeparatorChar + nameOfCopyTestFile;
            File.Copy(pathOfOriginalTestFile, pathOfCopyTestFile, true);
            StringWriter consoleOutput = TestingTools.RerouteConsoleOutput();

            // Test:
            // First, the user is prompted to enter into the console the path of the first file.
            // Then, after entering in a valid path, the user is prompted to enter into the console the path of the second file.
            // We pre-input these into the console so that it will be detected immediately after each prompt:
            TestingTools.PreloadInputToConsole(pathOfOriginalTestFile + "\n" + pathOfCopyTestFile);
            MenuOption3.DisplayIfBothFilesAreIdentical();

            // Check:
            string expectedDisplayedOutput = "Please enter the full path of the first file to analyze: \n" +
                                             "Generating hash for given file...\r\n" +
                                             "File hash complete.\n\r\n" +
                                             "Please enter the full path of the second file to analyze: \n" +
                                             "Generating hash for given file...\r\n" +
                                             "File hash complete.\n\r\n\n" +
                                             "Verdict:\r\n" +
                                             "Equivalent";
            string actualDisplayedOutput = consoleOutput.ToString().Trim();
            Assert.AreEqual(expectedDisplayedOutput, actualDisplayedOutput);

            // Clean up:
            File.Delete(pathOfOriginalTestFile);
            File.Delete(pathOfCopyTestFile);
        }


        [TestMethod()]
        [Timeout(5000)]
        public void DisplayIfBothFilesAreIdentical_ComparingNonIdenticalFiles_DisplayNotEquivalent()
        {
            // Set up:
            string nameOfFirstTestFile = "File_Integrity_Utility_First_Test_File.txt";
            string pathOfFirstTestFile = Path.GetTempPath() + Path.DirectorySeparatorChar + nameOfFirstTestFile;
            TestingTools.CreateNewTestFile(pathOfFirstTestFile);
            string nameOfSecondTestFile = "File_Integrity_Utility_Second_Test_File.txt";
            string pathOfSecondTestFile = Path.GetTempPath() + Path.DirectorySeparatorChar + nameOfSecondTestFile;
            File.Copy(pathOfFirstTestFile, pathOfSecondTestFile, true);
            File.AppendAllText(pathOfSecondTestFile, "blah");
            StringWriter consoleOutput = TestingTools.RerouteConsoleOutput();

            // Test:
            // First, the user is prompted to enter into the console the path of the first file.
            // Then, after entering in a valid path, the user is prompted to enter into the console the path of the second file.
            // We pre-input these into the console so that it will be detected immediately after each prompt:
            TestingTools.PreloadInputToConsole(pathOfFirstTestFile + "\n" + pathOfSecondTestFile);
            MenuOption3.DisplayIfBothFilesAreIdentical();

            // Check:
            string expectedDisplayedOutput = "Please enter the full path of the first file to analyze: \n" +
                                             "Generating hash for given file...\r\n" +
                                             "File hash complete.\n\r\n" +
                                             "Please enter the full path of the second file to analyze: \n" +
                                             "Generating hash for given file...\r\n" +
                                             "File hash complete.\n\r\n\n" +
                                             "Verdict:\r\n" +
                                             "Not equivalent";
            string actualDisplayedOutput = consoleOutput.ToString().Trim();
            Assert.AreEqual(expectedDisplayedOutput, actualDisplayedOutput);

            // Clean up:
            File.Delete(pathOfFirstTestFile);
            File.Delete(pathOfSecondTestFile);
        }


        [TestMethod()]
        public void DisplayIfBothFilesAreIdentical_ForNonExistingFile_DisplayErrorMessage()
        {
            // The way that the method is designed, it will not attempt to hash any files that has not already been proven to exist via "ConsoleTools.ObtainFilePathFromUser()".
            // The latter method is tested elsewhere.
        }
    }
}