using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using ProjectsAndTasks.MongoDb.Model;
using ProjectsAndTasks.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsAndTasks.ProgramLogic
{
    public class SaveTitle
    {
        public async Task SaveMyTitleAsync(string title) {
            string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Info");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            string path = Path.Combine(folder, "title.txt");
            await File.WriteAllTextAsync(path, title);
        }
    }
}
