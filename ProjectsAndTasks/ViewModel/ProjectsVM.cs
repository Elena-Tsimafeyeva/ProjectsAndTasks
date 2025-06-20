using ProjectsAndTasks.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjectsAndTasks.ViewModel
{
    internal class ProjectsVM : INotifyPropertyChanged
    {

        public ObservableCollection<ProjectItemVM> Projects { get; set; }

        public ICommand CreateProjectCommand { get; }

        public ProjectsVM()
        {
            Projects = new ObservableCollection<ProjectItemVM>();
            CreateProjectCommand = new RelayCommand(CreateProject);
        }

        private void CreateProject()
        {
            var project = new ProjectItem
            {
                Title = $"Проект {Projects.Count + 1}",
                Description = "Описание проекта...",
                Progress = 0
            };
            Projects.Add(new ProjectItemVM(project));
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
