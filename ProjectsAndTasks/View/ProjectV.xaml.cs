using MahApps.Metro.Controls;
using ProjectsAndTasks.ViewModel;

namespace ProjectsAndTasks.View
{
    /// <summary>
    /// Interaction logic for Project.xaml
    /// </summary>
    public partial class ProjectV : MetroWindow
    {
        public ProjectV(ProjectTasks tasksVM)
        {
            InitializeComponent();
            DataContext = tasksVM;
        }
    }
}
