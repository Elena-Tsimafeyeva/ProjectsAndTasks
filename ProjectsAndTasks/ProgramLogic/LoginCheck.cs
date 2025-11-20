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
    public class LoginCheck
    {
        private readonly MongoDbContext _context;

        public LoginCheck()
        {
            _context = new MongoDbContext();
        }

        /// <summary>
        ///  E.A.T. 05-November-2025
        /// Checks if a login exists in the Persons collection.
        /// </summary>
        public bool IsLoginExists(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
                return true;

            var filter = Builders<Person>.Filter.Eq(p => p.Login, login);
            return _context.Persons.Find(filter).Any();
        }
    }
}
