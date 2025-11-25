using ProjectsAndTasks.Model;
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
    public class ProjectItemVM : INotifyPropertyChanged
    {
        private string title;
        private string description;
        private int progress;
        private readonly Action<ProjectItemVM> openTasksAction;
        private readonly Action<ProjectItemVM> saveProjectСhangesAction;
        private readonly Action<ProjectItemVM> removeProjectAction;
        public string Title
        {
            get => title; 
            set{
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        public int Progress
        {
            get => progress;
            set
            {
                progress = value;
                OnPropertyChanged(nameof(Progress));
            }
        }
        public ICommand OpenTasksCommand { get; }
        public ICommand SaveProjectChangesCommand { get; }
        public ICommand RemoveProjectCommand { get; }
        public ProjectItemVM(Action<ProjectItemVM> openTasks, Action<ProjectItemVM> saveProjectChanges, Action<ProjectItemVM> removeProject)
        {
            openTasksAction = openTasks;
            OpenTasksCommand = new RelayCommand(() => openTasksAction(this));

            saveProjectСhangesAction = saveProjectChanges;
            SaveProjectChangesCommand = new RelayCommand(() => saveProjectСhangesAction(this));

            removeProjectAction = removeProject;
            RemoveProjectCommand = new RelayCommand(() => removeProjectAction(this));

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

