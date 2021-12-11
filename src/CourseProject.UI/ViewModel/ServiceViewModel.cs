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
    public class ServiceViewModel : ViewModelBase
    {
        private readonly IServiceService _serviceService;
        private bool _isEditMode;
        private ObservableCollection<ServiceEntity> _services;
        private ServiceEntity _selectedService;
        private DelegateCommand _addServiceCommand;
        private AsyncRelayCommand _removeServiceRelayCommand;
        private AsyncRelayCommand _applyServiceChangesRelayCommand;
        private DelegateCommand _changeEditModeCommand;
        private AsyncRelayCommand _reloadServicesRelayCommand;

        public ServiceViewModel(IServiceService salesServiceService)
        {
            _serviceService = salesServiceService;
            Services = new ObservableCollection<ServiceEntity>();

            ReloadServicesAsync()
                    .Wait();
        }

        public DelegateCommand AddServiceCommand =>
                _addServiceCommand ??= new DelegateCommand(OnAddServiceCommandExecuted);

        public AsyncRelayCommand RemoveServiceRelayCommand =>
                _removeServiceRelayCommand ??= new AsyncRelayCommand(OnRemoveServiceCommandExecuted);

        public AsyncRelayCommand ApplyServiceChangesRelayCommand =>
                _applyServiceChangesRelayCommand ??= new AsyncRelayCommand(OnApplyServiceChangesCommandExecuted);

        public DelegateCommand ChangeEditModeCommand =>
                _changeEditModeCommand ??= new DelegateCommand(OnChangeEditModeCommandExecuted,
                                                               CanManipulateOnSale)
                        .ObservesProperty(() => SelectedService);

        public AsyncRelayCommand ReloadServicesRelayCommand =>
                _reloadServicesRelayCommand ??= new AsyncRelayCommand(ReloadServicesAsync);

        public ObservableCollection<ServiceEntity> Services
        {
            get => _services;
            set => Set(ref _services, value);
        }

        public bool IsEditMode
        {
            get => _isEditMode;
            set => Set(ref _isEditMode, value);
        }

        public ServiceEntity SelectedService
        {
            get => _selectedService;
            set => Set(ref _selectedService, value);
        }

        private bool CanManipulateOnSale() => SelectedService is not null;

        private void OnChangeEditModeCommandExecuted() => IsEditMode = !IsEditMode;

        private void OnAddServiceCommandExecuted()
        {
            Services.Insert(0,
                            new ServiceEntity(new Service
                                              {
                                                      UsersPerDay = 0,
                                                      Rating = 0,
                                                      ServiceName = String.Empty,
                                                      Logotype = String.Empty
                                              }));

            SelectedService = Services.First();
        }

        private async Task OnRemoveServiceCommandExecuted()
        {
            if (SelectedService.Entity.ServiceId == 0)
                Services.Remove(SelectedService);

            await _serviceService.RemoveAsync(SelectedService.Entity);
            Services.Remove(SelectedService);
            SelectedService = null;
        }

        private async Task OnApplyServiceChangesCommandExecuted()
        {
            if (SelectedService.Entity.ServiceId == 0)
                await _serviceService.AddAsync(SelectedService.Entity);
            else
                await _serviceService.UpdateAsync(SelectedService.Entity);

            await ReloadServicesAsync();
        }

        private async Task ReloadServicesAsync()
        {
            var dbSales = await _serviceService.GetAllAsync();
            Services.Clear();

            foreach (var sale in dbSales)
                Services.Add(new ServiceEntity(sale));
        }
    }
}