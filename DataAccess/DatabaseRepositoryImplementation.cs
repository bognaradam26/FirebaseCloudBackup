using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Firebase.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Firebase.DataAccess
{
    public class DatabaseRepositoryImplementation : IDataRepository
    {
        private string directoryPath = Path.Combine("D:\\projects\\FirebasebackuoToCloud\\FirebaseCloudBackup\\ConfigFiles");
        public void AddProject(Project project)
        {
            string json = "";

            directoryPath = Path.Combine(directoryPath, project.ProjectId);
            Directory.CreateDirectory(directoryPath);

            directoryPath = Path.Combine(directoryPath, project.ProjectId + ".json");
            File.WriteAllText(directoryPath, json);
        }

        public void DeleteProject(string projectId)
        {
            directoryPath = Path.Combine(directoryPath, projectId);
            Directory.Delete(directoryPath, true);
        }

        internal Project findById(string projectId)
        {
            string filePath = Path.Combine(directoryPath, projectId, projectId + ".json");
            Console.WriteLine(directoryPath);
            if (File.Exists(filePath))
            {
                try
                {
                    string jsonContent = File.ReadAllText(filePath);
                    var projectData = JsonConvert.DeserializeObject<Project>(jsonContent);

                    Project project = new()
                    {
                        ProjectId = projectData.ProjectId,
                        ServiceAccountFilePath = projectData.ServiceAccountFilePath
                    };
                    return project;
                }
                catch (System.Text.Json.JsonException)
                {
                    Console.WriteLine("Nem találom ezt a projektet :(");
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public List<Project> GetProjects()
        {

            List<Project> projects = new();
            string[] folders = Directory.GetDirectories(directoryPath);
            foreach (string folder in folders)
            {
                string folderName = Path.GetFileName(folder);
                string json = File.ReadAllText(Path.Combine(folder, folderName + ".json"));

                Project? project = JsonConvert.DeserializeObject<Project>(json);
                if (project != null)
                {
                    projects.Add(project);
                }
            }

            return projects;
        }

        public void UpdateProject(Project project)
        {
            throw new NotImplementedException();
        }
    }
}
