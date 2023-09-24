using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using Firebase.DataAccess;
using Firebase.Models;
using Firebase.Services;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace FirestoreHttpClient
{
    internal class Program
    {
        public class FirestoreNode
        {
            public Dictionary<string, object> Data { get; set; }
            public Dictionary<string, FirestoreNode> Subcollections { get; set; }
        }
        private const string ServiceAccountFilePath = "C:\\suli\\szakdoga\\mvc_DOTNET\\Firebase\\utalom-3b9c1.json";
        public static BackupService backupService = new();
        public static ProjectService projectService = new();
        public static List<Project> projects = new();

        private async static Task Main(string[] args)
        {
            projects = projectService.GetAllProjects();
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
                Console.WriteLine(project.ProjectId + ":\n" + project.ServiceAccountFilePath);


                //itt hívnám meg a megadott projektre a backupservice-t de a bemutatás érdekében a már létező firebase projektemen mutatom be
                Project backup = new("utalom-3b9c1", ServiceAccountFilePath);
                await BackupService.BackupData(backup);
            }



        }
    }
}