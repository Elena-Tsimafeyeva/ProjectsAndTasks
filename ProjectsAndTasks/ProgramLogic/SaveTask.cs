using MongoDB.Bson;
using MongoDB.Driver;
using ProjectsAndTasks.MongoDb;
using ProjectsAndTasks.MongoDb.Model;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectsAndTasks.ProgramLogic
{
    public class SaveTask
    {
        private readonly MongoDbContext _context = new MongoDbContext();
        private readonly TaskCheck _taskCheck = new TaskCheck();
        public void SaveMyTask(string title, string description, int progress)
        {
            if (_taskCheck.IsTaskExists(title))
            {
                MessageBox.Show("Такое имя задачи уже есть или пустое значение");
                var saveTitle = new SaveTitle();
                saveTitle.SaveMyTitleAsync("");
            }
            else
            {
                string text = ReadProjectId();
                ObjectId projectId = ObjectId.Parse(text);

                var newProjectTask = new ProjectTask
                {
                    ProjectId = projectId,
                    TaskName = title,
                    TaskDescription = description,
                    TaskPercent = progress
                };
                _context.Tasks.InsertOne(newProjectTask);
            }
        }
        public async Task UpdateTaskAsync(string title, string description, int progress)
        {
            string text = ReadProjectId();
            ObjectId projectId = ObjectId.Parse(text);

            var filter = Builders<ProjectTask>.Filter.And(
                Builders<ProjectTask>.Filter.Eq(p => p.ProjectId, projectId),
                Builders<ProjectTask>.Filter.Eq(p => p.TaskName, title)
            );

            var update = Builders<ProjectTask>.Update
                .Set(p => p.TaskDescription, description)
                .Set(p => p.TaskPercent, progress);

            var result = await _context.Tasks.UpdateOneAsync(filter, update);

            if (result.ModifiedCount > 0)
            {
                MessageBox.Show("Задача успешно обновлена");
            }
            else
            {
                MessageBox.Show("Задача не найдена или данные не изменились");
            }
        }
        public static string ReadProjectId()
        {
            string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Info");
            string path = Path.Combine(folder, "projectId.txt");

            string text = File.ReadAllText(path);
            return text;
        }
    }
}
