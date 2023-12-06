﻿using Google.Api;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Cloud.Firestore;
using Microsoft.IdentityModel.Tokens;
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

            var request = service.Files.List();
            var responseFiles = await request.ExecuteAsync();
            if (responseFiles.Files.Count > 0)
            {
                string isExistId = "";
                foreach (var driveFiles in responseFiles.Files)
                {
                    string fileToUploadName = Path.GetFileName(filePath);
                    if (driveFiles.Name.Equals(fileToUploadName))
                    {
                        isExistId = driveFiles.Id;

                    }
                }
                if (!isExistId.IsNullOrEmpty()) {
                    UpdateFile(service,isExistId,filePath);
                } else {
                    CreateFile(service, filePath);
                }

            } else {
                CreateFile(service, filePath);
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

        static void UpdateFile(DriveService service, string fileId, string updateFilePath)
        {
            try
            {
                byte[] newFileContent = System.IO.File.ReadAllBytes(updateFilePath);
                MemoryStream stream = new MemoryStream(newFileContent);
                FilesResource.UpdateMediaUpload updateRequest = service.Files.Update(null, fileId, stream, "application/octet-stream");
                updateRequest.Upload();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating file: {ex.Message}");
            }
        }

        static void CreateFile(DriveService service, string fileToCreatePath)
        {
            byte[] newFileContent = System.IO.File.ReadAllBytes(fileToCreatePath);

            File fileMetadata = new() { Name = Path.GetFileName(fileToCreatePath), Parents = new List<string>() };
            MemoryStream stream = new MemoryStream(newFileContent);
            try
            {
                FilesResource.CreateMediaUpload createRequest = service.Files.Create(fileMetadata, stream, "application/octet-stream");
                createRequest.Upload();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating file: {ex.Message}");
            }
        }
    }
}
