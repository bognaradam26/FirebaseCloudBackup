﻿using Firebase.Models;
using FirebaseBackupWindowsForm.Services;
using Google.Cloud.Firestore;
using System.Text.Json;

namespace Firebase.Services
{


    public class BackupService
    {
        private static FirestoreService FirestoreService = new();
        private static GoogleDriveService DriveService = new();

        public async static Task<string> BackupData(Project project)
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", project.ServiceAccountFilePath);
            FirestoreDb db = FirestoreDb.Create(project.ProjectId);

            IAsyncEnumerable<CollectionReference> rootCollections = db.ListRootCollectionsAsync();

            string json = "";

            await foreach (CollectionReference collection in rootCollections)
            {
                FirestoreCollection root = new()
                {
                    Id = collection.Id,
                    Documents = new List<FirestoreDocument>()
                };

                await FirestoreService.searchCollection(collection, root);

                string json1 = JsonSerializer.Serialize(root, new JsonSerializerOptions
                {
                    WriteIndented = true, // Optional: format the JSON for readability
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });
                string savePath = Path.Combine(Directory.GetCurrentDirectory(), "ConfigFiles", project.ProjectId, collection.Id + ".json");
                File.WriteAllText(savePath, json1);

                await DriveService.UploadFile(Path.Combine(Directory.GetCurrentDirectory(), project.ProjectId + ".json"), savePath);
                File.Delete(savePath);
            }
            MessageBox.Show("Backup succefully ended");
            // Display the serialized JSON
            return json;

        }
    }


}