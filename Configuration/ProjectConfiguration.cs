using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Models;

namespace Firebase.Configuration
{
    public class ProjectConfiguration
    {
        public List<Project> Projects { get; set; }

        public ProjectConfiguration(List<Project> projects)
        {
            Projects = projects;
        }
    }
}
