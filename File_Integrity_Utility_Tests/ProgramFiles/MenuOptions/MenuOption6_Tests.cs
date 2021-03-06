using File_Integrity_Utility.ProgramFiles.MenuOptions;
using File_Integrity_Utility_Tests.ProgramFiles.MenuOptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace File_Integrity_Utility.ProgramFiles.MenuOptions_Tests
{
    public class MenuOption6_Class_Tests
    {
        [TestClass()]
        public class GenerateTextFileListingFileNamesToHashes_Method_Tests
        {
            private const string NAME_OF_TEST_FOLDER = "File_Integrity_Utility_Test_Folder";
            private const string ENTER_FOLDER_PATH_PROMPT = "Please enter the full path of the folder to analyze: \n";


            [TestMethod()]
            [Timeout(TestingTools.ONE_SECOND)]
            public void GivenEmptyFolder_DisplayHashesFileNotCreated()
            {
                // Set up:
                string pathOfTestFolder = TestingTools.CreateTestFolder(NAME_OF_TEST_FOLDER);
                StringWriter consoleOutput = TestingTools.RerouteConsoleOutput();

                // Execute:
                LoadConsoleInputAndRunMethod(pathOfTestFolder);

                // Assert:
                string expectedDisplayedOutput = ENTER_FOLDER_PATH_PROMPT + "\n" +
                                                 "Error, given folder contains no top level files: hashes file not created.";
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
                MenuOption6.GenerateTextFileListingFileNamesToHashes();
            }


            [TestMethod()]
            [Timeout(TestingTools.ONE_SECOND)]
            public void GivenEmptyFolder_HashesFileNotCreated()
            {
                // Set up:
                string pathOfTestFolder = TestingTools.CreateTestFolder(NAME_OF_TEST_FOLDER);

                // Execute:
                LoadConsoleInputAndRunMethod(pathOfTestFolder);

                // Assert:
                string pathOfHashesFile = pathOfTestFolder + Path.DirectorySeparatorChar + "File SHA 256 Hashes.txt";
                bool doesHashesFileExist = File.Exists(pathOfHashesFile);
                Assert.IsFalse(doesHashesFileExist);

                // Tear down:
                Directory.Delete(pathOfTestFolder, true);
            }


            [TestMethod()]
            [Timeout(TestingTools.ONE_SECOND)]
            public void GivenFolderContainingOneFile_DisplayHashesFileCreated()
            {
                // Set up:
                string pathOfTestFolder = TestingTools.CreateTestFolder(NAME_OF_TEST_FOLDER);
                string pathOfTestFile = TestingTools.CreateNewTestFile(pathOfTestFolder, "File_Integrity_Utility_Test_File.txt");
                StringWriter consoleOutput = TestingTools.RerouteConsoleOutput();

                // Execute:
                LoadConsoleInputAndRunMethod(pathOfTestFolder);

                // Assert:
                string expectedDisplayedOutput = ENTER_FOLDER_PATH_PROMPT +
                                                 "Generating hashes for all files in " + pathOfTestFolder + "...\r\n" +
                                                 pathOfTestFile + "... DONE\r\n" +
                                                 "All file hashes generated.\r\n\n" +
                                                 "Writing file hashes to text file...\r\n" +
                                                 "All file hashes written to text file.";
                string actualDisplayedOutput = consoleOutput.ToString().Trim();
                Assert.AreEqual(expectedDisplayedOutput, actualDisplayedOutput);

                // Tear down:
                Directory.Delete(pathOfTestFolder, true);
            }


            [TestMethod()]
            [Timeout(TestingTools.ONE_SECOND)]
            public void GivenFolderContainingOneFile_HashesFileCreated()
            {
                // Set up:
                string pathOfTestFolder = TestingTools.CreateTestFolder(NAME_OF_TEST_FOLDER);
                string nameOfTestFile = "File_Integrity_Utility_Test_File.txt";
                string pathOfTestFile = TestingTools.CreateNewTestFile(pathOfTestFolder, nameOfTestFile);

                // Execute:
                LoadConsoleInputAndRunMethod(pathOfTestFolder);

                // Assert:
                string pathOfHashesFile = pathOfTestFolder + Path.DirectorySeparatorChar + "File SHA 256 Hashes.txt";
                StreamReader hashesFileReader = File.OpenText(pathOfHashesFile);
                string? hashesFileFirstLine = hashesFileReader.ReadLine();
                Assert.AreEqual(nameOfTestFile, hashesFileFirstLine);
                string hashOfTestFile = HashingTools.ObtainFileHash(pathOfTestFile);
                string? hashesFileSecondLine = hashesFileReader.ReadLine();
                Assert.AreEqual(hashOfTestFile, hashesFileSecondLine);

                // Tear down:
                hashesFileReader.Close();
                Directory.Delete(pathOfTestFolder, true);
            }


            [TestMethod()]
            [Timeout(TestingTools.ONE_SECOND)]
            public void GivenFolderContainingOneFileAndHashesFile_DisplayHashesFileCreated()
            {
                // Set up:
                string pathOfTestFolder = TestingTools.CreateTestFolder(NAME_OF_TEST_FOLDER);
                string pathOfTestFile = TestingTools.CreateNewTestFile(pathOfTestFolder, "File_Integrity_Utility_Test_File.txt");
                CreateDummyHashesFileMeantToBeReplaced(pathOfTestFolder);
                StringWriter consoleOutput = TestingTools.RerouteConsoleOutput();

                // Execute:
                LoadConsoleInputAndRunMethod(pathOfTestFolder);

                // Assert:
                string expectedDisplayedOutput = ENTER_FOLDER_PATH_PROMPT +
                                                 "Generating hashes for all files in " + pathOfTestFolder + "...\r\n" +
                                                 pathOfTestFile + "... DONE\r\n" +
                                                 "All file hashes generated.\r\n\n" +
                                                 "Writing file hashes to text file...\r\n" +
                                                 "All file hashes written to text file.";
                string actualDisplayedOutput = consoleOutput.ToString().Trim();
                Assert.AreEqual(expectedDisplayedOutput, actualDisplayedOutput);

                // Tear down:
                Directory.Delete(pathOfTestFolder, true);
            }


            private static void CreateDummyHashesFileMeantToBeReplaced(string pathOfTestFolder)
            {
                // This is meant to be a placeholder hashes file, which should be replaced when the method we are testing is called.
                TestingTools.CreateNewTestFile(pathOfTestFolder, "File SHA 256 Hashes.txt");
            }


            [TestMethod()]
            [Timeout(TestingTools.ONE_SECOND)]
            public void GivenFolderContainingOneFileAndHashesFile_OldHashesFileReplacedAndUpdated()
            {
                // Set up:
                string pathOfTestFolder = TestingTools.CreateTestFolder(NAME_OF_TEST_FOLDER);
                string nameOfTestFile = "File_Integrity_Utility_Test_File.txt";
                string pathOfTestFile = TestingTools.CreateNewTestFile(pathOfTestFolder, nameOfTestFile);
                CreateDummyHashesFileMeantToBeReplaced(pathOfTestFolder);

                // Execute:
                LoadConsoleInputAndRunMethod(pathOfTestFolder);

                // Assert:
                string pathOfHashesFile = pathOfTestFolder + Path.DirectorySeparatorChar + "File SHA 256 Hashes.txt";
                StreamReader hashesFileReader = File.OpenText(pathOfHashesFile);
                string? hashesFileFirstLine = hashesFileReader.ReadLine();
                Assert.AreEqual(nameOfTestFile, hashesFileFirstLine);
                string hashOfTestFile = HashingTools.ObtainFileHash(pathOfTestFile);
                string? hashesFileSecondLine = hashesFileReader.ReadLine();
                Assert.AreEqual(hashOfTestFile, hashesFileSecondLine);

                // Tear down:
                hashesFileReader.Close();
                Directory.Delete(pathOfTestFolder, true);
            }


            [TestMethod()]
            [Timeout(TestingTools.ONE_SECOND)]
            public void GivenFolderContainingTwoFilesWithSameHash_DisplayHashesFileCreated()
            {
                // Set up:
                string pathOfTestFolder = TestingTools.CreateTestFolder(NAME_OF_TEST_FOLDER);
                string pathOfOriginalTestFile = TestingTools.CreateNewTestFile(pathOfTestFolder, "File_Integrity_Utility_Original_Test_File.txt");
                string pathOfCopyTestFile = pathOfTestFolder + Path.DirectorySeparatorChar + "File_Integrity_Utility_Copy_Test_File.txt";
                File.Copy(pathOfOriginalTestFile, pathOfCopyTestFile, true);
                StringWriter consoleOutput = TestingTools.RerouteConsoleOutput();

                // Execute:
                LoadConsoleInputAndRunMethod(pathOfTestFolder);

                // Assert:
                string expectedDisplayedOutput = ENTER_FOLDER_PATH_PROMPT +
                                                 "Generating hashes for all files in " + pathOfTestFolder + "...\r\n" +
                                                 pathOfCopyTestFile + "... DONE\r\n" +
                                                 pathOfOriginalTestFile + "... DONE\r\n" +
                                                 "All file hashes generated.\r\n\n" +
                                                 "Writing file hashes to text file...\r\n" +
                                                 "All file hashes written to text file.";
                string actualDisplayedOutput = consoleOutput.ToString().Trim();
                Assert.AreEqual(expectedDisplayedOutput, actualDisplayedOutput);

                // Tear down:
                Directory.Delete(pathOfTestFolder, true);
            }


            [TestMethod()]
            [Timeout(TestingTools.ONE_SECOND)]
            public void GivenFolderContainingTwoFilesWithSameHash_HashesFileCreated()
            {
                // Set up:
                string pathOfTestFolder = TestingTools.CreateTestFolder(NAME_OF_TEST_FOLDER);
                string nameOfOriginalTestFile = "File_Integrity_Utility_Original_Test_File.txt";
                string pathOfOriginalTestFile = TestingTools.CreateNewTestFile(pathOfTestFolder, nameOfOriginalTestFile);
                string nameOfCopyTestFile = "File_Integrity_Utility_Copy_Test_File.txt";
                string pathOfCopyTestFile = pathOfTestFolder + Path.DirectorySeparatorChar + nameOfCopyTestFile;
                File.Copy(pathOfOriginalTestFile, pathOfCopyTestFile, true);

                // Execute:
                LoadConsoleInputAndRunMethod(pathOfTestFolder);

                // Assert:
                string pathOfHashesFile = pathOfTestFolder + Path.DirectorySeparatorChar + "File SHA 256 Hashes.txt";
                StreamReader hashesFileReader = File.OpenText(pathOfHashesFile);
                string hashOfBothTestFiles = HashingTools.ObtainFileHash(pathOfOriginalTestFile);
                AssertNextTwoLinesInHashesFileMatchFileNameAndHash(hashesFileReader, nameOfCopyTestFile, hashOfBothTestFiles);
                // Each [file name, file hash] entry in the text file is spaced out with an empty line, so we must skip it:
                hashesFileReader.ReadLine();
                AssertNextTwoLinesInHashesFileMatchFileNameAndHash(hashesFileReader, nameOfOriginalTestFile, hashOfBothTestFiles);

                // Tear down:
                hashesFileReader.Close();
                Directory.Delete(pathOfTestFolder, true);
            }


            private void AssertNextTwoLinesInHashesFileMatchFileNameAndHash(StreamReader hashesFileReader, string nameOfFile, string hashOfFile)
            {
                string? hashesFileFirstLine = hashesFileReader.ReadLine();
                Assert.AreEqual(nameOfFile, hashesFileFirstLine);
                string? hashesFileSecondLine = hashesFileReader.ReadLine();
                Assert.AreEqual(hashOfFile, hashesFileSecondLine);
            }


            [TestMethod()]
            [Timeout(TestingTools.ONE_SECOND)]
            public void GivenFolderContainingMultipleFilesWithDifferentHashes_DisplayHashesFileCreated()
            {
                // Set up:
                string pathOfTestFolder = TestingTools.CreateTestFolder(NAME_OF_TEST_FOLDER);
                string[] listOfTestFileNames = TestingTools.CreateMultipleTestFilesWithDifferentContents(pathOfTestFolder, 5);
                StringWriter consoleOutput = TestingTools.RerouteConsoleOutput();

                // Execute:
                LoadConsoleInputAndRunMethod(pathOfTestFolder);

                // Assert:
                string expectedDisplayedOutput = ENTER_FOLDER_PATH_PROMPT +
                                                 "Generating hashes for all files in " + pathOfTestFolder + "...\r\n";
                foreach (string currentFileName in listOfTestFileNames)
                {
                    expectedDisplayedOutput += pathOfTestFolder + Path.DirectorySeparatorChar + currentFileName + "... DONE\r\n";
                }
                expectedDisplayedOutput += "All file hashes generated.\r\n\n" +
                                           "Writing file hashes to text file...\r\n" +
                                           "All file hashes written to text file.";
                string actualDisplayedOutput = consoleOutput.ToString().Trim();
                Assert.AreEqual(expectedDisplayedOutput, actualDisplayedOutput);

                // Tear down:
                Directory.Delete(pathOfTestFolder, true);
            }


            [TestMethod()]
            [Timeout(TestingTools.ONE_SECOND)]
            public void GivenFolderContainingMultipleFilesWithDifferentHashes_HashesFileCreated()
            {
                // Set up:
                string pathOfTestFolder = TestingTools.CreateTestFolder(NAME_OF_TEST_FOLDER);
                string[] listOfTestFileNames = TestingTools.CreateMultipleTestFilesWithDifferentContents(pathOfTestFolder, 5);

                // Execute:
                LoadConsoleInputAndRunMethod(pathOfTestFolder);

                // Assert:
                string pathOfHashesFile = pathOfTestFolder + Path.DirectorySeparatorChar + "File SHA 256 Hashes.txt";
                StreamReader hashesFileReader = File.OpenText(pathOfHashesFile);
                foreach (string currentFileName in listOfTestFileNames)
                {
                    string currentFilePath = pathOfTestFolder + Path.DirectorySeparatorChar + currentFileName;
                    string currentFileHash = HashingTools.ObtainFileHash(currentFilePath);
                    AssertNextTwoLinesInHashesFileMatchFileNameAndHash(hashesFileReader, currentFileName, currentFileHash);
                    // Each [file name, file hash] entry in the text file is spaced out with an empty line, so we must skip it:
                    hashesFileReader.ReadLine();
                }

                // Tear down:
                hashesFileReader.Close();
                Directory.Delete(pathOfTestFolder, true);
            }
        }
    }
}