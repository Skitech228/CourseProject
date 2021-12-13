#region Using derectives

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    public class SubscriptionViewModel : ViewModelBase
    {
        private readonly ISubscriptionService _subscriptionService;
        private bool _isEditMode;
        private ObservableCollection<SubscriptionEntity> _subscriptions;
        private SubscriptionEntity _selectedSubscription;
        private DelegateCommand _addSubscriptionCommand;
        private AsyncRelayCommand _removeSubscriptionRelayCommand;
        private AsyncRelayCommand _applySubscriptionChangesRelayCommand;
        private DelegateCommand _changeEditModeCommand;
        private AsyncRelayCommand _reloadSubscriptionsRelayCommand;
        private DelegateCommand _buySubCommand;
        private string _selectService;
        private DelegateCommand _subscriptionNameFiltCommand;

        public SubscriptionViewModel(ISubscriptionService salesService)
        {
            _subscriptionService = salesService;
            Subscriptions = new ObservableCollection<SubscriptionEntity>();
            Dispatcher.CurrentDispatcher.InvokeAsync(async () => await ReloadSubscriptionsAsync());
            //ReloadSubscriptionsAsync()
            //        .Wait();
        }

        public DelegateCommand BuySub =>
                _buySubCommand ??= new DelegateCommand(OnBuySubCommandExecuted);

        private void OnBuySubCommandExecuted()
        {
            Process.Start(new ProcessStartInfo("cmd", $"/c start https://{SelectedSubscription.Entity.Link}"));
        }

        public DelegateCommand AddSubscriptionCommand =>
                _addSubscriptionCommand ??= new DelegateCommand(OnAddSubscriptionCommandExecuted);

        public AsyncRelayCommand RemoveSubscriptionRelayCommand =>
                _removeSubscriptionRelayCommand ??= new AsyncRelayCommand(OnRemoveSubscriptionCommandExecuted);

        public AsyncRelayCommand ApplySubscriptionChangesRelayCommand =>
                _applySubscriptionChangesRelayCommand ??=
                        new AsyncRelayCommand(OnApplySubscriptionChangesCommandExecuted);

        public DelegateCommand ChangeEditModeCommand =>
                _changeEditModeCommand ??= new DelegateCommand(OnChangeEditModeCommandExecuted,
                                                               CanManipulateOnSubscription)
                        .ObservesProperty(() => SelectedSubscription);

        public AsyncRelayCommand ReloadSubscriptionsRelayCommand =>
                _reloadSubscriptionsRelayCommand ??= new AsyncRelayCommand(ReloadSubscriptionsAsync);

        public DelegateCommand SubscriptionNameFilt => _subscriptionNameFiltCommand ??= new DelegateCommand(OnSubscriptionNameFilt);

        private async void OnSubscriptionNameFilt()
        {
            var dbSales = await _subscriptionService.CostFilt();
            Subscriptions.Clear();

            foreach (var sale in dbSales)
                Subscriptions.Add(new SubscriptionEntity(sale));

        }

        public ObservableCollection<SubscriptionEntity> Subscriptions
        {
            get => _subscriptions;
            set => Set(ref _subscriptions, value);
        }

        public bool IsEditMode
        {
            get => _isEditMode;
            set => Set(ref _isEditMode, value);
        }
        public string SelectService
        {
            get => _selectService;
            set => Set(ref _selectService, value);
        } 

        public SubscriptionEntity SelectedSubscription
        {
            get => _selectedSubscription;
            set => Set(ref _selectedSubscription, value);
        }

        private bool CanManipulateOnSubscription() => SelectedSubscription is not null;

        private void OnChangeEditModeCommandExecuted() => IsEditMode = !IsEditMode;

        private void OnAddSubscriptionCommandExecuted()
        {
            Subscriptions.Insert(0,
                                 new SubscriptionEntity(new Subscription
                                                        {
                                                                Cost = 0,
                                                                Period = 0,
                                                                ServiceId = 0,
                                                                Link = String.Empty,
                                                                Description = String.Empty
                                                        }));

            SelectedSubscription = Subscriptions.First();
        }

        private async Task OnRemoveSubscriptionCommandExecuted()
        {
            if (SelectedSubscription.Entity.ServiceId == 0)
                Subscriptions.Remove(SelectedSubscription);

            await _subscriptionService.RemoveAsync(SelectedSubscription.Entity);
            Subscriptions.Remove(SelectedSubscription);
            SelectedSubscription = null;
        }

        private async Task OnApplySubscriptionChangesCommandExecuted()
        {
            if (SelectedSubscription.Entity.ServiceId == 0)
                await _subscriptionService.AddAsync(SelectedSubscription.Entity, SelectService);
            else
                await _subscriptionService.UpdateAsync(SelectedSubscription.Entity, SelectService);

            await ReloadSubscriptionsAsync();
        }

        private async Task ReloadSubscriptionsAsync()
        {
            var dbSales = await _subscriptionService.GetAllAsync();
            Subscriptions.Clear();

            foreach (var sale in dbSales)
                Subscriptions.Add(new SubscriptionEntity(sale));
        }
    }
}