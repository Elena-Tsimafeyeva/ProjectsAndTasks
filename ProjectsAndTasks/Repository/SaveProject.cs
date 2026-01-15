using ProjectsAndTasks.Interfaces;
using ProjectsAndTasks.ProgramLogic;
using ProjectsAndTasks.MongoDb.Model;
using System.IO;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ProjectsAndTasks.Repository
{
    public class SaveProject
    {
        private readonly IMongoDbContext _context;
        private readonly IMessageBox _messageBox;
        private readonly IProjectCheck _projectCheck;
        public SaveProject(IMongoDbContext context, IMessageBox messageBox, IProjectCheck projectCheck)
        {
            _context = context;
            _messageBox = messageBox;
            _projectCheck = projectCheck;
        }

        public void SaveMyProject(string title, string description, int progress)
        {
            if (_projectCheck.IsProjectExists(title))
            {
                _messageBox.Show("Такое имя проекта уже есть или пустое значение");
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
                _messageBox.Show("Проект успешно обновлён");
            }
            else
            {
                _messageBox.Show("Проект не найден или данные не изменились");
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
