using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectsAndTasks.Model
{
    internal class ProjectItem : INotifyPropertyChanged
    {
        private string title;
        private string description;
        private int progress;

        public string Title
        {
            get { return title; }
            set 
            { 
                title = value; 
                OnPropertyChanged(nameof(Title)); 
            }
        }

        public string Description
        {
            get { return description; }
            set 
            { 
                description = value; 
                OnPropertyChanged(nameof(Description)); 
            }
        }

        public int Progress
        {
            get {return progress;}
            set 
            { 
                progress = value; 
                OnPropertyChanged(nameof(Progress)); 
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
