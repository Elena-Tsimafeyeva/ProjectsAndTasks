using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using ProjectsAndTasks.MongoDb;
using ProjectsAndTasks.MongoDb.Model;
using ProjectsAndTasks.Repository;
using ProjectsAndTasks.Interfaces;
using ProjectsAndTasks.MongoDb.Model.InterfacesModel;
using ProjectsAndTasks.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsAndTasks.ProgramLogic
{
    public class SaveProjectId
    {
        private readonly MongoDbContext _context = new MongoDbContext();
        public async Task SaveMyProjectId(string title)
        {
            string text = SaveProject.ReadUserId();
            ObjectId personId = ObjectId.Parse(text);
            var filter = Builders<Project>.Filter.And(
                Builders<Project>.Filter.Eq(p => p.PersonId, personId),
                Builders<Project>.Filter.Eq(p => p.ProjectName, title)
            );

            var project = await _context.Projects.Find(filter).FirstOrDefaultAsync();

            string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Info");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            string path = Path.Combine(folder, "projectID.txt");

            if (project != null)
            {
                await File.WriteAllTextAsync(path, project.Id.ToString());
            }
            else
            {
                await File.WriteAllTextAsync(path, "Проект не найден");
            }
        }
    }
}
