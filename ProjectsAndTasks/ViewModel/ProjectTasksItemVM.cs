using System.ComponentModel;
using ProjectsAndTasks.Model;
using System.Windows.Input;

namespace ProjectsAndTasks.ViewModel
{
    public class ProjectTasksItemVM : ViewModelBase
    {
        private readonly TaskItem task;
        private readonly Action<ProjectTasksItemVM> saveTaskСhangesAction;
        private readonly Action<ProjectTasksItemVM> removeTaskAction;
        public string Title
        {
            get => task.Title;
            set
            {
                task.Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        public string Description
        {
            get => task.Description;
            set
            {
                task.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        public int Progress
        {
            get => task.Progress;
            set
            {
                task.Progress = value;
                OnPropertyChanged(nameof(Progress));
            }
        }
        public ICommand SaveTaskChangesCommand { get; }
        public ICommand RemoveTaskCommand { get; }
        public ProjectTasksItemVM(TaskItem taskItem, Action<ProjectTasksItemVM> saveTaskChanges, Action<ProjectTasksItemVM> removeTask)
        {
            task = taskItem;

            saveTaskСhangesAction = saveTaskChanges;
            SaveTaskChangesCommand = new RelayCommand(() => saveTaskСhangesAction(this));

            removeTaskAction = removeTask;
            RemoveTaskCommand = new RelayCommand(() => removeTaskAction(this));

        }
        
}
}
