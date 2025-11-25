using ProjectsAndTasks.MongoDb;
using ProjectsAndTasks.MongoDb.Model;
using ProjectsAndTasks.ProgramLogic;
using ProjectsAndTasks.View;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace ProjectsAndTasks.ViewModel
{
    public class LoginVM : INotifyPropertyChanged
    {
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
        /// <summary>
        /// E.A.T. 24-November-2025
        /// User logIn.
        /// </summary>
        private async void LogIn()
        {
            if (_loginCheck.IsLoginExists(Login))
            {
                if (_loginCheck.ValidatePassword(this.Login, this.Password))
                {
                    MessageBox.Show($"LogIn. Welcome, {this.Login}");
                    SaveUser saveUser = new SaveUser();
                    await saveUser.SaveUserFileAsync(this.Login);
                    OpenMainWindow();
                }
                else
                {
                    MessageBox.Show("Неверный пароль");
                }
            }
            else
            {
                MessageBox.Show("Такого пользователя нет. Зарегестрируйтесь или исправьте Login");
            }
        }
        /// <summary>
        /// E.A.T. 14-November-2025
        /// User registration.
        /// </summary>
        private async void Register()
        {
            if (_loginCheck.IsLoginExists(Login))
            {
                MessageBox.Show("Такой логин уже существует или поле пустое");
            }
            else
            {
                SaveUser saveUser = new SaveUser();
                await saveUser.RegisterUser(this.Login, this.Password);

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
