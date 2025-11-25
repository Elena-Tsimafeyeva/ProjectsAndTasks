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
using static System.Net.Mime.MediaTypeNames;

namespace ProjectsAndTasks.ProgramLogic
{
    public class ProjectPercent
    {
        private readonly MongoDbContext _context = new MongoDbContext();
        public double CalculationOfProjectPercentages()
        {
            string text = SaveTask.ReadProjectId();
            ObjectId projectId = ObjectId.Parse(text);
            var filter = Builders<ProjectTask>.Filter.Eq(t => t.ProjectId, projectId);
            var tasks = _context.Tasks.Find(filter).ToList();
            if (tasks.Count == 0)
                return 0;
            double avg = tasks.Average(t => t.TaskPercent);
            return avg;
        }
        public async Task UpdateProjectPercentAsync(int percent)
        {
            string text = SaveTask.ReadProjectId();
            ObjectId projectId = ObjectId.Parse(text);
            var filter = Builders<Project>.Filter.Eq(p => p.Id, projectId);
            var update = Builders<Project>.Update.Set(p => p.Percent, percent);
            await _context.Projects.UpdateOneAsync(filter, update);
        }
    }
}
