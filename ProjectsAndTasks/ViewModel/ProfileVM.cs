using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ProjectsAndTasks.Model;

namespace ProjectsAndTasks.ViewModel
{
    public class ProfileVM : ViewModelBase
    {
        private readonly ProfileItem profileItem;
        public ICommand SaveProfileInfoCommand { get; }
        public ProfileVM()
        {
            SaveProfileInfoCommand = new RelayCommand(SaveProfileInfo);
        }
        public void SaveProfileInfo()
        {

        }
    }
}
