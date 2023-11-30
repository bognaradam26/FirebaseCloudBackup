using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using File = Google.Apis.Drive.v3.Data.File;

namespace FirebaseBackupWindowsForm.Services
{
    internal class GoogleDriveService
    {

        public async Task UploadFile(string serviceAccountFilePath, string filePath)
        {
            var credential = GoogleCredential.FromFile(serviceAccountFilePath)
                .CreateScoped(DriveService.ScopeConstants.DriveFile);

            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });

            File fileMetadata = new() { Name = Path.GetFileName(filePath), Parents = new List<string>() };

            var stream = new FileStream(filePath, FileMode.Open);


            var request = service.Files.List();
            var responseFiles = await request.ExecuteAsync();
            foreach (var driveFiles in responseFiles.Files)
            {
                string fileToUploadName = Path.GetFileName(filePath);
                if (driveFiles.Name.Equals(fileToUploadName))
                {
                    MessageBox.Show("egyezik");
                    
                }
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

        static void UpdateFile(DriveService service, string fileId, File updateFile)
        {
            try
            {
                service.Files.Update(updateFile, fileId).Execute();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating file: {ex.Message}");
            }
        }

        static void CreateFile(DriveService service, File fileToCreate)
        {
            try
            {
                service.Files.Create(fileToCreate).Execute();
            } catch (Exception ex)
            {
                MessageBox.Show($"Error creating file: {ex.Message}");
            }
        }
    }
}
