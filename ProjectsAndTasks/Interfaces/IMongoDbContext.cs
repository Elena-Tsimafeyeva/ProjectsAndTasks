using MongoDB.Driver;
using ProjectsAndTasks.MongoDb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsAndTasks.Interfaces
{
    public interface IMongoDbContext
    {
        IMongoCollection<Person> Persons { get; }
        IMongoCollection<Project> Projects { get; }
        IMongoCollection<ProjectTask> Tasks { get; }
        IMongoCollection<ProfileInfoDB> ProfileInform { get; }
    }
}
