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
                AddCurrentFilePathAndItsHashToList(currentFullFilePath, fileNamesFollowedByTheirHashes);
            }
            Console.WriteLine("All file hashes generated.");
            return fileNamesFollowedByTheirHashes;
        }


        private static void AddCurrentFilePathAndItsHashToList(string currentFullFilePath, List<string> fileNamesFollowedByTheirHashes)
        {
            Console.Write(currentFullFilePath + "... ");
            fileNamesFollowedByTheirHashes.Add(currentFullFilePath);
            string currentFileHashString = ObtainFileHash(currentFullFilePath);
            fileNamesFollowedByTheirHashes.Add(currentFileHashString);
            Console.WriteLine("DONE");
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