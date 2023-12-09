using FirebaseBackupWindowsForm.Models;
using Google.Cloud.Firestore;
using System.Text.Json;

namespace FirebaseBackupWindowsForm.Services
{
    internal class FirestoreService
    {
        public static async Task searchCollection(CollectionReference collection, FirestoreCollection collectionNode)
        {
            QuerySnapshot snapshots = await collection.GetSnapshotAsync();

            foreach (DocumentSnapshot document in snapshots.Documents)
            {

                FirestoreDocument documentNode = new()
                {
                    Data = new Dictionary<string, object>(),
                    Subcollections = new List<FirestoreCollection>()
                };

                DocumentReference docref = document.Reference;
                Dictionary<string, object> data = document.ToDictionary();

                documentNode.Data.Add("ItemId", document.Id);

                foreach (KeyValuePair<string, object> field in data)
                {
                    string key = field.Key;
                    object value = field.Value;
                    documentNode.Data.Add(key, value);
                }

                await searchDocument(docref, documentNode);


                collectionNode.Documents.Add(documentNode);
            }
        }

        static async Task searchDocument(DocumentReference document, FirestoreDocument documentNode)
        {
            IAsyncEnumerable<CollectionReference> subcollections = document.ListCollectionsAsync();

            if (subcollections != null)
            {
                await foreach (CollectionReference subcollection in subcollections)
                {
                    FirestoreCollection subCollectionNode = new()
                    {
                        Id = subcollection.Id,
                        Documents = new List<FirestoreDocument>()
                    };
                    await searchCollection(subcollection, subCollectionNode);
                    documentNode.Subcollections.Add(subCollectionNode);
                }
            }
        }

        public static void RestoreCollection(string json, string collectionName)
        {
            FirestoreDocument deserializedFirestoreDocument = JsonSerializer.Deserialize<FirestoreDocument>(json);

            if (deserializedFirestoreDocument != null)
            {
                MessageBox.Show($"Data Key1: {deserializedFirestoreDocument.Data?["Key1"]}");
                MessageBox.Show($"Subcollections Id: {deserializedFirestoreDocument.Subcollections?[0]?.Id}");
            }

        }
    }
}
