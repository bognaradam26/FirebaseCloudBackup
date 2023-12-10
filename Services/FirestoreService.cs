using Firebase.Models;
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

                documentNode.Id = document.Id;

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
            CollectionReference collectionRef = parentCollection.Document(firestoreCollection.Id).Collection(firestoreCollection.Id);
            foreach (var firestoreDocument in firestoreCollection.Documents)
            {
                DocumentReference documentRef = collectionRef.Document(firestoreDocument.Id);

                Dictionary<string, object> data = firestoreDocument.Data ?? new Dictionary<string, object>();
                await documentRef.SetAsync(data);

                // Rekurzív hívás az alkollekciók esetén
                if (firestoreDocument.Subcollections != null)
                {
                    await CreateFirestoreCollection(collectionRef, firestoreDocument.Subcollections); 
                }
            }
        }

        static async Task CreateFirestoreCollection(CollectionReference parentCollection, List<FirestoreCollection> firestoreCollections)
        {
            // Bejárás és Firestore kollekciók létrehozása
            foreach (var collection in firestoreCollections)
            {
                await CreateFirestoreCollection(parentCollection, collection);
            }
        }
    }
}
