using MongoDB.Driver;
using ProjectsAndTasks.MongoDb.Model;
using ProjectsAndTasks.MongoDb;
using ProjectsAndTasks.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsAndTasks.Service
{
    public class LoginCheck
    {
        private readonly IMongoDbContext _context;
        public LoginCheck(IMongoDbContext context)
        {
            _context = context;
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
        /// <summary>
        ///  E.A.T. 24-November-2025
        /// Checks if a password is correct.
        /// </summary>
        public bool ValidatePassword(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;
            var filter = Builders<Person>.Filter.Eq(p => p.Login, login);
            var user = _context.Persons.Find(filter).FirstOrDefault();
            return user != null && user.Password == password;
        }
        public bool IsPasswordExists(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return true;
            else
                return false;
        }
    }
}
