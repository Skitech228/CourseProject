﻿#region Using derectives

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

#endregion

namespace CourseProject.Database.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    internal class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            #pragma warning disable 612, 618
            modelBuilder
                    .HasAnnotation("Relational:MaxIdentifierLength", 128)
                    .HasAnnotation("ProductVersion", "5.0.11")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy",
                                   SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CourseProject.Domain.Entity.Artist", b =>
                                                                      {
                                                                          b.Property<int>("ArtistId")
                                                                                  .ValueGeneratedOnAdd()
                                                                                  .HasColumnType("int")
                                                                                  .HasAnnotation("SqlServer:ValueGenerationStrategy",
                                                                                                 SqlServerValueGenerationStrategy
                                                                                                         .IdentityColumn);

                                                                          b.Property<string>("Description")
                                                                                  .HasColumnType("nvarchar(max)");

                                                                          b.Property<string>("Facebook")
                                                                                  .HasColumnType("nvarchar(max)");

                                                                          b.Property<string>("Instagram")
                                                                                  .HasColumnType("nvarchar(max)");

                                                                          b.Property<string>("Name")
                                                                                  .HasColumnType("nvarchar(max)");

                                                                          b.Property<string>("Nickname")
                                                                                  .HasColumnType("nvarchar(max)");

                                                                          b.Property<string>("Vk")
                                                                                  .HasColumnType("nvarchar(max)");

                                                                          b.HasKey("ArtistId");
                                                                          b.ToTable("Artists");
                                                                      });

            modelBuilder.Entity("CourseProject.Domain.Entity.Genre", b =>
                                                                     {
                                                                         b.Property<int>("GenreId")
                                                                                 .ValueGeneratedOnAdd()
                                                                                 .HasColumnType("int")
                                                                                 .HasAnnotation("SqlServer:ValueGenerationStrategy",
                                                                                                SqlServerValueGenerationStrategy
                                                                                                        .IdentityColumn);

                                                                         b.Property<string>("Description")
                                                                                 .HasColumnType("nvarchar(max)");

                                                                         b.Property<string>("GenreName")
                                                                                 .HasColumnType("nvarchar(max)");

                                                                         b.HasKey("GenreId");
                                                                         b.ToTable("Genres");
                                                                     });

            modelBuilder.Entity("CourseProject.Domain.Entity.Service", b =>
                                                                       {
                                                                           b.Property<int>("ServiceId")
                                                                                   .ValueGeneratedOnAdd()
                                                                                   .HasColumnType("int")
                                                                                   .HasAnnotation("SqlServer:ValueGenerationStrategy",
                                                                                                  SqlServerValueGenerationStrategy
                                                                                                          .IdentityColumn);

                                                                           b.Property<string>("Logotype")
                                                                                   .HasColumnType("nvarchar(max)");

                                                                           b.Property<int>("Rating")
                                                                                   .HasColumnType("int");

                                                                           b.Property<string>("ServiceName")
                                                                                   .HasColumnType("nvarchar(max)");

                                                                           b.Property<int>("UsersPerDay")
                                                                                   .HasColumnType("int");

                                                                           b.HasKey("ServiceId");
                                                                           b.ToTable("Services");
                                                                       });

            modelBuilder.Entity("CourseProject.Domain.Entity.ServiceSponsors", b =>
                                                                               {
                                                                                   b.Property<int>("ServiceId")
                                                                                           .HasColumnType("int");

                                                                                   b.Property<int>("SponsorId")
                                                                                           .HasColumnType("int");

                                                                                   b.HasIndex("ServiceId");
                                                                                   b.HasIndex("SponsorId");
                                                                                   b.ToTable("ServiceSponsors");
                                                                               });

            modelBuilder.Entity("CourseProject.Domain.Entity.ServiceTracks", b =>
                                                                             {
                                                                                 b.Property<int>("ServiceId")
                                                                                         .HasColumnType("int");

                                                                                 b.Property<int>("TrackId")
                                                                                         .HasColumnType("int");

                                                                                 b.HasIndex("ServiceId");
                                                                                 b.HasIndex("TrackId");
                                                                                 b.ToTable("ServiceTracks");
                                                                             });

            modelBuilder.Entity("CourseProject.Domain.Entity.Sponsor", b =>
                                                                       {
                                                                           b.Property<int>("SponsorId")
                                                                                   .ValueGeneratedOnAdd()
                                                                                   .HasColumnType("int")
                                                                                   .HasAnnotation("SqlServer:ValueGenerationStrategy",
                                                                                                  SqlServerValueGenerationStrategy
                                                                                                          .IdentityColumn);

                                                                           b.Property<int>("AdDurationInSeconds")
                                                                                   .HasColumnType("int");

                                                                           b.Property<string>("SponsorName")
                                                                                   .HasColumnType("nvarchar(max)");

                                                                           b.HasKey("SponsorId");
                                                                           b.ToTable("Sponsors");
                                                                       });

            modelBuilder.Entity("CourseProject.Domain.Entity.Subscription", b =>
                                                                            {
                                                                                b.Property<int>("SubscriptionId")
                                                                                        .ValueGeneratedOnAdd()
                                                                                        .HasColumnType("int")
                                                                                        .HasAnnotation("SqlServer:ValueGenerationStrategy",
                                                                                                       SqlServerValueGenerationStrategy
                                                                                                               .IdentityColumn);

                                                                                b.Property<int>("Cost")
                                                                                        .HasColumnType("int");

                                                                                b.Property<string>("Description")
                                                                                        .HasColumnType("nvarchar(max)");

                                                                                b.Property<string>("Link")
                                                                                        .HasColumnType("nvarchar(max)");

                                                                                b.Property<int>("Period")
                                                                                        .HasColumnType("int");

                                                                                b.Property<int>("ServiceId")
                                                                                        .HasColumnType("int");

                                                                                b.HasKey("SubscriptionId");
                                                                                b.HasIndex("ServiceId");
                                                                                b.ToTable("Subscriptions");
                                                                            });

            modelBuilder.Entity("CourseProject.Domain.Entity.Track", b =>
                                                                     {
                                                                         b.Property<int>("TrackId")
                                                                                 .ValueGeneratedOnAdd()
                                                                                 .HasColumnType("int")
                                                                                 .HasAnnotation("SqlServer:ValueGenerationStrategy",
                                                                                                SqlServerValueGenerationStrategy
                                                                                                        .IdentityColumn);

                                                                         b.Property<int>("ArtistId")
                                                                                 .HasColumnType("int");

                                                                         b.Property<int>("Cost")
                                                                                 .HasColumnType("int");

                                                                         b.Property<int>("DurationInSeconds")
                                                                                 .HasColumnType("int");

                                                                         b.Property<int>("GenreId")
                                                                                 .HasColumnType("int");

                                                                         b.Property<string>("Path")
                                                                                 .HasColumnType("nvarchar(max)");

                                                                         b.Property<string>("TrackName")
                                                                                 .HasColumnType("nvarchar(max)");

                                                                         b.HasKey("TrackId");
                                                                         b.HasIndex("ArtistId");
                                                                         b.HasIndex("GenreId");
                                                                         b.ToTable("Tracks");
                                                                     });

            modelBuilder.Entity("CourseProject.Domain.Entity.ServiceSponsors", b =>
                                                                               {
                                                                                   b.HasOne("CourseProject.Domain.Entity.Service",
                                                                                            "Service")
                                                                                           .WithMany()
                                                                                           .HasForeignKey("ServiceId")
                                                                                           .OnDelete(DeleteBehavior
                                                                                                             .Cascade)
                                                                                           .IsRequired();

                                                                                   b.HasOne("CourseProject.Domain.Entity.Sponsor",
                                                                                            "Sponsor")
                                                                                           .WithMany()
                                                                                           .HasForeignKey("SponsorId")
                                                                                           .OnDelete(DeleteBehavior
                                                                                                             .Cascade)
                                                                                           .IsRequired();

                                                                                   b.Navigation("Service");
                                                                                   b.Navigation("Sponsor");
                                                                               });

            modelBuilder.Entity("CourseProject.Domain.Entity.ServiceTracks", b =>
                                                                             {
                                                                                 b.HasOne("CourseProject.Domain.Entity.Service",
                                                                                          "Service")
                                                                                         .WithMany()
                                                                                         .HasForeignKey("ServiceId")
                                                                                         .OnDelete(DeleteBehavior
                                                                                                           .Cascade)
                                                                                         .IsRequired();

                                                                                 b.HasOne("CourseProject.Domain.Entity.Track",
                                                                                          "Track")
                                                                                         .WithMany()
                                                                                         .HasForeignKey("TrackId")
                                                                                         .OnDelete(DeleteBehavior
                                                                                                           .Cascade)
                                                                                         .IsRequired();

                                                                                 b.Navigation("Service");
                                                                                 b.Navigation("Track");
                                                                             });

            modelBuilder.Entity("CourseProject.Domain.Entity.Subscription", b =>
                                                                            {
                                                                                b.HasOne("CourseProject.Domain.Entity.Service",
                                                                                         "Service")
                                                                                        .WithMany()
                                                                                        .HasForeignKey("ServiceId")
                                                                                        .OnDelete(DeleteBehavior
                                                                                                          .Cascade)
                                                                                        .IsRequired();

                                                                                b.Navigation("Service");
                                                                            });

            modelBuilder.Entity("CourseProject.Domain.Entity.Track", b =>
                                                                     {
                                                                         b.HasOne("CourseProject.Domain.Entity.Artist",
                                                                                  "Artist")
                                                                                 .WithMany()
                                                                                 .HasForeignKey("ArtistId")
                                                                                 .OnDelete(DeleteBehavior.Cascade)
                                                                                 .IsRequired();

                                                                         b.HasOne("CourseProject.Domain.Entity.Genre",
                                                                                  "Genre")
                                                                                 .WithMany()
                                                                                 .HasForeignKey("GenreId")
                                                                                 .OnDelete(DeleteBehavior.Cascade)
                                                                                 .IsRequired();

                                                                         b.Navigation("Artist");
                                                                         b.Navigation("Genre");
                                                                     });
            #pragma warning restore 612, 618
        }
    }
}