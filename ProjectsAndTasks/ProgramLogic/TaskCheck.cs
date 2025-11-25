using MongoDB.Driver;
using ProjectsAndTasks.MongoDb.Model;
using ProjectsAndTasks.MongoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsAndTasks.ProgramLogic
{
    public class TaskCheck
    {
        private readonly MongoDbContext _context = new MongoDbContext();
        public bool IsTaskExists(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return true;

            var filter = Builders<ProjectTask>.Filter.Eq(p => p.TaskName, title);
            return _context.Tasks.Find(filter).Any();
        }
    }
}
