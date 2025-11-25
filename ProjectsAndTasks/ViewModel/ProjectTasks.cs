using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using ProjectsAndTasks.Model;
using ProjectsAndTasks.MongoDb;
using ProjectsAndTasks.MongoDb.Model;
using ProjectsAndTasks.ProgramLogic;
using ProjectsAndTasks.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectsAndTasks.ViewModel
{
    public class ProjectTasks
    {
        private readonly MongoDbContext _context = new MongoDbContext();
        public ObservableCollection<ProjectTasksItemVM> NewTasks { get; set; }
        public ICommand CreateNewTaskCommand { get; }
        public ProjectTasks()
        {
            NewTasks = new ObservableCollection<ProjectTasksItemVM>();
            CreateNewTaskCommand = new RelayCommand(CreateTask);
            LoadTasksFromDb();
        }
        private void LoadTasksFromDb()
        {
            try
            {
                string text = SaveTask.ReadProjectId();
                ObjectId projectId = ObjectId.Parse(text);

                var filter = Builders<ProjectTask>.Filter.Eq(p => p.ProjectId, projectId);
                var tasksFromDb = _context.Tasks.Find(filter).ToList();

                foreach (var task in tasksFromDb)
                {
                    NewTasks.Add(new ProjectTasksItemVM(SaveTaskChenges, RemoveTask)
                    {
                        Title = task.TaskName,
                        Description = task.TaskDescription,
                        Progress = task.TaskPercent
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки задач: {ex.Message}");
            }
        }
        private void CreateTask()
        {
            var window = new InputTitle();
            window.ShowDialog();
            string title = OutputTitle.ReadTitle();
            if (!string.IsNullOrWhiteSpace(title))
            {
                var _taskCheck = new TaskCheck();
                if (_taskCheck.IsTaskExists(title))
                {
                    MessageBox.Show("Такое имя задачи уже есть или пустое значение");
                    return;
                }
                var task = new ProjectTasksItemVM(SaveTaskChenges, RemoveTask)
                {
                    Title = $"{title}",
                    Description = "Описание проекта...",
                    Progress = 0
                };
                NewTasks.Add(task);
                var saveTask = new SaveTask();
                saveTask.SaveMyTask(task.Title, task.Description, task.Progress);
            }
        }
        private void SaveTaskChenges(ProjectTasksItemVM task)
        {
            var saveTask = new SaveTask();
            saveTask.UpdateTaskAsync(task.Title, task.Description, task.Progress);
        }
        private void RemoveTask(ProjectTasksItemVM task)
        {

        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
