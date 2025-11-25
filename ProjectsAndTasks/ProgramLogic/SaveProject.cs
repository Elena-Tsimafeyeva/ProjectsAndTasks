using ProjectsAndTasks.MongoDb;
using ProjectsAndTasks.MongoDb.Model;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ProjectsAndTasks.ProgramLogic
{
    public class SaveProject
    {
        private readonly MongoDbContext _context = new MongoDbContext();
        private readonly ProjectCheck _projectCheck = new ProjectCheck();
        public void SaveMyProject(string title, string description, int progress)
        {
            if (_projectCheck.IsProjectExists(title))
            {
                MessageBox.Show("Такое имя проекта уже есть или пустое значение");
                var saveTitle = new SaveTitle();
                saveTitle.SaveMyTitleAsync("");
            }
            else
            {
                string text = ReadUserId();
                ObjectId personId = ObjectId.Parse(text);

                var newProject = new Project
                {
                    PersonId = personId,
                    ProjectName = title,
                    ProjectDescription = description,
                    Percent = progress
                };
                _context.Projects.InsertOne(newProject);
            }
        }
        public async Task UpdateProjectAsync(string title, string description, int progress)
        {
            string text = ReadUserId();
            ObjectId personId = ObjectId.Parse(text);

            var filter = Builders<Project>.Filter.And(
                Builders<Project>.Filter.Eq(p => p.PersonId, personId),
                Builders<Project>.Filter.Eq(p => p.ProjectName, title)
            );

            var update = Builders<Project>.Update
                .Set(p => p.ProjectDescription, description)
                .Set(p => p.Percent, progress);

            var result = await _context.Projects.UpdateOneAsync(filter, update);

            if (result.ModifiedCount > 0)
            {
                MessageBox.Show("Проект успешно обновлён");
            }
            else
            {
                MessageBox.Show("Проект не найден или данные не изменились");
            }
        }
        public static string ReadUserId()
        {
            string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Info");
            string path = Path.Combine(folder, "user.txt");

            string text = File.ReadAllText(path);
            return text;
        }
    }
}
