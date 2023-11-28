namespace Firebase.Models
{
    public class Project
    {
        public string ProjectId { get; set; }
        public string ServiceAccountFilePath { get; set; }
        public DateTimeOffset? LastBackupDate { get; set; } = null;
        public List<String>? rootCollections { get; set; } = null;

        public Project(string projectId, string serviceAccountFilePath)
        {
            ProjectId = projectId;
            ServiceAccountFilePath = serviceAccountFilePath;
            LastBackupDate = null;
            List<String> rootCollections = new List<String>();
        }
    }
}
