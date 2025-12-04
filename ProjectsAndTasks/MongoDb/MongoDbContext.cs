using MongoDB.Driver;
using ProjectsAndTasks.MongoDb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsAndTasks.MongoDb
{
    /// <summary>
    /// E.A.T. 16-October-2025
    /// Connecting MongoDB.
    /// </summary>
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            _database = client.GetDatabase("ProjectsAndTasksDB");
        }

        public IMongoCollection<Person> Persons => _database.GetCollection<Person>("Persons");
        public IMongoCollection<Project> Projects => _database.GetCollection<Project>("Projects");
        public IMongoCollection<ProjectTask> Tasks => _database.GetCollection<ProjectTask>("Tasks");
        public IMongoCollection<ProfileInfoDB> ProfileInform => _database.GetCollection<ProfileInfoDB>("ProfileInform");
    }
}
