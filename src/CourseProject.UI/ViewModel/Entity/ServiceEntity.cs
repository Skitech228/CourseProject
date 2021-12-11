#region Using derectives

using CourseProject.Domain.Entity;
using GalaSoft.MvvmLight;

#endregion

namespace CourseProject.UI.ViewModel.Entity
{
    public class ServiceEntity : ViewModelBase
    {
        public ServiceEntity(Service service) => Entity = service;

        #region _serviceEntity Property

        /// <summary>
        ///     Private member backing variable for <see cref="MyProperty" />
        /// </summary>
        private Service _serviceEntity;

        /// <summary>
        ///     Gets and sets The property's value
        /// </summary>
        public Service Entity
        {
            get => _serviceEntity;
            set { Set(() => Entity, ref _serviceEntity, value); }
        }

        #endregion
    }
}