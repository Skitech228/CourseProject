#region Using derectives

using System.Windows.Input;
using GalaSoft.MvvmLight;
using Prism.Commands;

#endregion

namespace CourseProject.UI.Pages
{
    public class SpotifyControlViewModel : ViewModelBase, IPageViewModel
    {
        private ICommand _goToServiceControl;

        public ICommand GoToServiceControl
        {
            get { return _goToServiceControl ??= new DelegateCommand(ServiceNotify); }
        }

        private void ServiceNotify()
        {
            Mediator.Notify("GoToServiceControl", "");
        }

        private ICommand _goToSpotifyGenres;
        
        public ICommand GoToSpotifyGenres
        {
            get { return _goToSpotifyGenres ??= new DelegateCommand(ServiceSpotifyGenres); }
        }

        private void ServiceSpotifyGenres()
        {
            Mediator.Notify("GoToSpotifyGenres", "");
        }

        private ICommand _goToSpotifySponsors;

        public ICommand GoToSpotifySponsors
        {
            get { return _goToSpotifySponsors ??= new DelegateCommand(ServiceSpotifySponsors); }
        }

        private void ServiceSpotifySponsors()
        {
            Mediator.Notify("GoToSpotifySponsors", "");
        }

        private ICommand _goToSpotifySubscriptions;

        public ICommand GoToSpotifySubscriptions
        {
            get { return _goToSpotifySubscriptions ??= new DelegateCommand(ServiceSpotifySubscriptions); }
        }

        private void ServiceSpotifySubscriptions()
        {
            Mediator.Notify("GoToSpotifySubscriptions", "");
        }
    }
}