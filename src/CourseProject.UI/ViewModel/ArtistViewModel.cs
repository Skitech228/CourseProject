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
    public class ArtistViewModel : ViewModelBase
    {
        private readonly IArtistService _artistService;
        private bool _isEditMode;
        private ObservableCollection<ArtistEntity> _artists;
        private ArtistEntity _selectedArtist;
        private DelegateCommand _addArtistCommand;
        private AsyncRelayCommand _removeArtistRelayCommand;
        private AsyncRelayCommand _applyArtistChangesRelayCommand;
        private DelegateCommand _changeEditModeCommand;
        private AsyncRelayCommand _reloadArtistsRelayCommand;

        public ArtistViewModel(IArtistService artistService)
        {
            _artistService = artistService;
            Artists = new ObservableCollection<ArtistEntity>();

            ReloadArtistsAsync()
                    .Wait();
        }

        public DelegateCommand AddArtistCommand =>
                _addArtistCommand ??= new DelegateCommand(OnAddArtistCommandExecuted);

        public AsyncRelayCommand RemoveArtistRelayCommand =>
                _removeArtistRelayCommand ??= new AsyncRelayCommand(OnRemoveArtistCommandExecuted);

        public AsyncRelayCommand ApplyArtistChangesRelayCommand =>
                _applyArtistChangesRelayCommand ??= new AsyncRelayCommand(OnApplyArtistChangesCommandExecuted);

        public DelegateCommand ChangeEditModeCommand =>
                _changeEditModeCommand ??= new DelegateCommand(OnChangeEditModeCommandExecuted,
                                                               CanManipulateOnSale)
                        .ObservesProperty(() => SelectedArtist);

        public AsyncRelayCommand ReloadArtistsRelayCommand =>
                _reloadArtistsRelayCommand ??= new AsyncRelayCommand(ReloadArtistsAsync);

        public ObservableCollection<ArtistEntity> Artists
        {
            get => _artists;
            set => Set(ref _artists, value);
        }

        public bool IsEditMode
        {
            get => _isEditMode;
            set => Set(ref _isEditMode, value);
        }

        public ArtistEntity SelectedArtist
        {
            get => _selectedArtist;
            set => Set(ref _selectedArtist, value);
        }

        private bool CanManipulateOnSale() => SelectedArtist is not null;

        private void OnChangeEditModeCommandExecuted() => IsEditMode = !IsEditMode;

        private void OnAddArtistCommandExecuted()
        {
            Artists.Insert(0,
                           new ArtistEntity(new Artist
                                            {
                                                    Nickname = String.Empty,
                                                    Name = String.Empty,
                                                    Instagram = String.Empty,
                                                    Vk = String.Empty,
                                                    Facebook = String.Empty,
                                                    Description = String.Empty
                                            }));

            SelectedArtist = Artists.First();
        }

        private async Task OnRemoveArtistCommandExecuted()
        {
            if (SelectedArtist.Entity.ArtistId == 0)
                Artists.Remove(SelectedArtist);

            await _artistService.RemoveAsync(SelectedArtist.Entity);
            Artists.Remove(SelectedArtist);
            SelectedArtist = null;
        }

        private async Task OnApplyArtistChangesCommandExecuted()
        {
            if (SelectedArtist.Entity.ArtistId == 0)
                await _artistService.AddAsync(SelectedArtist.Entity);
            else
                await _artistService.UpdateAsync(SelectedArtist.Entity);

            await ReloadArtistsAsync();
        }

        private async Task ReloadArtistsAsync()
        {
            var dbSales = await _artistService.GetAllAsync();
            Artists.Clear();

            foreach (var sale in dbSales)
                Artists.Add(new ArtistEntity(sale));
        }
    }
}