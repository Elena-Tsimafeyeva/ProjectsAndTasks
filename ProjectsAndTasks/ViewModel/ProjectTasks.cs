using MongoDB.Bson;
using MongoDB.Driver;
using ProjectsAndTasks.Model;
using ProjectsAndTasks.MongoDb;
using ProjectsAndTasks.MongoDb.Model;
using ProjectsAndTasks.ProgramLogic;
using ProjectsAndTasks.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectsAndTasks.ViewModel
{
    public class ProjectTasks : ViewModelBase
    {
        private readonly MongoDbContext _context = new MongoDbContext();
        public ObservableCollection<ProjectTasksItemVM> NewTasks { get; set; }
        public ICommand CreateNewTaskCommand { get; }
        public ICommand ReturnToMainPageCommand { get; }
        public ProjectTasks()
        {
            NewTasks = new ObservableCollection<ProjectTasksItemVM>();
            CreateNewTaskCommand = new RelayCommand(CreateTask);
            ReturnToMainPageCommand = new RelayCommand(ReturnMainWindow);
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
                    var taskItem = new TaskItem
                    {
                        Title = task.TaskName,
                        Description = task.TaskDescription,
                        Progress = task.TaskPercent
                    };
                    NewTasks.Add(new ProjectTasksItemVM(taskItem, SaveTaskChanges, RemoveTask));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки задач: {ex.Message}");
            }
        }
        private async void CreateTask()
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
                var taskItem = new TaskItem
                {
                    Title = $"{title}",
                    Description = "Описание проекта...",
                    Progress = 0
                };
                var taskItemVM = new ProjectTasksItemVM(taskItem, SaveTaskChanges, RemoveTask);
                NewTasks.Add(taskItemVM);
                var saveTask = new SaveTask();
                saveTask.SaveMyTask(taskItem.Title, taskItem.Description, taskItem.Progress);

                var projectPercent = new ProjectPercent();
                double projPercent = projectPercent.CalculationOfProjectPercentages();
                int percent = (int)projPercent;
                MessageBox.Show($"Процент проекта: {percent}");
                await projectPercent.UpdateProjectPercentAsync(percent);
            }
        }
        private async void SaveTaskChanges(ProjectTasksItemVM task)
        {
            var saveTask = new SaveTask();
            await saveTask.UpdateTaskAsync(task.Title, task.Description, task.Progress);

            var projectPercent = new ProjectPercent();
            double projPercent = projectPercent.CalculationOfProjectPercentages();
            int percent = (int)projPercent;
            MessageBox.Show($"Процент проекта: {percent}");
            await projectPercent.UpdateProjectPercentAsync(percent);
        }
        private async void RemoveTask(ProjectTasksItemVM task)
        {
            NewTasks.Remove(task);
            var deleteTask = new DeleteTask();
            await deleteTask.RemoveMyTask(task.Title);

            var projectPercent = new ProjectPercent();
            double projPercent = projectPercent.CalculationOfProjectPercentages();
            int percent = (int)projPercent;
            MessageBox.Show($"Процент проекта: {percent}");
            await projectPercent.UpdateProjectPercentAsync(percent);
        }
        private void ReturnMainWindow()
        {
            var window = new MainWindow();
            window.Show();
            CloseSpecificWindow();
        }
        public static void CloseSpecificWindow()
        {
            var windowToClose = Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(w => w.Title == "Tasks");
            windowToClose?.Close();
        }
       
    }
}
