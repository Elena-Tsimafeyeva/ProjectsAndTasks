using ProjectsAndTasks.MongoDb.Model;
using ProjectsAndTasks.MongoDb;
using System.IO;
using MongoDB.Driver;

namespace ProjectsAndTasks.ProgramLogic
{
    /// <summary>
    /// E.A.T. 24-November-2025
    /// User registration.
    /// </summary>
    public class SaveUser
    {
        private readonly MongoDbContext _context = new MongoDbContext();
        public async Task RegisterUser(string login, string password)
        {
            var newPerson = new Person
            {
                Login = login,
                Password = password
            };
            var context = new MongoDbContext();
            context.Persons.InsertOne(newPerson);

            await SaveUserFileAsync(login);
        }
        public async Task SaveUserFileAsync(string? login)
        {
            string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Info");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }    
            string path = Path.Combine(folder, "user.txt");

            var filter = Builders<Person>.Filter.Eq(p => p.Login, login);
            var user = _context.Persons.Find(filter).FirstOrDefault();

            if (user != null)
            {
                await File.WriteAllTextAsync(path, user.Id.ToString());
            }
            else
            {
                await File.WriteAllTextAsync(path, "Пользователь не найден");
            }
        }
    }
}
