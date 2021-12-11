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
    public class GenreViewModel : ViewModelBase
    {
        private readonly IGenreService _genreService;
        private bool _isEditMode;
        private ObservableCollection<GenreEntity> _genres;
        private GenreEntity _selectedGenre;
        private DelegateCommand _addGenreCommand;
        private AsyncRelayCommand _removeGenreRelayCommand;
        private AsyncRelayCommand _applyGenreChangesRelayCommand;
        private DelegateCommand _changeEditModeCommand;
        private AsyncRelayCommand _reloadGenresRelayCommand;

        public GenreViewModel(IGenreService salesService)
        {
            _genreService = salesService;
            Genres = new ObservableCollection<GenreEntity>();

            ReloadGenresAsync()
                    .Wait();
        }

        public DelegateCommand AddGenreCommand => _addGenreCommand ??= new DelegateCommand(OnAddGenreCommandExecuted);

        public AsyncRelayCommand RemoveGenreRelayCommand =>
                _removeGenreRelayCommand ??= new AsyncRelayCommand(OnRemoveGenreCommandExecuted);

        public AsyncRelayCommand ApplyGenreChangesRelayCommand =>
                _applyGenreChangesRelayCommand ??= new AsyncRelayCommand(OnApplyGenreChangesCommandExecuted);

        public DelegateCommand ChangeEditModeCommand =>
                _changeEditModeCommand ??= new DelegateCommand(OnChangeEditModeCommandExecuted,
                                                               CanManipulateOnSale)
                        .ObservesProperty(() => SelectedGenre);

        public AsyncRelayCommand ReloadGenresRelayCommand =>
                _reloadGenresRelayCommand ??= new AsyncRelayCommand(ReloadGenresAsync);

        public ObservableCollection<GenreEntity> Genres
        {
            get => _genres;
            set => Set(ref _genres, value);
        }

        public bool IsEditMode
        {
            get => _isEditMode;
            set => Set(ref _isEditMode, value);
        }

        public GenreEntity SelectedGenre
        {
            get => _selectedGenre;
            set => Set(ref _selectedGenre, value);
        }

        private bool CanManipulateOnSale() => SelectedGenre is not null;

        private void OnChangeEditModeCommandExecuted() => IsEditMode = !IsEditMode;

        private void OnAddGenreCommandExecuted()
        {
            Genres.Insert(0,
                          new GenreEntity(new Genre
                                          {
                                                  GenreName = String.Empty,
                                                  Description = String.Empty
                                          }));

            SelectedGenre = Genres.First();
        }

        private async Task OnRemoveGenreCommandExecuted()
        {
            if (SelectedGenre.Entity.GenreId == 0)
                Genres.Remove(SelectedGenre);

            await _genreService.RemoveAsync(SelectedGenre.Entity);
            Genres.Remove(SelectedGenre);
            SelectedGenre = null;
        }

        private async Task OnApplyGenreChangesCommandExecuted()
        {
            if (SelectedGenre.Entity.GenreId == 0)
                await _genreService.AddAsync(SelectedGenre.Entity);
            else
                await _genreService.UpdateAsync(SelectedGenre.Entity);

            await ReloadGenresAsync();
        }

        private async Task ReloadGenresAsync()
        {
            var dbSales = await _genreService.GetAllAsync();
            Genres.Clear();

            foreach (var sale in dbSales)
                Genres.Add(new GenreEntity(sale));
        }
    }
}