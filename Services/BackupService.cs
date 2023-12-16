using Firebase.Models;
using FirebaseBackupWindowsForm.Models;
using FirebaseBackupWindowsForm.Services;
using Google.Cloud.Firestore;
using System.Text.Json;

namespace Firebase.Services
{


    public class BackupService
    {
        private static GoogleDriveService DriveService = new();
        public static ProjectService projectService = new();

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
                });
                string savePath = Path.Combine(Directory.GetCurrentDirectory(), "ConfigFiles", project.ProjectId, collection.Id + ".json");
                File.WriteAllText(savePath, json1);

                await DriveService.UploadFile(project.ServiceAccountFilePath, savePath);
                //File.Delete(savePath);

                currentCount++;
                totalCount = currentCount + (1 / currentCount);// Az aktuális szám növelése minden befejezett gyűjteménnyel

                // A ProgressBar frissítése
                int progressPercentage = (int)((float)currentCount / totalCount * 100);
                progressBar.Invoke((MethodInvoker)delegate
                {
                    progressBar.Value = progressPercentage;
                });
            }
            progressBar.Invoke((MethodInvoker)delegate
            {
                progressBar.Value = 100;
            });

            project.LastBackupDate = DateTime.Now;
            projectService.UpdateProject(project);
        }
    }


}