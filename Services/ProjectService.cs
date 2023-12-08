using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.DataAccess;
using Firebase.Models;

namespace Firebase.Services
{
    public class ProjectService
    {
        private readonly DatabaseRepositoryImplementation dataRepository = new();

        public ProjectService(DatabaseRepositoryImplementation dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        public ProjectService()
        {
        }

        public List<Project> GetAllProjects()
        {
            return dataRepository.GetProjects();
        }

        public void AddProject(Project project)
        {
            dataRepository.AddProject(project);
        }

        public Project findById(string? projectId)
        {
            return dataRepository.findById(projectId);
        }

        public void DeleteProject(Project project)
        {
            dataRepository.DeleteProject(project);
        }

        // Implement UpdateProject and DeleteProject methods similarly
    }
}
