using Firebase.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;

namespace FirebaseBackupWindowsForm.Services
{
    public class RestoreServices
    {
        public static async Task RestoreData(Project project)
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
                        
                    }
                    if (File.Exists(filePath))
                    {
                        string json = File.ReadAllText(filePath);

                        // Kezeljük a FirestoreService.RestoreCollection függvényt.
                        try
                        {
                            await FirestoreService.RestoreCollection(json, fileInfo.Name, project);
                            File.Delete(filePath);
                        }
                        catch (Exception ex)
                        {
                            // Kivételkezelés: kezeld a FirestoreService.RestoreCollection hívás közbeni hibákat.
                            MessageBox.Show($"Hiba a FirestoreService.RestoreCollection függvény hívása közben: {ex.Message}");
                        }
                    }
                    else
                    {
                        MessageBox.Show($"A fájl nem található: {filePath}");
                    }
                }

            }
        }
    }
}
