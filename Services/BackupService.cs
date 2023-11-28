﻿using Firebase.Models;
using FirebaseBackupWindowsForm.Services;
using Google.Cloud.Firestore;
using System.Text.Json;

namespace Firebase.Services
{


    public class BackupService
    {
        FirestoreService firestoreService;
        GoogleDriveService driveService;

        public async static Task<string> BackupData(Project project)
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", project.ServiceAccountFilePath);
            FirestoreDb db = FirestoreDb.Create(project.ProjectId);

            IAsyncEnumerable<CollectionReference> rootCollections = db.ListRootCollectionsAsync();
            FirestoreCollection root = new()
            {
                Data = new List<KeyValuePair<string, object>>(),
                Subcollections = new List<FirestoreCollection>()
            };

            await foreach (CollectionReference collection in rootCollections)
            {

                await searchCollection(collection, root);

                string json1 = JsonSerializer.Serialize(root, new JsonSerializerOptions
                {
                    WriteIndented = true, // Optional: format the JSON for readability
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });

                return json1;
            }
            string json = JsonSerializer.Serialize(root, new JsonSerializerOptions
            {
                WriteIndented = true, // Optional: format the JSON for readability
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });

            // Display the serialized JSON
            return json;
        }
        static async Task searchCollection(CollectionReference collection, FirestoreDocument collectionNode)
        {
            QuerySnapshot snapshots = await collection.GetSnapshotAsync();

            foreach (DocumentSnapshot document in snapshots.Documents)
            {
                FirestoreDocument documentNode = new()
                {
                    Data = new List<KeyValuePair<string, object>>(),
                    Subcollections = new List<FirestoreCollection>()
                };

                DocumentReference docref = document.Reference;
                Dictionary<string, object> data = document.ToDictionary();

                foreach (KeyValuePair<string, object> field in data)
                {
                    string key = field.Key;
                    object value = field.Value;
                    documentNode.Data.Add(new KeyValuePair<string, object>(key, value));
                }

                IAsyncEnumerable<CollectionReference> subcollections = docref.ListCollectionsAsync();

                if (subcollections != null)
                {
                    await foreach (CollectionReference subcollection in subcollections)
                    {
                        FirestoreDocument subCollectionNode = new()
                        {
                            Data = new List<KeyValuePair<string, object>>(),
                            Subcollections = new List<FirestoreCollection>()
                        };
                        await searchCollection(subcollection, subCollectionNode);
                        documentNode.Subcollections.Add(new FirestoreCollection(subcollection.Id, subCollectionNode));
                    }
                }

                collectionNode.Subcollections.Add(new FirestoreCollection(document.Id, documentNode));
            }
        }
    }


}