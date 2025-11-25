using ProjectsAndTasks.Model;
using ProjectsAndTasks.MongoDb.Model;
using ProjectsAndTasks.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ProjectsAndTasks.ProgramLogic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using ProjectsAndTasks.MongoDb;
using ProjectsAndTasks.MongoDb.Model;

namespace ProjectsAndTasks.ViewModel
{
    public class ProjectsVM : INotifyPropertyChanged
    {
        private readonly MongoDbContext _context = new MongoDbContext();
        public ObservableCollection<ProjectItemVM> Projects { get; set; }
        public ICommand CreateProjectCommand { get; }
        public ProjectsVM()
        {
            Projects = new ObservableCollection<ProjectItemVM>();
            CreateProjectCommand = new RelayCommand(CreateProject);
            LoadProjectsFromDb();
        }
        private void LoadProjectsFromDb()
        {
            try
            {
                string text = SaveProject.ReadUserId();
                ObjectId personId = ObjectId.Parse(text);

                var filter = Builders<Project>.Filter.Eq(p => p.PersonId, personId);
                var projectsFromDb = _context.Projects.Find(filter).ToList();

                foreach (var proj in projectsFromDb)
                {
                    Projects.Add(new ProjectItemVM(OpenTasks, SaveProjectChanges, RemoveProject)
                    {
                        Title = proj.ProjectName,
                        Description = proj.ProjectDescription,
                        Progress = proj.Percent
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки проектов: {ex.Message}");
            }
        }
        private void CreateProject()
        {
            var window = new InputTitle();
            window.ShowDialog();
            string title = OutputTitle.ReadTitle();
            if (!string.IsNullOrWhiteSpace(title))
            {
                var _projectCheck = new ProjectCheck();
                if (_projectCheck.IsProjectExists(title))
                {
                    MessageBox.Show("Такое имя проекта уже есть или пустое значение");
                    return; 
                }
                var project = new ProjectItemVM(OpenTasks, SaveProjectChanges, RemoveProject)
                {
                    Title = $"{title}",
                    Description = "Описание проекта...",
                    Progress = 0
                };
            Projects.Add(project);
            var saveProject = new SaveProject();
            saveProject.SaveMyProject(project.Title, project.Description, project.Progress);
            }
        }
        private async void OpenTasks(ProjectItemVM project)
        {
            MessageBox.Show($"Открываю задачи проекта: {project.Title}");
            var saveProjectId = new SaveProjectId();
            await saveProjectId.SaveMyProjectId(project.Title);
            var tasksVm = new ProjectTasks();  
            var window = new ProjectV(tasksVm);
            window.Show();
            CloseSpecificWindow();
        }
        private async void SaveProjectChanges(ProjectItemVM project)
        {
            MessageBox.Show($"Save changes {project.Title}");
            var saveProject = new SaveProject();
            await saveProject.UpdateProjectAsync(project.Title, project.Description, project.Progress);
        }
        private async void RemoveProject(ProjectItemVM project)
        {
            Projects.Remove(project);
            var deleteProject = new DeleteProject();
            await deleteProject.RemoveMyProject(project.Title);
        }
        public static void CloseSpecificWindow()
        {
            var windowToClose = Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(w => w.Title == " Projects and Tasks");
            windowToClose?.Close();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
