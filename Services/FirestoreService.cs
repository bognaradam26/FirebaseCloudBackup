using Firebase.Models;
using FirebaseBackupWindowsForm.Models;
using Google.Cloud.Firestore;
using Google.Protobuf.WellKnownTypes;
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

                await searchDocument(document, documentNode);

                collectionNode.Documents.Add(documentNode);
            }
        }

        static async Task searchDocument(DocumentSnapshot document, FirestoreDocument documentNode)
        {
            DocumentReference documentReference = document.Reference;
            IAsyncEnumerable<CollectionReference> subcollections = documentReference.ListCollectionsAsync();
            
            documentNode.Id = documentReference.Id;
            Dictionary<string, object> data = document.ToDictionary();
            foreach (KeyValuePair<string, object> field in data)
            {
                string key = field.Key;
                object value = field.Value;
                documentNode.Data.Add(key, value);
            }

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

        public static async Task RestoreCollection(string json, string collectionName, Project project)
        {
            FirestoreCollection? deserializedFirestoreCollection = JsonSerializer.Deserialize<FirestoreCollection>(json);

            if (deserializedFirestoreCollection != null)
            {
                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", project.ServiceAccountFilePath);
                FirestoreDb db = FirestoreDb.Create(project.ProjectId);
                CollectionReference rootCollectionRef = db.Collection(deserializedFirestoreCollection.Id);
                await CreateFirestoreCollection(rootCollectionRef, deserializedFirestoreCollection);
            }

        }

        static async Task CreateFirestoreCollection(CollectionReference parentCollection, FirestoreCollection firestoreCollection)
        {

            foreach (var firestoreDocument in firestoreCollection.Documents)
            {
                DocumentReference documentRef = parentCollection.Document(firestoreDocument.Id);

                Dictionary<string, object> data = new();
                foreach (var asd in firestoreDocument.Data)
                {
                    JsonElement jsonElement = (JsonElement)asd.Value;
                    switch (jsonElement.ValueKind)
                    {
                        case JsonValueKind.String:
                            data.Add(asd.Key, jsonElement.ToString());
                            break;

                        case JsonValueKind.Number:
                            if (jsonElement.TryGetInt32(out int intValue))
                            {
                                Console.WriteLine("Integer érték: " + intValue);
                                data.Add(asd.Key, intValue);
                            }
                            else if (jsonElement.TryGetDouble(out double doubleValue))
                            {
                                data.Add(asd.Key, doubleValue);
                            }
                            break;

                        case JsonValueKind.True:
                            data.Add(asd.Key, jsonElement.GetBoolean());
                            break;
                        case JsonValueKind.False:
                            data.Add(asd.Key, jsonElement.GetBoolean());
                            break;
                        case JsonValueKind.Array:
                            foreach (JsonElement arrayElement in jsonElement.EnumerateArray())
                            {
                                // Kezelje az egyes tömb elemeket itt
                            }
                            break;

                        // További esetek hozzáadása szükség esetén

                        default:
                            Console.WriteLine("Ismeretlen vagy nem támogatott értéktípus.");
                            break;
                    }
                    
                }
                await documentRef.SetAsync(data);

                // Rekurzív hívás az alkollekciók esetén
                if (firestoreDocument.Subcollections != null)
                {
                    await CreateFirestoreCollection(documentRef, firestoreDocument.Subcollections);
                }
            }
        }

        static async Task CreateFirestoreCollection(DocumentReference parentCollection, List<FirestoreCollection> firestoreCollections)
        {
            // Bejárás és Firestore kollekciók létrehozása
            foreach (var collection in firestoreCollections)
            {
                CollectionReference collRef = parentCollection.Collection(collection.Id);
                await CreateFirestoreCollection(collRef, collection);
            }
        }
    }
}
