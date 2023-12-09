using Firebase.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;

namespace FirebaseBackupWindowsForm.Services
{
    public class RestoreServices
    {
        public static void RestoreData(Project project)
        {
            var credential = GoogleCredential.FromFile(project.ServiceAccountFilePath).CreateScoped(DriveService.ScopeConstants.DriveFile);

            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });
            //meglévő fájlok megkeresése
            List<string> fileIds = GoogleDriveService.GetAllFiles(service, project.ServiceAccountFilePath);

            foreach (string fileId in fileIds)
            {
                var request = service.Files.Get(fileId);
                Google.Apis.Drive.v3.Data.File fileInfo = request.Execute();
                using (MemoryStream stream = new MemoryStream())
                {
                    request.Download(stream);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "ConfigFiles", project.ProjectId, fileInfo.Name);
                    using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        stream.WriteTo(fileStream);
                        string json = File.ReadAllText(filePath);
                        FirestoreService.RestoreCollection(json,fileInfo.Name);
                        
                    }
                }

            }
        }
    }
}
