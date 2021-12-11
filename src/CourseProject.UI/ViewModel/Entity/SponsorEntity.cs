#region Using derectives

using CourseProject.Domain.Entity;
using GalaSoft.MvvmLight;

#endregion

namespace CourseProject.UI.ViewModel.Entity
{
    public class SponsorEntity : ViewModelBase
    {
        public SponsorEntity(Sponsor sponsor) => Entity = sponsor;

        #region _sponsorEntity Property

        /// <summary>
        ///     Private member backing variable for <see cref="MyProperty" />
        /// </summary>
        private Sponsor _sponsorEntity;

        /// <summary>
        ///     Gets and sets The property's value
        /// </summary>
        public Sponsor Entity
        {
            get => _sponsorEntity;
            set { Set(() => Entity, ref _sponsorEntity, value); }
        }

        #endregion
    }
}