using System.Windows;

namespace ProjectsAndTasks.Interfaces
{
    ///<summary>
    /// Interface for displaying messages and confirmation dialogs to the user.
    ///</summary>
    public interface IMessageBox
    {
        void Show(string message);
        MessageBoxResult ShowConfirmation(string message, string caption);
    }
}
