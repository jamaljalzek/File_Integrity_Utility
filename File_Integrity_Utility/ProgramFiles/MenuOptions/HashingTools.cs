using System.Security.Cryptography;

namespace File_Integrity_Utility.ProgramFiles
{
    class HashingTools
    {
        /// <summary>
        /// The format of the returned list is that of { [file1Name, file1Hash], [file2Name, file2Hash], etc. }.
        /// </summary>
        /// <param name="pathOfFolder"></param>
        /// <param name="recursivelySearchOrNot"></param>
        /// <returns></returns>
        public static List<string[]> GetListOfFilePathsToHashes(string pathOfFolder, SearchOption recursivelySearchOrNot)
        {
            Console.WriteLine("\n" + "Generating hashes for all files in " + pathOfFolder + "...");
            string[] listOfFilePathsInFolder = Directory.GetFiles(pathOfFolder, "*", recursivelySearchOrNot);
            List<string[]> listOfFilePathsToHashes = new List<string[]>(listOfFilePathsInFolder.Length);
            foreach (string currentFilePath in listOfFilePathsInFolder)
            {
                AddCurrentFilePathAndItsFileHashToList(currentFilePath, listOfFilePathsToHashes);
            }
            Console.WriteLine("All file hashes generated.");
            return listOfFilePathsToHashes;
        }


        private static void AddCurrentFilePathAndItsFileHashToList(string currentFilePath, List<string[]> listOfFilePathsToHashes)
        {
            Console.Write(currentFilePath + "... ");
            string[] currentFilePathAndFileHash = new string[2];
            currentFilePathAndFileHash[0] = currentFilePath;
            currentFilePathAndFileHash[1] = ObtainFileHash(currentFilePath);
            listOfFilePathsToHashes.Add(currentFilePathAndFileHash);
            Console.WriteLine("DONE");
        }


        public static string ObtainFileHash(string pathOfCurrentFile)
        {
            FileStream currentFileStream = new FileStream(pathOfCurrentFile, FileMode.Open);
            SHA256 sha256HashGenerator = SHA256.Create();
            byte[] currentFileHashBytes = sha256HashGenerator.ComputeHash(currentFileStream);
            // We dispose of the currentFileStream now that we are done with it, otherwise if we try to open another FileStream for the same file that is currently in use by the currentFileStream, it will throw an exception:
            currentFileStream.Dispose();
            string currentFileHashString = BitConverter.ToString(currentFileHashBytes);
            currentFileHashString = currentFileHashString.Replace("-", "");
            return currentFileHashString.ToLower();
        }
    }
}