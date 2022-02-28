using File_Integrity_Utility.ProgramFiles.MenuOptions;
using File_Integrity_Utility_Tests.ProgramFiles.MenuOptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace File_Integrity_Utility.ProgramFiles.MenuOptions_Tests
{
    [TestClass()]
    public class MenuOption6_Tests
    {
        private string NameOfTestFolder = "File_Integrity_Utility_Test_Folder";
        private string EnterFolderPathPrompt = "Please enter the full path of the folder to analyze: \n";


        [TestMethod()]
        [Timeout(TestingTools.ONE_SECOND)]
        public void CompareHashesOfFilesInFolderToRecordedHashesInTextFile_GivenFolderWithNoHashesFile_DisplayNoHashesFileFound()
        {
            // Set up:
            string pathOfTestFolder = TestingTools.CreateTestFolder(NameOfTestFolder);
            StringWriter consoleOutput = TestingTools.RerouteConsoleOutput();

            // Execute:
            LoadConsoleInputAndRunMethod(pathOfTestFolder);

            // Assert:
            string expectedDisplayedOutput = EnterFolderPathPrompt +
                                             "Error: 'File SHA 256 Hashes.txt' was not found in the given folder.";
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
            MenuOption6.CompareHashesOfFilesInFolderToRecordedHashesInTextFile();
        }


        [TestMethod()]
        [Timeout(TestingTools.ONE_SECOND)]
        public void CompareHashesOfFilesInFolderToRecordedHashesInTextFile_GivenFolderWithEmptyHashesFileAndNoOtherFiles_DisplayNoInconsistenciesFound()
        {
            // Set up:
            string pathOfTestFolder = TestingTools.CreateTestFolder(NameOfTestFolder);
            string pathOfEmptyHashesFile = TestingTools.CreateNewTestFile(pathOfTestFolder, "File SHA 256 Hashes.txt");
            StringWriter consoleOutput = TestingTools.RerouteConsoleOutput();

            // Execute:
            LoadConsoleInputAndRunMethod(pathOfTestFolder);

            // Assert:
            string expectedDisplayedOutput = EnterFolderPathPrompt;
            expectedDisplayedOutput += ReturnDisplayedOutputWhenGeneratingHashesForListOfFiles(null, pathOfEmptyHashesFile, pathOfTestFolder);
            expectedDisplayedOutput += ReturnDisplayedOutputNoInconsistenciesFound();
            string actualDisplayedOutput = consoleOutput.ToString().Trim();
            Assert.AreEqual(expectedDisplayedOutput, actualDisplayedOutput);

            // Tear down:
            Directory.Delete(pathOfTestFolder, true);
        }


        private string ReturnDisplayedOutputWhenGeneratingHashesForListOfFiles(string[]? listOfFileNames, string pathOfHashesFile, string pathOfFolder)
        {
            string displayedOutput = "Generating hashes for all files in " + pathOfFolder + "...\r\n";
            displayedOutput += pathOfHashesFile + "... DONE\r\n";
            if (listOfFileNames != null)
            {
                foreach (string currentFileName in listOfFileNames)
                {
                    displayedOutput += pathOfFolder + Path.DirectorySeparatorChar + currentFileName + "... DONE\r\n";
                }
            }
            displayedOutput += "All file hashes generated.\r\n\n";
            return displayedOutput;
        }


        private string ReturnDisplayedOutputNoInconsistenciesFound()
        {
            return "Comparing file names and hashes with those in text file...\r\n" +
                   "Comparing complete.\r\n\n" +
                   "Verdict:\r\n" +
                   "No inconsistency(s) found";
        }


        [TestMethod()]
        [Timeout(TestingTools.ONE_SECOND)]
        public void CompareHashesOfFilesInFolderToRecordedHashesInTextFile_GivenFolderWithEmptyHashesFileAndOtherFiles_DisplayNoRecordsForFilesFound()
        {
            // Set up:
            string pathOfTestFolder = TestingTools.CreateTestFolder(NameOfTestFolder);
            string pathOfEmptyHashesFile = TestingTools.CreateNewTestFile(pathOfTestFolder, "File SHA 256 Hashes.txt");
            // It does not matter what the contents of the test files are, so we go with the slightly more efficient method below:
            string[] listOfFileNames = TestingTools.CreateMultipleTestFilesWithDifferentContents(pathOfTestFolder, 5);
            StringWriter consoleOutput = TestingTools.RerouteConsoleOutput();

            // Execute:
            LoadConsoleInputAndRunMethod(pathOfTestFolder);

            // Assert:
            string expectedDisplayedOutput = EnterFolderPathPrompt;
            expectedDisplayedOutput += ReturnDisplayedOutputWhenGeneratingHashesForListOfFiles(listOfFileNames, pathOfEmptyHashesFile, pathOfTestFolder);
            expectedDisplayedOutput += "Comparing file names and hashes with those in text file...\r\n";
            foreach (string currentFileName in listOfFileNames)
            {
                expectedDisplayedOutput += "Error: " + currentFileName + " was not found in File SHA 256 Hashes.txt.\r\n";
            }
            expectedDisplayedOutput += ReturnDisplayedOutputInconsistenciesFound();
            string actualDisplayedOutput = consoleOutput.ToString().Trim();
            Assert.AreEqual(expectedDisplayedOutput, actualDisplayedOutput);

            // Tear down:
            Directory.Delete(pathOfTestFolder, true);
        }


        private string ReturnDisplayedOutputInconsistenciesFound()
        {
            return "Comparing complete.\r\n\n" +
                   "Verdict:\r\n" +
                   "Inconsistency(s) found";
        }


        [TestMethod()]
        [Timeout(TestingTools.ONE_SECOND)]
        public void CompareHashesOfFilesInFolderToRecordedHashesInTextFile_GivenFolderWithHashesFileContainingHashesOfNoLongerExistingFiles_DisplayNoInconsistenciesFound()
        {
            // Set up:
            string pathOfTestFolder = TestingTools.CreateTestFolder(NameOfTestFolder);
            // It does not matter what the contents of the test files are, so we go with the slightly more efficient method below:
            string[] listOfFileNames = TestingTools.CreateMultipleTestFilesWithDifferentContents(pathOfTestFolder, 5);
            string pathOfHashesFile = GenerateHashesFile(pathOfTestFolder);
            DeleteFiles(listOfFileNames, pathOfTestFolder);
            StringWriter consoleOutput = TestingTools.RerouteConsoleOutput();

            // Execute:
            LoadConsoleInputAndRunMethod(pathOfTestFolder);

            // Assert:
            string expectedDisplayedOutput = EnterFolderPathPrompt;
            expectedDisplayedOutput += ReturnDisplayedOutputWhenGeneratingHashesForListOfFiles(null, pathOfHashesFile, pathOfTestFolder);
            expectedDisplayedOutput += ReturnDisplayedOutputNoInconsistenciesFound();
            string actualDisplayedOutput = consoleOutput.ToString().Trim();
            Assert.AreEqual(expectedDisplayedOutput, actualDisplayedOutput);

            // Tear down:
            Directory.Delete(pathOfTestFolder, true);
        }


        private string GenerateHashesFile(string pathOfTestFolder)
        {
            // The user is prompted to enter into the console the path of the folder.
            // We pre-input this into the console so that it will be detected immediately after the prompt:
            TestingTools.PreloadInputToConsole(pathOfTestFolder);
            MenuOption5.GenerateTextFileListingFileNamesToHashes();
            return pathOfTestFolder + Path.DirectorySeparatorChar + "File SHA 256 Hashes.txt";
        }


        private void DeleteFiles(string[] listOfFileNames, string pathOfTestFolder)
        {
            foreach (string currentFileName in listOfFileNames)
            {
                string currentFilePath = pathOfTestFolder + Path.DirectorySeparatorChar + currentFileName;
                File.Delete(currentFilePath);
            }
        }


        [TestMethod()]
        [Timeout(TestingTools.ONE_SECOND)]
        public void CompareHashesOfFilesInFolderToRecordedHashesInTextFile_HashesFileContainsOnlyInconsistentFileHashes_DisplayInconsistenciesFound()
        {
            // Set up:
            string pathOfTestFolder = TestingTools.CreateTestFolder(NameOfTestFolder);
            // It does not matter what the contents of the test files are, so we go with the slightly more efficient method below:
            string[] listOfFileNames = TestingTools.CreateMultipleTestFilesWithDifferentContents(pathOfTestFolder, 5);
            string pathOfHashesFile = GenerateHashesFile(pathOfTestFolder);
            string[] listOfOriginalFileHashes = GetListOfFileHashes(listOfFileNames, pathOfTestFolder);
            ModifyFiles(listOfFileNames, pathOfTestFolder);
            string[] listOfNewFileHashes = GetListOfFileHashes(listOfFileNames, pathOfTestFolder);
            StringWriter consoleOutput = TestingTools.RerouteConsoleOutput();

            // Execute:
            LoadConsoleInputAndRunMethod(pathOfTestFolder);

            // Assert:
            string expectedDisplayedOutput = EnterFolderPathPrompt;
            expectedDisplayedOutput += ReturnDisplayedOutputWhenGeneratingHashesForListOfFiles(listOfFileNames, pathOfHashesFile, pathOfTestFolder);
            expectedDisplayedOutput += "Comparing file names and hashes with those in text file...\r\n";
            for (int currentIndex = 0; currentIndex < listOfFileNames.Length; ++currentIndex)
            {
                expectedDisplayedOutput += "Error: hash for " + listOfFileNames[currentIndex] + " does NOT match associated hash in File SHA 256 Hashes.txt:\r\n" +
                                           "Recent file hash: " + listOfNewFileHashes[currentIndex] + "\r\n" +
                                           "File SHA 256 Hashes.txt hash: " + listOfOriginalFileHashes[currentIndex] + "\r\n";
            }
            expectedDisplayedOutput += ReturnDisplayedOutputInconsistenciesFound();
            string actualDisplayedOutput = consoleOutput.ToString().Trim();
            Assert.AreEqual(expectedDisplayedOutput, actualDisplayedOutput);

            // Tear down:
            Directory.Delete(pathOfTestFolder, true);
        }


        private string[] GetListOfFileHashes(string[] listOfFileNames, string pathOfTestFolder)
        {
            string[] listOfFileHashes = new string[listOfFileNames.Length];
            for (int currentIndex = 0; currentIndex < listOfFileNames.Length; ++currentIndex)
            {
                string currentFileName = listOfFileNames[currentIndex];
                string currentFilePath = pathOfTestFolder + Path.DirectorySeparatorChar + currentFileName;
                listOfFileHashes[currentIndex] = HashingTools.ObtainFileHash(currentFilePath);
            }
            return listOfFileHashes;
        }


        private void ModifyFiles(string[] listOfFileNames, string pathOfTestFolder)
        {
            foreach (string currentFileName in listOfFileNames)
            {
                string currentFilePath = pathOfTestFolder + Path.DirectorySeparatorChar + currentFileName;
                File.AppendAllText(currentFilePath, "blah");
            }
        }


        [TestMethod()]
        [Timeout(TestingTools.ONE_SECOND)]
        public void CompareHashesOfFilesInFolderToRecordedHashesInTextFile_HashesFileContainsOnlyConsistentFileHashes_DisplayNoInconsistenciesFound()
        {
            // Set up:
            string pathOfTestFolder = TestingTools.CreateTestFolder(NameOfTestFolder);
            // It does not matter what the contents of the test files are, so we go with the slightly more efficient method below:
            string[] listOfFileNames = TestingTools.CreateMultipleTestFilesWithDifferentContents(pathOfTestFolder, 5);
            string pathOfHashesFile = GenerateHashesFile(pathOfTestFolder);
            StringWriter consoleOutput = TestingTools.RerouteConsoleOutput();

            // Execute:
            LoadConsoleInputAndRunMethod(pathOfTestFolder);

            // Assert:
            string expectedDisplayedOutput = EnterFolderPathPrompt;
            expectedDisplayedOutput += ReturnDisplayedOutputWhenGeneratingHashesForListOfFiles(listOfFileNames, pathOfHashesFile, pathOfTestFolder);
            expectedDisplayedOutput += ReturnDisplayedOutputNoInconsistenciesFound();
            string actualDisplayedOutput = consoleOutput.ToString().Trim();
            Assert.AreEqual(expectedDisplayedOutput, actualDisplayedOutput);

            // Tear down:
            Directory.Delete(pathOfTestFolder, true);
        }
    }
}