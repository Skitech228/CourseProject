#region Using derectives

using System.Threading.Tasks;
using System.Windows.Input;

#endregion

namespace CourseProject.Application.AsyncConmands
{
    internal interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync();

        bool CanExecute();
    }
}