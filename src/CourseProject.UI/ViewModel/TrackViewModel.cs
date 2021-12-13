#region Using derectives

using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;
using CourseProject.Application.AsyncConmands;
using CourseProject.Domain.Entity;
using CourseProject.Shared.IEntityService;
using CourseProject.UI.ViewModel.Entity;
using GalaSoft.MvvmLight;
using Prism.Commands;

#endregion

namespace CourseProject.UI.ViewModel
{
    public class TrackViewModel : ViewModelBase
    {
        private readonly ITrackService _trackService;
        private bool _isEditMode;
        private ObservableCollection<TrackEntity> _tracks;
        private TrackEntity _selectedTrack;
        private DelegateCommand _addTrackCommand;
        private AsyncRelayCommand _removeTrackRelayCommand;
        private AsyncRelayCommand _applyTrackChangesRelayCommand;
        private DelegateCommand _changeEditModeCommand;
        private AsyncRelayCommand _reloadTracksRelayCommand;
        private DelegateCommand _playOrOffMusic;
        private bool _isMusicPlay;
        private string _behavior;
        private string _selectedGenre;
        private string _selectedArtist;
        private DelegateCommand _genreNameFiltCommand;

        public TrackViewModel(ITrackService salesService)
        {
            _trackService = salesService;
            Tracks = new ObservableCollection<TrackEntity>();
            Dispatcher.CurrentDispatcher.InvokeAsync(async () => await ReloadTracksAsync());
            Behavior = "Stop";

            //ReloadTracksAsync()
            //        .Wait();
        }

        public DelegateCommand AddTrackCommand => _addTrackCommand ??= new DelegateCommand(OnAddTrackCommandExecuted);

        public AsyncRelayCommand RemoveTrackRelayCommand =>
                _removeTrackRelayCommand ??= new AsyncRelayCommand(OnRemoveTrackCommandExecuted);

        public AsyncRelayCommand ApplyTrackChangesRelayCommand =>
                _applyTrackChangesRelayCommand ??= new AsyncRelayCommand(OnApplyTrackChangesCommandExecuted);

        public DelegateCommand ChangeEditModeCommand =>
                _changeEditModeCommand ??= new DelegateCommand(OnChangeEditModeCommandExecuted,
                                                               CanManipulateOnTrack)
                        .ObservesProperty(() => SelectedTrack);

        public AsyncRelayCommand ReloadTracksRelayCommand =>
                _reloadTracksRelayCommand ??= new AsyncRelayCommand(ReloadTracksAsync);

        public DelegateCommand GenreNameFilt => _genreNameFiltCommand ??= new DelegateCommand(OnGenreNameFilt);

        public ObservableCollection<TrackEntity> Tracks
        {
            get => _tracks;
            set => Set(ref _tracks, value);
        }

        public bool IsEditMode
        {
            get => _isEditMode;
            set => Set(ref _isEditMode, value);
        }

        public string SelectedGenre
        {
            get => _selectedGenre;
            set => Set(ref _selectedGenre, value);
        }

        public string SelectedArtist
        {
            get => _selectedArtist;
            set => Set(ref _selectedArtist, value);
        }

        public TrackEntity SelectedTrack
        {
            get => _selectedTrack;
            set => Set(ref _selectedTrack, value);
        }

        private async void OnGenreNameFilt()
        {
            var dbSales = await _trackService.NameFilt(SelectedTrack.Entity.Genre.GenreName);
            Tracks.Clear();

            foreach (var sale in dbSales)
                Tracks.Add(new TrackEntity(sale));
        }

        private bool CanManipulateOnTrack() => SelectedTrack is not null;

        private void OnChangeEditModeCommandExecuted() => IsEditMode = !IsEditMode;

        private void OnAddTrackCommandExecuted()
        {
            Tracks.Insert(0,
                          new TrackEntity(new Track
                                          {
                                                  TrackName = "String.Empty",
                                                  Cost = 0,
                                                  DurationInSeconds = 0,
                                                  GenreId = 0,
                                                  ArtistId = 0,
                                                  Path = @"C:/Enemy.mp3"
                                          }));

            SelectedTrack = Tracks.First();
        }

        private async Task OnRemoveTrackCommandExecuted()
        {
            if (SelectedTrack.Entity.TrackId == 0)
                Tracks.Remove(SelectedTrack);

            await _trackService.RemoveAsync(SelectedTrack.Entity);
            Tracks.Remove(SelectedTrack);
            SelectedTrack = null;
        }

        private async Task OnApplyTrackChangesCommandExecuted()
        {
            if (SelectedTrack.Entity.TrackId == 0)
                await _trackService.AddAsync(SelectedTrack.Entity, SelectedArtist, SelectedGenre);
            else
                await _trackService.UpdateAsync(SelectedTrack.Entity, SelectedArtist, SelectedGenre);

            await ReloadTracksAsync();
        }

        private async Task ReloadTracksAsync()
        {
            var dbSales = await _trackService.GetAllAsync();
            Tracks.Clear();

            foreach (var sale in dbSales)
                Tracks.Add(new TrackEntity(sale));
        }

        #region Music

        public DelegateCommand PlayOrOffMusic => _playOrOffMusic ??= new DelegateCommand(OnPlayOrOffMusic);

        private void OnPlayOrOffMusic()
        {
            if (IsMusicPlay)
            {
                IsMusicPlay = false;
                Behavior = "Pause";
            }
            else
            {
                if (SelectedTrack != null)
                {
                    if (SelectedTrack.Entity.Path != "")
                    {
                        IsMusicPlay = true;
                        Behavior = "Play";
                    }
                }
            }
        }

        public bool IsMusicPlay
        {
            get => _isMusicPlay;
            set => Set(ref _isMusicPlay, value);
        }

        public string Behavior
        {
            get => _behavior;
            set => Set(ref _behavior, value);
        }

        #endregion
    }
}