using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace FirebaseBackupWindowsForm.Models
{
    internal class FirestoreDocument
    {
        [JsonPropertyName("Data")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, object>? Data { get; set; }

        [JsonPropertyName("Subcollections")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<FirestoreCollection>? Subcollections { get; set; }
    }
}
