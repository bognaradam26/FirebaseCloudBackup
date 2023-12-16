using Firebase.Models;
using Newtonsoft.Json;

namespace Firebase.DataAccess
{
    public class DatabaseRepositoryImplementation : IDataRepository
    {
        private string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "ConfigFiles");
        public void AddProject(Project project)
        {
            string json = JsonConvert.SerializeObject(project, Formatting.Indented); 

            string saveDirectoryPath = Path.Combine(directoryPath, project.ProjectId);
            Directory.CreateDirectory(saveDirectoryPath);

            saveDirectoryPath = Path.Combine(saveDirectoryPath, project.ProjectId + "Config.json");
            File.WriteAllText(saveDirectoryPath, json);
        }

        public void DeleteProject(Project project)
        {
            string deleteDirectoryPath = Path.Combine(directoryPath, project.ProjectId);
            Directory.Delete(deleteDirectoryPath, true);
        }

        internal Project findById(string projectId)
        {
            string filePath = Path.Combine(directoryPath, projectId, projectId + "Config.json");
            Console.WriteLine(directoryPath);
            if (File.Exists(filePath))
            {
                try
                {
                    string jsonContent = File.ReadAllText(filePath);
                    var projectData = JsonConvert.DeserializeObject<Project>(jsonContent);

                    return new Project(projectData.ProjectId, projectData.ServiceAccountFilePath); 
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
                string json = File.ReadAllText(Path.Combine(folder, folderName + "Config.json"));

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
            // Ellenőrizzük, hogy a projekt már létezik-e
            string projectDirectoryPath = Path.Combine(directoryPath, project.ProjectId);
            string projectConfigPath = Path.Combine(projectDirectoryPath, project.ProjectId + "Config.json");

            if (Directory.Exists(projectDirectoryPath) && File.Exists(projectConfigPath))
            {
                // Módosítsuk a projekt adatait
                string json = JsonConvert.SerializeObject(project, Formatting.Indented);

                // Frissítsük a fájlt az új adatokkal
                File.WriteAllText(projectConfigPath, json);
            }
            else
            {
                // Ha a projekt nem létezik, akkor hozzáadhatunk egy hibakezelést vagy kivételt dobhatunk
                MessageBox.Show("A config fájl nem létezik.");
            }
        }
    }
}
