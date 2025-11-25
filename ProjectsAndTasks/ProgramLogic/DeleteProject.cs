using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using ProjectsAndTasks.MongoDb;
using ProjectsAndTasks.MongoDb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectsAndTasks.ProgramLogic
{
    public class DeleteProject
    {
        private readonly MongoDbContext _context = new MongoDbContext();
        public async Task RemoveMyProject(string title)
        {
            string text = SaveTask.ReadProjectId();
            ObjectId projectId = ObjectId.Parse(text);

            var filter = Builders<ProjectTask>.Filter.And(
            Builders<ProjectTask>.Filter.Eq(p => p.ProjectId, projectId),
            Builders<ProjectTask>.Filter.Eq(p => p.TaskName, title));

            var result = await _context.Tasks.DeleteOneAsync(filter);

            if (result.DeletedCount > 0)
            {
                MessageBox.Show("Задача успешно удалена");
            }
            else
            {
                MessageBox.Show("Задача не найдена");
            }
        }
    }
}
