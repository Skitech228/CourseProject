#region Using derectives

using CourseProject.Domain.Entity;
using Microsoft.EntityFrameworkCore;

#endregion

namespace CourseProject.Database
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
                : base(options) { }

        public ApplicationContext()
        {
        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceSponsors> ServiceSponsors { get; set; }
        public DbSet<ServiceTracks> ServiceTracks { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Track> Tracks { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.ApplyConfigurationsFromAssembly(typeof(ArtistConfiguration).Assembly);
        //    base.OnModelCreating(modelBuilder);
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
                options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=CourseProjectDatabase;Trusted_Connection=True;");
    }
}