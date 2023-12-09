using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FirebaseBackupWindowsForm.Models
{
    internal class FirestoreCollection
    {
        public string? Id { get; set; }
        public List<FirestoreDocument>? Documents { get; set; }
    }
}
