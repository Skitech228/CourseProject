#region Using derectives

using System.Windows.Input;
using GalaSoft.MvvmLight;
using Prism.Commands;

#endregion

namespace CourseProject.UI.Pages
{
    public class ServiceControlViewModel : ViewModelBase, IPageViewModel
    {
        private ICommand _goToSpotifyControl;

        private ICommand _goToYouTubeMusicControl;

        private ICommand _goToGoogleMusicControl;

        private ICommand _goToYandexMusicControl;

        private ICommand _goToAppleMusicControl;

        public ICommand GoToSpotifyControl
        {
            get { return _goToSpotifyControl ??= new DelegateCommand(SpotifyNotify); }
        }

        public ICommand GoToYouTubeMusicControl
        {
            get { return _goToYouTubeMusicControl ??= new DelegateCommand(YouTubeNotify); }
        }

        public ICommand GoToGoogleMusicControl
        {
            get { return _goToGoogleMusicControl ??= new DelegateCommand(GoogleNotify); }
        }

        public ICommand GoToYandexMusicControl
        {
            get { return _goToYandexMusicControl ??= new DelegateCommand(YandexNotify); }
        }

        public ICommand GoToAppleMusicControl
        {
            get { return _goToAppleMusicControl ??= new DelegateCommand(AppleNotify); }
        }

        private void SpotifyNotify()
        {
            Mediator.Notify("GoToSpotifyControl", "");
        }

        private void YouTubeNotify()
        {
            Mediator.Notify("GoToYouTubeMusicControl", "");
        }

        private void GoogleNotify()
        {
            Mediator.Notify("GoToGoogleMusicControl", "");
        }

        private void YandexNotify()
        {
            Mediator.Notify("GoToYandexMusicControl", "");
        }

        private void AppleNotify()
        {
            Mediator.Notify("GoToAppleMusicControl", "");
        }
    }
}