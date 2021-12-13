using System.Windows.Input;
using CourseProject.Shared.IEntityService;
using CourseProject.UI.Pages;
using CourseProject.UI.ViewModel;
using GalaSoft.MvvmLight;
using Prism.Commands;

namespace CourseProject.UI
{
    public class AdminPageViewModel : ViewModelBase, IPageViewModel
    {
        private ArtistViewModel _artists;
        private GenreViewModel _genres;
        private ServiceViewModel _services;
        private SponsorViewModel _sponsors;
        private SubscriptionViewModel _subscriptions;
        private TrackViewModel _tracks;

        public AdminPageViewModel(IArtistService artist,
                                  IGenreService genre,
                                  IServiceService service,
                                  ISponsorService sponsor,
                                  ISubscriptionService subscription,
                                  ITrackService track)
        {
            ArtistsContext = new ArtistViewModel(artist);
            GenresContext = new GenreViewModel(genre);
            ServicesContext = new ServiceViewModel(service);
            SponsorsContext = new SponsorViewModel(sponsor);
            SubscriptionsContext = new SubscriptionViewModel(subscription);
            TracksContext = new TrackViewModel(track);
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

        private ICommand _goToUserForm;

        public ICommand GoToUserForm
        {
            get { return _goToUserForm ??= new DelegateCommand(UserFormNotify); }
        }

        private void UserFormNotify()
        {
            Mediator.Notify("GoToUserForm", "");
        }
    }
}