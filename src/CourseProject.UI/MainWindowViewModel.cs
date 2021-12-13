#region Using derectives

using System.Collections.Generic;
using System.Linq;
using CourseProject.Shared.IEntityService;
using CourseProject.UI.Pages;
using CourseProject.UI.ViewModel;
using GalaSoft.MvvmLight;
using Prism.Commands;

#endregion

namespace CourseProject.UI
{
    public class MainWindowViewModel : ViewModelBase
    {
        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;

        // private PreOrdersViewModel _preOrders;IPreOrderService preOrder,
        private ArtistViewModel _artists;
        private GenreViewModel _genres;
        private ServiceViewModel _services;
        private SponsorViewModel _sponsors;
        private SubscriptionViewModel _subscriptions;
        private TrackViewModel _tracks;

        public MainWindowViewModel(IArtistService artist,
                                   IGenreService genre,
                                   IServiceService service,
                                   ISponsorService sponsor,
                                   ISubscriptionService subscription,
                                   ITrackService track)
        {
            PageViewModels.Add(new ServiceControlViewModel());
            PageViewModels.Add(new SpotifyControlViewModel());
            PageViewModels.Add(new YouTubeMusicControlViewModel());
            PageViewModels.Add(new GoogleMusicControlViewModel());
            PageViewModels.Add(new YandexMusicControlViewModel());
            PageViewModels.Add(new AppleMusicControlViewModel());

            PageViewModels.Add(new SpotifyGenresViewModel(genre,track));
            PageViewModels.Add(new SpotifySponsorsViewModel(sponsor));
            PageViewModels.Add(new SpotifySubscriptionsViewModel(subscription));

            PageViewModels.Add(new UserPageViewModel());
            PageViewModels.Add(new AdminPageViewModel(artist, genre, service, sponsor, subscription, track));

            CurrentPageViewModel = PageViewModels[0];
            AdminPageViewModel = PageViewModels[9];

            Mediator.Subscribe("GoToServiceControl", OnGoServiceControl);
            Mediator.Subscribe("GoToSpotifyControl", OnGoSpotifyControl);
            Mediator.Subscribe("GoToYouTubeMusicControl", OnGoYouTubeMusicControl);
            Mediator.Subscribe("GoToGoogleMusicControl", OnGoGoogleMusicControl);
            Mediator.Subscribe("GoToYandexMusicControl", OnGoYandexMusicControl);
            Mediator.Subscribe("GoToAppleMusicControl", OnGoAppleMusicControl);

            Mediator.Subscribe("GoToSpotifyGenres", OnGoSpotifyGenres);
            Mediator.Subscribe("GoToSpotifySponsors", OnGoSpotifySponsors);
            Mediator.Subscribe("GoToSpotifySubscriptions", OnGoSpotifySubscriptions);

            Mediator.Subscribe("GoToUserForm", OnGoUserForm);
            Mediator.Subscribe("GoToAdminForm", OnGoAdminForm);



            ArtistsContext = new ArtistViewModel(artist);
            GenresContext = new GenreViewModel(genre);
            ServicesContext = new ServiceViewModel(service);
            SponsorsContext = new SponsorViewModel(sponsor);
            SubscriptionsContext = new SubscriptionViewModel(subscription);
            TracksContext = new TrackViewModel(track);
        }

        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<IPageViewModel>();

                return _pageViewModels;
            }
        }

        public IPageViewModel CurrentPageViewModel
        {
            get => _currentPageViewModel;
            set { Set(() => CurrentPageViewModel, ref _currentPageViewModel, value); }
        }

        public IPageViewModel AdminPageViewModel
        {
            get => _adminPageViewModel;
            set { Set(() => AdminPageViewModel, ref _adminPageViewModel, value); }
        }

        public ArtistViewModel ArtistsContext
        {
            get => _artists;
            set { Set(() => ArtistsContext, ref _artists, value); }
        }

        public GenreViewModel GenresContext
        {
            get => _genres;
            set { Set(() => GenresContext, ref _genres, value); }
        }

        public ServiceViewModel ServicesContext
        {
            get => _services;
            set { Set(() => ServicesContext, ref _services, value); }
        }

        public SponsorViewModel SponsorsContext
        {
            get => _sponsors;
            set { Set(() => SponsorsContext, ref _sponsors, value); }
        }

        public SubscriptionViewModel SubscriptionsContext
        {
            get => _subscriptions;
            set { Set(() => SubscriptionsContext, ref _subscriptions, value); }
        }

        public TrackViewModel TracksContext
        {
            get => _tracks;
            set { Set(() => TracksContext, ref _tracks, value); }
        }

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                    .FirstOrDefault(vm => vm == viewModel);
        }

        private void ChangeAdminViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            AdminPageViewModel = PageViewModels
                    .FirstOrDefault(vm => vm == viewModel);
        }


        private void OnGoServiceControl(object obj)
        {
            ChangeViewModel(PageViewModels[0]);
        }

        private void OnGoSpotifyControl(object obj)
        {
            ChangeViewModel(PageViewModels[1]);
        }

        private void OnGoYouTubeMusicControl(object obj)
        {
            ChangeViewModel(PageViewModels[2]);
        }

        private void OnGoGoogleMusicControl(object obj)
        {
            ChangeViewModel(PageViewModels[3]);
        }

        private void OnGoYandexMusicControl(object obj)
        {
            ChangeViewModel(PageViewModels[4]);
        }

        private void OnGoAppleMusicControl(object obj)
        {
            ChangeViewModel(PageViewModels[5]);
        }

        private void OnGoSpotifyGenres(object obj)
        {
            ChangeViewModel(PageViewModels[6]);
        }

        private void OnGoSpotifySponsors(object obj)
        {
            ChangeViewModel(PageViewModels[7]);
        }

        private void OnGoSpotifySubscriptions(object obj)
        {
            ChangeViewModel(PageViewModels[8]);
        }

        private void OnGoUserForm(object obj)
        {
            ChangeAdminViewModel(PageViewModels[9]);
        }

        private void OnGoAdminForm(object obj)
        {
            ChangeAdminViewModel(PageViewModels[10]);
        }

        #region  NavigationCommands

        public DelegateCommand NavigateToServiceCommand =>
                _navigateToServiceCommand ??= new DelegateCommand(NavigateToService);

        private DelegateCommand _navigateToServiceCommand;

        private void NavigateToService()
        {
            ChangeViewModel(PageViewModels[0]);
        }

        public DelegateCommand NavigateToSpotifyControlCommand =>
                _navigateToSpotifyControlCommand ??= new DelegateCommand(NavigateToSpotifyControl);

        private DelegateCommand _navigateToSpotifyControlCommand;

        private void NavigateToSpotifyControl()
        {
            ChangeViewModel(PageViewModels[1]);
        }

        public DelegateCommand NavigateToYouTubeControlCommand =>
                _navigateToYouTubeControlCommand ??= new DelegateCommand(NavigateToYouTubeControl);

        private DelegateCommand _navigateToYouTubeControlCommand;

        private void NavigateToYouTubeControl()
        {
            ChangeViewModel(PageViewModels[2]);
        }

        public DelegateCommand NavigateToGoogleControlCommand =>
                _navigateToGoogleControlCommand ??= new DelegateCommand(NavigateToGoogleControl);

        private DelegateCommand _navigateToGoogleControlCommand;

        private void NavigateToGoogleControl()
        {
            ChangeViewModel(PageViewModels[3]);
        }

        public DelegateCommand NavigateToYandexControlCommand =>
                _navigateToYandexControlCommand ??= new DelegateCommand(NavigateToYandexControl);

        private DelegateCommand _navigateToYandexControlCommand;

        private void NavigateToYandexControl()
        {
            ChangeViewModel(PageViewModels[4]);
        }

        public DelegateCommand NavigateToAppleControlCommand =>
                _navigateToAppleControlCommand ??= new DelegateCommand(NavigateToAppleControl);

        private DelegateCommand _navigateToAppleControlCommand;

        private void NavigateToAppleControl()
        {
            ChangeViewModel(PageViewModels[5]);
        }

        public DelegateCommand NavigateToSpotifyGenresCommand =>
                _navigateToSpotifyGenresCommand ??= new DelegateCommand(NavigateToSpotifyGenres);

        private DelegateCommand _navigateToSpotifyGenresCommand;

        private void NavigateToSpotifyGenres()
        {
            ChangeViewModel(PageViewModels[6]);
        }

        public DelegateCommand NavigateToSpotifySponsorsCommand =>
                _navigateToSpotifySponsorsCommand ??= new DelegateCommand(NavigateToSpotifySponsors);

        private DelegateCommand _navigateToSpotifySponsorsCommand;

        private void NavigateToSpotifySponsors()
        {
            ChangeViewModel(PageViewModels[7]);
        }

        public DelegateCommand NavigateToSpotifySubscriptionsCommand =>
                _navigateToSpotifySubscriptionsCommand ??= new DelegateCommand(NavigateToSpotifySubscriptions);

        private DelegateCommand _navigateToSpotifySubscriptionsCommand;
        private IPageViewModel _adminPageViewModel;

        private void NavigateToSpotifySubscriptions()
        {
            ChangeViewModel(PageViewModels[8]);
        }

        public DelegateCommand NavigateToAdminCommand =>
                _navigateToAdminCommand ??= new DelegateCommand(NavigateToAdmin);

        private DelegateCommand _navigateToAdminCommand;

        private void NavigateToAdmin()
        {
            ChangeAdminViewModel(PageViewModels[10]);
        }
        #endregion
    }
}