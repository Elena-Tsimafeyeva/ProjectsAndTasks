using ProjectsAndTasks.ProgramLogic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace ProjectsAndTasks.ViewModel
{
    public class InputTitleVM : ViewModelBase
    {
        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        public ICommand SaveTitleCommand { get; }
        public InputTitleVM() {
            SaveTitleCommand = new RelayCommand(SaveTheEnteredTitle);
        }
        private async void SaveTheEnteredTitle()
        {
            var saveTitle = new SaveTitle();
            await saveTitle.SaveMyTitleAsync(Title);
            CloseSpecificWindow();
        }
        public static void CloseSpecificWindow()
        {
            var windowToClose = Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(w => w.Title == "InputTitle");
            windowToClose?.Close();
        }
    }
}
