#region Using derectives

using System.Windows;
using CourseProject.Application.EntityService;
using CourseProject.Database;
using CourseProject.Shared.IEntityService;
using CourseProject.UI;
using Prism.Ioc;
using Prism.Unity;

#endregion

namespace CourseProject
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        #region Overrides of PrismApplicationBase

        /// <inheritdoc />
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ApplicationContext>(() =>
                                                                    {
                                                                        var context = new ApplicationContext();

                                                                        return context;
                                                                    });

            containerRegistry.RegisterScoped<IArtistService, ArtistService>();
            containerRegistry.RegisterScoped<IGenreService, GenreService>();
            containerRegistry.RegisterScoped<IServiceService, ServiceService>();
            containerRegistry.RegisterScoped<ISponsorService, SponsorService>();
            containerRegistry.RegisterScoped<ISubscriptionService, SubscriptionService>();
            containerRegistry.RegisterScoped<ITrackService, TrackService>();
            containerRegistry.RegisterForNavigation<MainWindow, MainWindowViewModel>("AdministratorPage");

        }

        /// <inheritdoc />
        /// 
        protected override Window CreateShell() => Container.Resolve<MainWindow>();

        #endregion
    }
}