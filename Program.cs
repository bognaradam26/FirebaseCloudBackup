using Firebase.Models;
using Firebase.Services;
using FirebaseBackupWindowsForm;

namespace FirestoreHttpClient
{
    internal class Program
    {

        private const string ServiceAccountFilePath = "";
        public static BackupService backupService = new();
        public static ProjectService projectService = new();
        public static List<Project> projects = new();

        public static string ServiceAccountFilePath1 => ServiceAccountFilePath;

        [STAThread]
        private static void Main(string[] args)
        {
            try
            {
                Directory.SetCurrentDirectory(Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\"));
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("The specified directory does not exist. {0}", e);
            }

            main form = new main();
            form.GetAllProjects();
            form.ShowDialog();
        }
    }
}