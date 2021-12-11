#region Using derectives

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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

        public TrackViewModel(ITrackService salesService)
        {
            _trackService = salesService;
            Tracks = new ObservableCollection<TrackEntity>();

            ReloadTracksAsync()
                    .Wait();
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

        public TrackEntity SelectedTrack
        {
            get => _selectedTrack;
            set => Set(ref _selectedTrack, value);
        }

        private bool CanManipulateOnTrack() => SelectedTrack is not null;

        private void OnChangeEditModeCommandExecuted() => IsEditMode = !IsEditMode;

        private void OnAddTrackCommandExecuted()
        {
            Tracks.Insert(0,
                          new TrackEntity(new Track
                                          {
                                                  TrackName = String.Empty,
                                                  Cost = 0,
                                                  DurationInSeconds = 0,
                                                  GenreId = 0,
                                                  ArtistId = 0,
                                                  Path = String.Empty
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
                await _trackService.AddAsync(SelectedTrack.Entity);
            else
                await _trackService.UpdateAsync(SelectedTrack.Entity);

            await ReloadTracksAsync();
        }

        private async Task ReloadTracksAsync()
        {
            var dbSales = await _trackService.GetAllAsync();
            Tracks.Clear();

            foreach (var sale in dbSales)
                Tracks.Add(new TrackEntity(sale));
        }
    }
}