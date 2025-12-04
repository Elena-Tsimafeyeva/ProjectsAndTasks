using ProjectsAndTasks.Model;
using System.ComponentModel;
using System.Windows.Input;

namespace ProjectsAndTasks.ViewModel
{
    public class ProjectItemVM : ViewModelBase
    {
        private readonly ProjectItem project;
        private readonly Action<ProjectItemVM> openTasksAction;
        private readonly Action<ProjectItemVM> saveProjectСhangesAction;
        private readonly Action<ProjectItemVM> removeProjectAction;
        public string Title
        {
            get => project.Title; 
            set{
                project.Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        public string Description
        {
            get => project.Description;
            set
            {
                project.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        public int Progress
        {
            get => project.Progress;
            set
            {
                project.Progress = value;
                OnPropertyChanged(nameof(Progress));
            }
        }
        public ICommand OpenTasksCommand { get; }
        public ICommand SaveProjectChangesCommand { get; }
        public ICommand RemoveProjectCommand { get; }
        public ProjectItemVM(ProjectItem projectItem, Action<ProjectItemVM> openTasks, Action<ProjectItemVM> saveProjectChanges, Action<ProjectItemVM> removeProject)
        {
            project = projectItem;

            openTasksAction = openTasks;
            OpenTasksCommand = new RelayCommand(() => openTasksAction(this));

            saveProjectСhangesAction = saveProjectChanges;
            SaveProjectChangesCommand = new RelayCommand(() => saveProjectСhangesAction(this));

            removeProjectAction = removeProject;
            RemoveProjectCommand = new RelayCommand(() => removeProjectAction(this));

        }

    }
}

