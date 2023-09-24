using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Firebase.Models;
using Google.Cloud.Firestore;

namespace Firebase.Services
{
    public class FirestoreNode
    {
        public List<KeyValuePair<string, object>> Data { get; set; }
        public List<KeyValuePair<string, FirestoreNode>> Subcollections { get; set; }
    }

    public class BackupService
    {
        public async static Task BackupData(Project project)
        {

            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", project.ServiceAccountFilePath);
            FirestoreDb db = FirestoreDb.Create(project.ProjectId);

            IAsyncEnumerable<CollectionReference> rootCollections = db.ListRootCollectionsAsync();
            FirestoreNode root = new()
            {
                Data = new List<KeyValuePair<string, object>>(),
                Subcollections = new List<KeyValuePair<string, FirestoreNode>>()
            };

            await foreach (CollectionReference collection in rootCollections)
            {

                FirestoreNode rootCollectionNode = new()
                {
                    Data = new List<KeyValuePair<string, object>>(),
                    Subcollections = new List<KeyValuePair<string, FirestoreNode>>()

                };
                await searchCollection(collection, rootCollectionNode);

                root.Subcollections.Add(new KeyValuePair<string, FirestoreNode>(collection.Id, rootCollectionNode));


                string json = JsonSerializer.Serialize(root, new JsonSerializerOptions
                {
                    WriteIndented = true // Optional: format the JSON for readability
                });

                // Display the serialized JSON
                Console.WriteLine(json);




            }

            static async Task searchCollection(CollectionReference collection, FirestoreNode collectionNode)
            {

                QuerySnapshot snapshots = await collection.GetSnapshotAsync();

                FirestoreNode documentNode = new()
                {
                    Data = new List<KeyValuePair<string, object>>(),
                    Subcollections = new List<KeyValuePair<string, FirestoreNode>>()
                };

                foreach (DocumentSnapshot document in snapshots.Documents)
                {

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
                            FirestoreNode subCollectionNode = new()
                            {
                                Data = new List<KeyValuePair<string, object>>(),
                                Subcollections = new List<KeyValuePair<string, FirestoreNode>>()
                            };
                            await searchCollection(subcollection, subCollectionNode);
                            documentNode.Subcollections.Add(new KeyValuePair<string, FirestoreNode>(subcollection.Id, subCollectionNode));
                        }
                    }
                    else
                    {
                        documentNode.Subcollections = new List<KeyValuePair<string, FirestoreNode>> ();
                    }
                    collectionNode.Subcollections.Add(new KeyValuePair<string,FirestoreNode>(collection.Id, documentNode));
                }
            }
        }
    }
}
