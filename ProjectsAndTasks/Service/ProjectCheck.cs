using MongoDB.Driver;
using ProjectsAndTasks.MongoDb.Model;
using ProjectsAndTasks.Interfaces;

namespace ProjectsAndTasks.Service
{
    public class ProjectCheck : IProjectCheck
    {
        private readonly IProjectRepository _repository;

        public ProjectCheck(IProjectRepository repository)
        {
            _repository = repository;
        }

        public bool IsProjectExists(string title)
        {
            return _repository.Exists(title);
        }
    }
}
