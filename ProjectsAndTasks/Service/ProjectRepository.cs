using MongoDB.Driver;
using ProjectsAndTasks.Interfaces;
using ProjectsAndTasks.MongoDb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsAndTasks.Service
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IMongoCollection<Project> _projects;

        public ProjectRepository(IMongoDbContext context)
        {
            _projects = context.Projects;
        }

        public bool Exists(string title)
        {
            if (string.IsNullOrWhiteSpace(title)) return false;
            var filter = Builders<Project>.Filter.Eq(p => p.ProjectName, title);
            return _projects.Find(filter).Project(p => p.Id).Limit(1).Any();
        }
    }
}
