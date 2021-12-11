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
            CurrentPageViewModel = PageViewModels[0];
            Mediator.Subscribe("GoToServiceControl", OnGoServiceControl);
            Mediator.Subscribe("GoToSpotifyControl", OnGoSpotifyControl);
            Mediator.Subscribe("GoToYouTubeMusicControl", OnGoYouTubeMusicControl);
            Mediator.Subscribe("GoToGoogleMusicControl", OnGoGoogleMusicControl);
            Mediator.Subscribe("GoToYandexMusicControl", OnGoYandexMusicControl);
            Mediator.Subscribe("GoToAppleMusicControl", OnGoAppleMusicControl);





            //ArtistsContext = new ArtistViewModel(artist);
            //GenresContext = new GenreViewModel(genre);
            //ServicesContext = new ServiceViewModel(service);
            //SponsorsContext = new SponsorViewModel(sponsor);
            //SubscriptionsContext = new SubscriptionViewModel(subscription);
            //TracksContext = new TrackViewModel(track);
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

        public DelegateCommand NavigateToServiceCommand =>
                _navigateToServiceCommand ??= new DelegateCommand(NavigateToService);

        private DelegateCommand _navigateToServiceCommand;

        private void NavigateToService()
        {
            ChangeViewModel(PageViewModels[0]);
        }
    }
}