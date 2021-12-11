#region Using derectives

using CourseProject.Domain.Entity;
using GalaSoft.MvvmLight;

#endregion

namespace CourseProject.UI.ViewModel.Entity
{
    public class GenreEntity : ViewModelBase
    {
        public GenreEntity(Genre genre) => Entity = genre;

        #region _genreEntity Property

        /// <summary>
        ///     Private member backing variable for <see cref="MyProperty" />
        /// </summary>
        private Genre _genreEntity;

        /// <summary>
        ///     Gets and sets The property's value
        /// </summary>
        public Genre Entity
        {
            get => _genreEntity;
            set { Set(() => Entity, ref _genreEntity, value); }
        }

        #endregion
    }
}