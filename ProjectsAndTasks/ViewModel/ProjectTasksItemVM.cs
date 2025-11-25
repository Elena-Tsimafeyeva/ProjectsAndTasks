using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectsAndTasks.ViewModel
{
    public class ProjectTasksItemVM : INotifyPropertyChanged
    {
        private string title;
        private string description;
        private int progress;
        private readonly Action<ProjectTasksItemVM> saveTaskСhangesAction;
        private readonly Action<ProjectTasksItemVM> removeTaskAction;
        public string Title
        {
            get => title;
            set
            {
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
        public ICommand SaveTaskChangesCommand { get; }
        public ICommand RemoveTaskCommand { get; }
        public ProjectTasksItemVM(Action<ProjectTasksItemVM> saveTaskChanges, Action<ProjectTasksItemVM> removeTask)
        {
            saveTaskСhangesAction = saveTaskChanges;
            SaveTaskChangesCommand = new RelayCommand(() => saveTaskСhangesAction(this));

            removeTaskAction = removeTask;
            RemoveTaskCommand = new RelayCommand(() => removeTaskAction(this));

        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    
}
}
