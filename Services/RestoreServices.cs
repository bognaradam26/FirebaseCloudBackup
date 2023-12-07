using Firebase.Models;

namespace FirebaseBackupWindowsForm.Services
{
    public class RestoreServices
    {
        private static FirestoreService FirestoreService = new();

        public void RestoreData(Project project)
        {
            GoogleDriveService.GetAllFiles(project.ServiceAccountFilePath);
        }
    }
}
