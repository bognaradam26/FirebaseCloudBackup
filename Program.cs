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

        private async static Task Main(string[] args)
        {

            main form = new main();
            form.GetAllProjects();
            form.ShowDialog();

            /*
            if (projects.Count == 0)
            {
                Console.WriteLine("Add meg a projekt azonosítóját:");
                string projectId = Console.ReadLine();

                Console.WriteLine("Add meg a projekt service acccount .json file abszolút útvonalát:");
                string path = Console.ReadLine();

                Project project = new(projectId, path);

                projectService.AddProject(project);
            }
            else
            {
                Console.WriteLine("A már meglévő projektjeid:");
                foreach (Project project in projects)
                {
                    Console.WriteLine("" + project.ProjectId);
                }
            }

            Console.WriteLine("Szeretnél új projektet hozzáadni? Y/N");
            if (Console.ReadLine().Equals("Y"))
            {
                Console.WriteLine("Add meg a projekt azonosítóját:");
                string projectId = Console.ReadLine();

                Console.WriteLine("Add meg a projekt service acccount .json file abszolút útvonalát:");
                string path = Console.ReadLine();

                Project project = new(projectId, path);

                projectService.AddProject(project);
            }
            else
            {
                Console.WriteLine("Add meg a projekt ID-t aminek a biztonsági mentését szeretnéd elkészíteni.");
                string id = Console.ReadLine();
                Project project = projectService.findById(id);
                Console.WriteLine(project.ProjectId + ":\n");


                //itt hívnám meg a megadott projektre a backupservice-t de a bemutatás érdekében a már létező firebase projektemen mutatom be
                Project backup = new("utalom-3b9c1", "D:\\projects\\FirebasebackuoToCloud\\FirebaseCloudBackup\\utalom-3b9c1.json");
                await BackupService.BackupData(backup);
            }
            Project backup = new("utalom-3b9c1", "D:\\projects\\FirebasebackuoToCloud\\FirebaseCloudBackup\\utalom-3b9c1.json");
            await BackupService.BackupData(backup);*/


        }
    }
}