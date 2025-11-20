using Microsoft.Win32;
using ProjectsAndTasks.MongoDb;
using ProjectsAndTasks.MongoDb.Model;
using ProjectsAndTasks.ProgramLogic;
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
        private readonly MongoDbContext _context = new MongoDbContext();
        private readonly LoginCheck _loginCheck = new LoginCheck();

        private string _login;
        public string Login
        {
            get => _login;
            set { 
                _login = value; 
                OnPropertyChanged(nameof(Login));
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set { 
                _password = value; 
                OnPropertyChanged(nameof(Password)); 
            }
        }
        public ICommand LogInCommand { get; }
        public ICommand RegisterCommand { get; }
        public LoginVM()
        {
            LogInCommand = new RelayCommand(LogIn);
            RegisterCommand = new RelayCommand(Register);
        }
        private void LogIn()
        {
            MessageBox.Show("LogIn");
            OpenMainWindow();
        }
        /// <summary>
        /// E.A.T. 14-November-2025
        /// User registration.
        /// </summary>
        private void Register()
        {
            if (_loginCheck.IsLoginExists(Login))
            {
                MessageBox.Show("Такой логин уже существует или поле пустое");
            }
            else
            {
                var newPerson = new Person
                {
                    Login = this.Login,
                    Password = this.Password
                };

                var context = new MongoDbContext();
                context.Persons.InsertOne(newPerson);

                MessageBox.Show("Пользователь успешно зарегистрирован!");
                MessageBox.Show("Register");
                OpenMainWindow();
            }
        }
        private void OpenMainWindow()
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
