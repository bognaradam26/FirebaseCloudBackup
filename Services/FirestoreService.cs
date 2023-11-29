using Google.Cloud.Firestore;
using System.Text.Json.Serialization;

namespace FirebaseBackupWindowsForm.Services
{
    public class FirestoreDocument
    {
        [JsonPropertyName("Data")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, object>? Data { get; set; }

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
    }
}
