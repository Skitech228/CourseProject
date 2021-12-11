#region Using derectives

using CourseProject.Domain.Entity;
using GalaSoft.MvvmLight;

#endregion

namespace CourseProject.UI.ViewModel.Entity
{
    public class TrackEntity : ViewModelBase
    {
        public TrackEntity(Track track) => Entity = track;

        #region _trackEntity Property

        /// <summary>
        ///     Private member backing variable for <see cref="MyProperty" />
        /// </summary>
        private Track _trackEntity;

        /// <summary>
        ///     Gets and sets The property's value
        /// </summary>
        public Track Entity
        {
            get => _trackEntity;
            set { Set(() => Entity, ref _trackEntity, value); }
        }

        #endregion
    }
}