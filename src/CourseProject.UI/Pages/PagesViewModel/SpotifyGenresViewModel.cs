using CourseProject.Shared.IEntityService;
using CourseProject.UI.ViewModel;
using GalaSoft.MvvmLight;

namespace CourseProject.UI.Pages
{
    public class SpotifyGenresViewModel : ViewModelBase, IPageViewModel
    {
        private TrackViewModel _tracks;
        private GenreViewModel _genres;
        public SpotifyGenresViewModel(IGenreService genre,ITrackService track)
        {
            GenresContext = new GenreViewModel(genre);
            TracksContext = new TrackViewModel(track);
        }

        public GenreViewModel GenresContext
        {
            get => _genres;
            set { Set(() => GenresContext, ref _genres, value); }
        }

        public TrackViewModel TracksContext
        {
            get => _tracks;
            set { Set(() => TracksContext, ref _tracks, value); }
        }
    }
}