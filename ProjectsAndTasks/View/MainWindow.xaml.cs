using MahApps.Metro.Controls;
using ProjectsAndTasks.ViewModel;
namespace ProjectsAndTasks.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ProjectsVM();
        }
    }
}
