using CourseProject.Shared.IEntityService;
using CourseProject.UI.ViewModel;
using GalaSoft.MvvmLight;

namespace CourseProject.UI.Pages
{
    public class SpotifySponsorsViewModel : ViewModelBase, IPageViewModel
    {
        private SponsorViewModel _sponsors;

        public SpotifySponsorsViewModel(ISponsorService sponsor)
        {
            SponsorsContext = new SponsorViewModel(sponsor);
        }

        public SponsorViewModel SponsorsContext
        {
            get => _sponsors;
            set { Set(() => SponsorsContext, ref _sponsors, value); }
        }

    }
}