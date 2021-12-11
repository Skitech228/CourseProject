#region Using derectives

using System.Windows.Input;
using GalaSoft.MvvmLight;
using Prism.Commands;

#endregion

namespace CourseProject.UI.Pages
{
    public class AppleMusicControlViewModel : ViewModelBase, IPageViewModel {
        private ICommand _goToServiceControl;

        public ICommand GoToServiceControl
        {
            get { return _goToServiceControl ??= new DelegateCommand(ServiceNotify); }
        }

        private void ServiceNotify()
        {
            Mediator.Notify("GoToServiceControl", "");
        }
    }
}