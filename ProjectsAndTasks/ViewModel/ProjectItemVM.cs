using ProjectsAndTasks.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectsAndTasks.ViewModel
{
    internal class ProjectItemVM : INotifyPropertyChanged
    {
        public ObservableCollection<TaskItem> Tasks { get; }
        public ICommand CreateTaskCommand { get; }
        public ProjectItemVM()
        {
            Tasks = new ObservableCollection<TaskItem>();
            CreateTaskCommand = new RelayCommand(CreateTask);
        }
        private void CreateTask()
        {
            Tasks.Add(new TaskItem
            {
                Title = $"Задача {Tasks.Count + 1}",
                Description = "Описание задачи...",
                Progress = 0
            });
            OnPropertyChanged(nameof(Tasks));
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
