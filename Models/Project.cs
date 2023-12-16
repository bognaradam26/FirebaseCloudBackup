namespace Firebase.Models
{
    public class Project
    {
        public string ProjectId { get; set; }
        public string ServiceAccountFilePath { get; set; }
        public DateTime? LastBackupDate { get; set; }

        public Project(string projectId, string serviceAccountFilePath, DateTime? lastBackupDate)
        {
            ProjectId = projectId;
            ServiceAccountFilePath = serviceAccountFilePath;
            LastBackupDate = lastBackupDate;
        }
    }
}
