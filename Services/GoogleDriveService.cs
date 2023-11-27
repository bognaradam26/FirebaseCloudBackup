using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using File = Google.Apis.Drive.v3.Data.File;

namespace FirebaseBackupWindowsForm.Services
{
    internal class GoogleDriveService
    {

        private static async
    Task UploadFile(string serviceAccountFilePath, string filePath, string folderName)
        {
            var credential = GoogleCredential.FromFile(serviceAccountFilePath)
                .CreateScoped(DriveService.ScopeConstants.Drive);

            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });

            File fileMetadata = new() { Name = Path.GetFileName(filePath), Parents = new List<string>() { "1JqFyQ2yqomZLTJFoVp6qE1o0Hd09b_el" } };

            var request = service.Drives.List();
            var results = await request.ExecuteAsync();

            foreach (var resultFile in results.Drives)
            {
                Console.WriteLine($"{resultFile.Name} {resultFile.CreatedTimeDateTimeOffset} {resultFile.Id}");
            }



        }

        private static string GetFolderId(DriveService service, string folderName)
        {

            var listRequest = service.Files.List();
            listRequest.Q = $"name='{folderName}' and mimeType='application/vnd.google-apps.folder'";

            var files = listRequest.Execute().Files;
            if (files != null && files.Count > 0)
            {
                return files[0].Id;
            }

            Console.WriteLine("Folder not found.");
            return null;
        }
    }
}
