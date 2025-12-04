using ProjectsAndTasks.Model;
using ProjectsAndTasks.MongoDb.Model;
using ProjectsAndTasks.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ProjectsAndTasks.ProgramLogic;
using System.Windows;
using System.Windows.Input;
using MongoDB.Bson;
using MongoDB.Driver;
using ProjectsAndTasks.MongoDb;

namespace ProjectsAndTasks.ViewModel
{
    public class ProjectsVM : ViewModelBase
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
                    var projectItem = new ProjectItem
                    {
                        Title = proj.ProjectName,
                        Description = proj.ProjectDescription,
                        Progress = proj.Percent
                    };
                    Projects.Add(new ProjectItemVM(projectItem, OpenTasks, SaveProjectChanges, RemoveProject));
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
                var projectItem = new ProjectItem
                {
                    Title = title,
                    Description = "Описание проекта...",
                    Progress = 0
                };
                var projectItemVM = new ProjectItemVM(projectItem, OpenTasks, SaveProjectChanges, RemoveProject);
                Projects.Add(projectItemVM);
                var saveProject = new SaveProject();
                saveProject.SaveMyProject(projectItem.Title, projectItem.Description, projectItem.Progress);
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
        
    }
}
