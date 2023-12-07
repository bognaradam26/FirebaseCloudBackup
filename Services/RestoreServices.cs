using Firebase.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;

namespace FirebaseBackupWindowsForm.Services
{
    public class RestoreServices
    {
        private static FirestoreService FirestoreService = new();

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
                MemoryStream stream = new MemoryStream();
                Google.Apis.Drive.v3.Data.File fileInfo = request.Execute();
                request.Download(stream);
                var fileStream = new FileStream(Path.Combine("D:\\projects\\FirebaseBackupWindowsForm\\ConfigFiles\\utalom-3b9c1",fileInfo.Name), FileMode.Create, FileAccess.Write);
                stream.WriteTo(fileStream);

            }
        }
    }
}
