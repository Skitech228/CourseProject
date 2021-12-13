#region Using derectives

using System;
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
    public class SponsorViewModel : ViewModelBase
    {
        private readonly ISponsorService _sponsorService;
        private bool _isEditMode;
        private ObservableCollection<SponsorEntity> _sponsors;
        private SponsorEntity _selectedSponsor;
        private DelegateCommand _addSponsorCommand;
        private AsyncRelayCommand _removeSponsorRelayCommand;
        private AsyncRelayCommand _applySponsorChangesRelayCommand;
        private DelegateCommand _changeEditModeCommand;
        private AsyncRelayCommand _reloadSponsorsRelayCommand;
        private DelegateCommand _sponsorNameFiltCommand;

        public SponsorViewModel(ISponsorService salesService)
        {
            _sponsorService = salesService;
            Sponsors = new ObservableCollection<SponsorEntity>();
            Dispatcher.CurrentDispatcher.InvokeAsync(async () => await ReloadSponsorsAsync());
            //ReloadSponsorsAsync()
            //        .Wait();
        }

        public DelegateCommand AddSponsorCommand =>
                _addSponsorCommand ??= new DelegateCommand(OnAddSponsorCommandExecuted);

        public AsyncRelayCommand RemoveSponsorRelayCommand =>
                _removeSponsorRelayCommand ??= new AsyncRelayCommand(OnRemoveSponsorCommandExecuted);

        public AsyncRelayCommand ApplySponsorChangesRelayCommand =>
                _applySponsorChangesRelayCommand ??= new AsyncRelayCommand(OnApplySponsorChangesCommandExecuted);

        public DelegateCommand ChangeEditModeCommand =>
                _changeEditModeCommand ??= new DelegateCommand(OnChangeEditModeCommandExecuted,
                                                               CanManipulateOnSponsor)
                        .ObservesProperty(() => SelectedSponsor);

        public AsyncRelayCommand ReloadSponsorsRelayCommand =>
                _reloadSponsorsRelayCommand ??= new AsyncRelayCommand(ReloadSponsorsAsync);

        public DelegateCommand SponsorNameFilt => _sponsorNameFiltCommand ??= new DelegateCommand(OnSponsorNameFilt);

        private async void OnSponsorNameFilt()
        {
            var dbSales = await _sponsorService.NameFilt();
            Sponsors.Clear();

            foreach (var sale in dbSales)
                Sponsors.Add(new SponsorEntity(sale));

        }

        public ObservableCollection<SponsorEntity> Sponsors
        {
            get => _sponsors;
            set => Set(ref _sponsors, value);
        }

        public bool IsEditMode
        {
            get => _isEditMode;
            set => Set(ref _isEditMode, value);
        }

        public SponsorEntity SelectedSponsor
        {
            get => _selectedSponsor;
            set => Set(ref _selectedSponsor, value);
        }

        private bool CanManipulateOnSponsor() => SelectedSponsor is not null;

        private void OnChangeEditModeCommandExecuted() => IsEditMode = !IsEditMode;

        private void OnAddSponsorCommandExecuted()
        {
            Sponsors.Insert(0,
                            new SponsorEntity(new Sponsor
                                              {
                                                      SponsorName = String.Empty,
                                                      AdDurationInSeconds = 0
                                              }));

            SelectedSponsor = Sponsors.First();
        }

        private async Task OnRemoveSponsorCommandExecuted()
        {
            if (SelectedSponsor.Entity.SponsorId == 0)
                Sponsors.Remove(SelectedSponsor);

            await _sponsorService.RemoveAsync(SelectedSponsor.Entity);
            Sponsors.Remove(SelectedSponsor);
            SelectedSponsor = null;
        }

        private async Task OnApplySponsorChangesCommandExecuted()
        {
            if (SelectedSponsor.Entity.SponsorId == 0)
                await _sponsorService.AddAsync(SelectedSponsor.Entity);
            else
                await _sponsorService.UpdateAsync(SelectedSponsor.Entity);

            await ReloadSponsorsAsync();
        }

        private async Task ReloadSponsorsAsync()
        {
            var dbSales = await _sponsorService.GetAllAsync();
            Sponsors.Clear();

            foreach (var sale in dbSales)
                Sponsors.Add(new SponsorEntity(sale));
        }
    }
}