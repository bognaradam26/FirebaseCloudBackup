using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firebase.Models
{
    public class Project
    {
        public string ProjectId { get; set; }
        public string ServiceAccountFilePath { get; set; }

        public Project(string projectId, string serviceAccountFilePath)
        {
            ProjectId = projectId;
            ServiceAccountFilePath = serviceAccountFilePath;
        }

        public Project()
        {
        }
    }
}
