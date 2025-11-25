using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ProjectsAndTasks.View;

namespace ProjectsAndTasks.ViewModel
{
    internal class TopMenuVM : INotifyPropertyChanged
    {
        public ICommand ProfileCommand { get; }
        public ICommand ExitProfileCommand { get; }
        public TopMenuVM()
        {
            ProfileCommand = new RelayCommand(OpenProfile);
            ExitProfileCommand = new RelayCommand(ExitProfile);
        }
        private void OpenProfile()
        {
            var window = new Profile();
            window.ShowDialog();
        }
    
        private void ExitProfile()
        {
            var loginWindow = new Login();
            foreach (var window in Application.Current.Windows.OfType<Window>().ToList())
            {
                if (window != loginWindow)
                    window.Close();
            }
            loginWindow.ShowDialog();
        }
       
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
