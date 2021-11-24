namespace FileIntegrityUtility.ProgramFiles.MenuOptions
{
    class MenuOption1
    {
        public static void DisplayFileNameFollowedByItsHashCode()
        {
            string fullPathOfFile = ConsoleTools.ObtainFullFilePathFromUser();
            Console.WriteLine();
            Console.WriteLine("Generating hash for given file...");
            string fileHashString = HashingTools.ObtainFileHash(fullPathOfFile);
            Console.WriteLine("File hash complete.");
            string currentFileNameWithExtension = Path.GetFileName(fullPathOfFile);
            Console.WriteLine();
            Console.WriteLine(currentFileNameWithExtension);
            Console.WriteLine(fileHashString);
        }
    }
}