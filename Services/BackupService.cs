using Firebase.Models;
using FirebaseBackupWindowsForm.Services;
using Google.Cloud.Firestore;
using System.Text.Json;

namespace Firebase.Services
{


    public class BackupService
    {
        private static FirestoreService FirestoreService = new();
        private static GoogleDriveService DriveService = new();

        public async static void BackupData(Project project, ProgressBar progressBar)
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", project.ServiceAccountFilePath);
            FirestoreDb db = FirestoreDb.Create(project.ProjectId);

            IAsyncEnumerable<CollectionReference> rootCollections = db.ListRootCollectionsAsync();

            
            int currentCount = 0;
            float totalCount = 0;

            await foreach (CollectionReference collection in rootCollections)
            {
                 // For the progress bar - minden gyűjteménnyel előrehalad
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

                currentCount++;
                totalCount = currentCount + (1 / currentCount);// Az aktuális szám növelése minden befejezett gyűjteménnyel

                // A ProgressBar frissítése
                int progressPercentage = (int)((float)currentCount / totalCount * 100);
                progressBar.Invoke((MethodInvoker)delegate
                {
                    progressBar.Value = progressPercentage;
                });
            }
            // Display the serialized JSON
            progressBar.Invoke((MethodInvoker)delegate
            {
                progressBar.Value = 100;
            });
        }
    }


}