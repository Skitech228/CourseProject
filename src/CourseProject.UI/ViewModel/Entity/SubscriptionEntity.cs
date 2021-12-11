#region Using derectives

using CourseProject.Domain.Entity;
using GalaSoft.MvvmLight;

#endregion

namespace CourseProject.UI.ViewModel.Entity
{
    public class SubscriptionEntity : ViewModelBase
    {
        public SubscriptionEntity(Subscription subscription) => Entity = subscription;

        #region _subscriptionEntity Property

        /// <summary>
        ///     Private member backing variable for <see cref="MyProperty" />
        /// </summary>
        private Subscription _subscriptionEntity;

        /// <summary>
        ///     Gets and sets The property's value
        /// </summary>
        public Subscription Entity
        {
            get => _subscriptionEntity;
            set { Set(() => Entity, ref _subscriptionEntity, value); }
        }

        #endregion
    }
}