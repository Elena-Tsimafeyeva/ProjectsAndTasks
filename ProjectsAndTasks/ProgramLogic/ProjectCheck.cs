using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using ProjectsAndTasks.MongoDb;
using ProjectsAndTasks.MongoDb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsAndTasks.ProgramLogic
{
    public class ProjectCheck
    {
        private readonly MongoDbContext _context = new MongoDbContext();
        public bool IsProjectExists(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return true;

            var filter = Builders<Project>.Filter.Eq(p => p.ProjectName, title);
            return _context.Projects.Find(filter).Any();
        }
    }
}
