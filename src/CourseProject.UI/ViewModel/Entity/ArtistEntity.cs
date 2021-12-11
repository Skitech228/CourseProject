#region Using derectives

using CourseProject.Domain.Entity;
using GalaSoft.MvvmLight;

#endregion

namespace CourseProject.UI.ViewModel.Entity
{
    public class ArtistEntity : ViewModelBase
    {
        public ArtistEntity(Artist artist) => Entity = artist;

        #region _artistEntity Property

        /// <summary>
        ///     Private member backing variable for <see cref="MyProperty" />
        /// </summary>
        private Artist _artistEntity;

        /// <summary>
        ///     Gets and sets The property's value
        /// </summary>
        public Artist Entity
        {
            get => _artistEntity;
            set { Set(() => Entity, ref _artistEntity, value); }
        }

        #endregion
    }
}