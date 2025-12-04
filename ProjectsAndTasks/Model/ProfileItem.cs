using System.ComponentModel;

namespace ProjectsAndTasks.Model
{
    public class ProfileItem : INotifyPropertyChanged
    {
        private string firstName;
        private string lastName;
        private string mail;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        public string Mail
        {
            get { return mail; }
            set
            {
                mail = value;
                OnPropertyChanged(nameof(Mail));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
