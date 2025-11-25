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
            string text = SaveProject.ReadUserId();
            ObjectId personId = ObjectId.Parse(text);

            var filter = Builders<Project>.Filter.And(
            Builders<Project>.Filter.Eq(p => p.PersonId, personId),
            Builders<Project>.Filter.Eq(p => p.ProjectName, title));

            var result = await _context.Projects.DeleteOneAsync(filter);

            if (result.DeletedCount > 0)
            {
                MessageBox.Show("Проект успешно удалён");
            }
            else
            {
                MessageBox.Show("Проект не найден");
            }
        }
    }
}
