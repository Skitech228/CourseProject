using CourseProject.Shared.IEntityService;
using CourseProject.UI.ViewModel;
using GalaSoft.MvvmLight;

namespace CourseProject.UI.Pages
{
    public class SpotifySubscriptionsViewModel : ViewModelBase, IPageViewModel
    {
        private SubscriptionViewModel _subscriptions;

        public SpotifySubscriptionsViewModel(ISubscriptionService subscription)
        {
            SubscriptionsContext = new SubscriptionViewModel(subscription);
        }

        public SubscriptionViewModel SubscriptionsContext
        {
            get => _subscriptions;
            set { Set(() => SubscriptionsContext, ref _subscriptions, value); }
        }

    }
}