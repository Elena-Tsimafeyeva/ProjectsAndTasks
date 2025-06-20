using ProjectsAndTasks.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectsAndTasks.ViewModel
{
    internal class LoginVM : INotifyPropertyChanged
    {
        public ICommand LogInCommand { get; }
        public LoginVM()
        {
            LogInCommand = new RelayCommand(LogIn);
        }
        private void LogIn()
        {
            var window = new MainWindow();
            window.Show();
            CloseSpecificWindow();
        }
        public static void CloseSpecificWindow()
        {
            var windowToClose = Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(w => w.Title == "Login");
            windowToClose?.Close();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
