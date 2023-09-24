using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Models;

namespace Firebase.DataAccess
{
    public interface IDataRepository
    {
        List<Project> GetProjects();
        void AddProject(Project project);
        void UpdateProject(Project project);
        void DeleteProject(string projectId);
    }
}
