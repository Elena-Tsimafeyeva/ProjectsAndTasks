using ProjectsAndTasks.Interfaces;
using System.Windows;

namespace ProjectsAndTasks.Service
{
    ///<summary>
    /// Implementation of the IMessageBox interface for displaying messages and confirmation dialogs.
    ///</summary>
    internal class MessageBoxService: IMessageBox
    {
        public void Show(string message)
        {
            MessageBox.Show(message);
        }

        public MessageBoxResult ShowConfirmation(string message, string caption)
        {
            return MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
        }
    }
}
