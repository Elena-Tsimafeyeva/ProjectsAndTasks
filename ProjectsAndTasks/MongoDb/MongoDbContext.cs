using MongoDB.Driver;
using ProjectsAndTasks.MongoDb.Model;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectsAndTasks.Interfaces;

namespace ProjectsAndTasks.MongoDb
{
    /// <summary>
    /// E.A.T. 16-October-2025
    /// Connecting MongoDB.
    /// </summary>
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext()
        {
            var connection = ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString;
            var databaseName = ConfigurationManager.AppSettings["MongoDatabase"];

            var client = new MongoClient(connection);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Person> Persons => _database.GetCollection<Person>("Persons");
        public IMongoCollection<Project> Projects => _database.GetCollection<Project>("Projects");
        public IMongoCollection<ProjectTask> Tasks => _database.GetCollection<ProjectTask>("Tasks");
        public IMongoCollection<ProfileInfoDB> ProfileInform => _database.GetCollection<ProfileInfoDB>("ProfileInform");
    }
}
