using Firebase.Models;
using Firebase.Services;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FirebaseBackupWindowsForm.Services
{
    public class FirestoreDocument
    {
        [JsonPropertyName("Data")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<KeyValuePair<string, object>>? Data { get; set; }

        [JsonPropertyName("Subcollections")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<FirestoreCollection>? Subcollections { get; set; }
    }

    public class FirestoreCollection
    {
        public string? Id { get; set; }
        public List<FirestoreDocument>? Documents { get; set; }
    }
    internal class FirestoreService
    {
        

            
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

        static async Task searchDocument(DocumentReference document, FirestoreDocument documentNode)
        {

        }
    }
}
