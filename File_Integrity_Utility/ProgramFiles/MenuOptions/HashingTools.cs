using System.Security.Cryptography;

namespace FileIntegrityUtility.ProgramFiles
{
    class HashingTools
    {
        public static List<string> GetListOfFileNamesFollowedByTheirHashes(string fullPathOfFolder, SearchOption recursivelySearchOrNot)
        {
            Console.WriteLine();
            Console.WriteLine("Generating hashes for all files in " + fullPathOfFolder + "...");
            string[] listOfFullFilePathsInFolder = Directory.GetFiles(fullPathOfFolder, "*", recursivelySearchOrNot);
            List<string> fileNamesFollowedByTheirHashes = new List<string>(listOfFullFilePathsInFolder.Length * 2);
            foreach (string currentFullFilePath in listOfFullFilePathsInFolder)
            {
                Console.Write(currentFullFilePath + "... ");
                AddCurrentFileNameAndItsHashToList(currentFullFilePath, fileNamesFollowedByTheirHashes);
                Console.WriteLine("DONE");
            }
            Console.WriteLine("All file hashes generated.");
            return fileNamesFollowedByTheirHashes;
        }


        private static void AddCurrentFileNameAndItsHashToList(string currentFullFilePath, List<string> fileNamesFollowedByTheirHashes)
        {
            string currentFileHashString = ObtainFileHash(currentFullFilePath);
            string currentFileNameWithExtension = Path.GetFileName(currentFullFilePath);
            fileNamesFollowedByTheirHashes.Add(currentFileNameWithExtension);
            fileNamesFollowedByTheirHashes.Add(currentFileHashString);
        }


        public static string ObtainFileHash(string fullPathOfCurrentFile)
        {
            FileStream currentFileStream = new FileStream(fullPathOfCurrentFile, FileMode.Open);
            SHA256 sha256HashGenerator = SHA256.Create();
            byte[] currentFileHashBytes = sha256HashGenerator.ComputeHash(currentFileStream);
            // We dispose of the currentFileStream now that we are done with it, otherwise if we try to open another FileStream for the same file that is currently in use by the currentFileStream, it will throw an exception:
            currentFileStream.Dispose();
            string currentFileHashString = BitConverter.ToString(currentFileHashBytes);
            currentFileHashString = currentFileHashString.Replace("-", "");
            return currentFileHashString;
        }
    }
}