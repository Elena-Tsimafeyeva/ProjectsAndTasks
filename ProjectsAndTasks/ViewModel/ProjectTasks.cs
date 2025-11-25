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
    public class ProjectTasks
    {
        public ObservableCollection<TaskItem> NewTasks { get; set; }
        public ICommand CreateNewTaskCommand { get; }
        public ProjectTasks()
        {
            NewTasks = new ObservableCollection<TaskItem>();
            CreateNewTaskCommand = new RelayCommand(CreateTask);
        }
        private void CreateTask()
        {
            NewTasks.Add(new TaskItem
            {
                Title = $"Задача {NewTasks.Count + 1}",
                Description = "Описание задачи...",
                Progress = 0
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
