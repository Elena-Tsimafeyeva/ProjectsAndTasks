using System.Windows;
using System.Windows.Input;
using ProjectsAndTasks.Service;
using ProjectsAndTasks.View;

namespace ProjectsAndTasks.ViewModel
{
    internal class TopMenuVM : ViewModelBase
    {
        public ICommand ProfileCommand { get; }
        public ICommand ExitProfileCommand { get; }
        public ICommand ChangeToRussianCommand { get; }
        public ICommand ChangeToEnglishCommand { get; }
        public TopMenuVM()
        {
            ProfileCommand = new RelayCommand(OpenProfile);
            ExitProfileCommand = new RelayCommand(ExitProfile);
            ChangeToRussianCommand = new RelayCommand(ChangeToRussian);
            ChangeToEnglishCommand = new RelayCommand(ChangeToEnglish);
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
        private void ChangeToRussian()
        {
            LocalizationService.SetRussian();
        }

        private void ChangeToEnglish()
        {
            LocalizationService.SetEnglish();
        }


    }
}
