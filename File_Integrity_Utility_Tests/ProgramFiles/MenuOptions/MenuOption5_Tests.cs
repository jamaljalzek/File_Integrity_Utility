using File_Integrity_Utility.ProgramFiles.MenuOptions;
using File_Integrity_Utility_Tests.ProgramFiles.MenuOptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace File_Integrity_Utility.ProgramFiles.MenuOptions_Tests
{
    public class MenuOption5_Class_Tests
    {
        [TestClass()]
        public class CompareNameOfEachTopLevelFileInGivenFolderToItsHash_Method_Tests
        {
            private const string NAME_OF_TEST_FOLDER = "File_Integrity_Utility_Test_Folder";
            private const string NAME_OF_TEST_FILE = "File_Integrity_Utility_Test_File.txt";


            /*
             * Tests:
             * 1. Given an empty folder.
             * 2. Given a folder with 1 file, where its name matches its hash.
             * 3. Given a folder with 1 file, where its name does not match its hash.
             * 
             * 4. Given a folder with multiple files, where each one's name matches its hash.
             * 5. Given a folder with multiple files, where for some files its name matches its hash, and some do not.
             * 6. Given a folder with multiple files, where each one's name does not match its hash.
             */
            [TestMethod()]
            [Timeout(TestingTools.ONE_SECOND)]
            public void GivenEmptyFolder_DisplayNoInconsistenciesFound()
            {
                // Set up:
                string pathOfTestFolder = TestingTools.CreateTestFolder(NAME_OF_TEST_FOLDER);
                StringWriter consoleOutput = TestingTools.RerouteConsoleOutput();

                // Execute:
                LoadConsoleInputAndRunMethod(pathOfTestFolder);

                // Assert:
                string expectedDisplayedOutput = ReturnEnterFolderPathPromptAndComparingFiles(pathOfTestFolder) + "\n";
                expectedDisplayedOutput += "Comparing files complete.\r\n\n" +
                                           "Verdict:\r\n" +
                                           "No inconsistency(s) found";
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
                MenuOption5.CompareNameOfEachTopLevelFileInGivenFolderToItsHash();
            }


            private string ReturnEnterFolderPathPromptAndComparingFiles(string pathOfTestFolder)
            {
                return "Please enter the full path of the folder to analyze: \n" +
                       "Comparing the name of each top level file in " + pathOfTestFolder + "to its hash...\r\n";
            }


            [TestMethod()]
            [Timeout(TestingTools.ONE_SECOND)]
            public void GivenFolderContainingOneFileWhoseNameMatchesItsHash_DisplayNoInconsistenciesFound()
            {
                // Set up:
                string pathOfTestFolder = TestingTools.CreateTestFolder(NAME_OF_TEST_FOLDER);
                string pathOfTestFile = TestingTools.CreateNewTestFile(pathOfTestFolder, NAME_OF_TEST_FILE);
                string hashOfTestFile = HashingTools.ObtainFileHash(pathOfTestFile);
                RenameAllTopLevelFilesInFolderAsTheirHash(pathOfTestFolder);
                StringWriter consoleOutput = TestingTools.RerouteConsoleOutput();

                // Execute:
                LoadConsoleInputAndRunMethod(pathOfTestFolder);

                // Assert:
                string expectedDisplayedOutput = ReturnEnterFolderPathPromptAndComparingFiles(pathOfTestFolder);
                expectedDisplayedOutput += hashOfTestFile + ".txt = " + hashOfTestFile + ".txt\r\n\n" +
                                           "Comparing files complete.\r\n\n" +
                                           "Verdict:\r\n" +
                                           "No inconsistency(s) found";
                string actualDisplayedOutput = consoleOutput.ToString().Trim();
                Assert.AreEqual(expectedDisplayedOutput, actualDisplayedOutput);

                // Tear down:
                Directory.Delete(pathOfTestFolder, true);
            }


            private void RenameAllTopLevelFilesInFolderAsTheirHash(string pathOfTestFolder)
            {
                // The user is prompted to enter into the console the path of the folder.
                // We pre-input this into the console so that it will be detected immediately after the prompt:
                TestingTools.PreloadInputToConsole(pathOfTestFolder);
                MenuOption4.RenameAllTopLevelFilesInGivenFolderAsTheirHash();
            }


            [TestMethod()]
            [Timeout(TestingTools.ONE_SECOND)]
            public void GivenFolderContainingOneFileWhoseNameDoesNotMatchItsHash_DisplayInconsistenciesFound()
            {
                // Set up:
                string pathOfTestFolder = TestingTools.CreateTestFolder(NAME_OF_TEST_FOLDER);
                string pathOfTestFile = TestingTools.CreateNewTestFile(pathOfTestFolder, NAME_OF_TEST_FILE);
                string originalHashOfTestFile = HashingTools.ObtainFileHash(pathOfTestFile);
                RenameAllTopLevelFilesInFolderAsTheirHash(pathOfTestFolder);
                // We modify the test file so that its new hash will be different:
                string newPathOfTestFile = pathOfTestFolder + Path.DirectorySeparatorChar + originalHashOfTestFile + ".txt";
                File.AppendAllText(newPathOfTestFile, "blah");
                string newHashOfTestFile = HashingTools.ObtainFileHash(newPathOfTestFile);
                StringWriter consoleOutput = TestingTools.RerouteConsoleOutput();

                // Execute:
                LoadConsoleInputAndRunMethod(pathOfTestFolder);

                // Assert:
                string expectedDisplayedOutput = ReturnEnterFolderPathPromptAndComparingFiles(pathOfTestFolder);
                expectedDisplayedOutput += originalHashOfTestFile + ".txt != " + newHashOfTestFile + ".txt\r\n\n" +
                                           "Comparing files complete.\r\n\n" +
                                           "Verdict:\r\n" +
                                           "Inconsistency(s) found";
                string actualDisplayedOutput = consoleOutput.ToString().Trim();
                Assert.AreEqual(expectedDisplayedOutput, actualDisplayedOutput);

                // Tear down:
                Directory.Delete(pathOfTestFolder, true);
            }
        }
    }
}