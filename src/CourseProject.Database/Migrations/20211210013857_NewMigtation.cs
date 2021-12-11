#region Using derectives

using Microsoft.EntityFrameworkCore.Migrations;

#endregion

namespace CourseProject.Database.Migrations
{
    public partial class NewMigtation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                                         "Artists",
                                         table => new
                                                  {
                                                          ArtistId = table.Column<int>("int", nullable:false)
                                                                  .Annotation("SqlServer:Identity", "1, 1"),
                                                          Nickname = table.Column<string>("nvarchar(max)",
                                                                                          nullable:true),
                                                          Name = table.Column<string>("nvarchar(max)", nullable:true),
                                                          Instagram = table.Column<string>("nvarchar(max)",
                                                                                           nullable:true),
                                                          Vk = table.Column<string>("nvarchar(max)", nullable:true),
                                                          Facebook = table.Column<string>("nvarchar(max)",
                                                                                          nullable:true),
                                                          Description =
                                                                  table.Column<string>("nvarchar(max)", nullable:true)
                                                  },
                                         constraints:table => { table.PrimaryKey("PK_Artists", x => x.ArtistId); });

            migrationBuilder.CreateTable(
                                         "Genres",
                                         table => new
                                                  {
                                                          GenreId = table.Column<int>("int", nullable:false)
                                                                  .Annotation("SqlServer:Identity", "1, 1"),
                                                          GenreName = table.Column<string>("nvarchar(max)",
                                                                                           nullable:true),
                                                          Description =
                                                                  table.Column<string>("nvarchar(max)", nullable:true)
                                                  },
                                         constraints:table => { table.PrimaryKey("PK_Genres", x => x.GenreId); });

            migrationBuilder.CreateTable(
                                         "Services",
                                         table => new
                                                  {
                                                          ServiceId = table.Column<int>("int", nullable:false)
                                                                  .Annotation("SqlServer:Identity", "1, 1"),
                                                          UsersPerDay = table.Column<int>("int", nullable:false),
                                                          Rating = table.Column<int>("int", nullable:false),
                                                          ServiceName =
                                                                  table.Column<string>("nvarchar(max)", nullable:true),
                                                          Logotype = table.Column<string>("nvarchar(max)",
                                                                                          nullable:true)
                                                  },
                                         constraints:table => { table.PrimaryKey("PK_Services", x => x.ServiceId); });

            migrationBuilder.CreateTable(
                                         "Sponsors",
                                         table => new
                                                  {
                                                          SponsorId = table.Column<int>("int", nullable:false)
                                                                  .Annotation("SqlServer:Identity", "1, 1"),
                                                          SponsorName =
                                                                  table.Column<string>("nvarchar(max)", nullable:true),
                                                          AdDurationInSeconds = table.Column<int>("int", nullable:false)
                                                  },
                                         constraints:table => { table.PrimaryKey("PK_Sponsors", x => x.SponsorId); });

            migrationBuilder.CreateTable(
                                         "Tracks",
                                         table => new
                                                  {
                                                          TrackId = table.Column<int>("int", nullable:false)
                                                                  .Annotation("SqlServer:Identity", "1, 1"),
                                                          TrackName = table.Column<string>("nvarchar(max)",
                                                                                           nullable:true),
                                                          Cost = table.Column<int>("int", nullable:false),
                                                          DurationInSeconds = table.Column<int>("int", nullable:false),
                                                          GenreId = table.Column<int>("int", nullable:false),
                                                          ArtistId = table.Column<int>("int", nullable:false),
                                                          Path = table.Column<string>("nvarchar(max)", nullable:true)
                                                  },
                                         constraints:table =>
                                                     {
                                                         table.PrimaryKey("PK_Tracks", x => x.TrackId);

                                                         table.ForeignKey(
                                                                          "FK_Tracks_Artists_ArtistId",
                                                                          x => x.ArtistId,
                                                                          "Artists",
                                                                          "ArtistId",
                                                                          onDelete:ReferentialAction.Cascade);

                                                         table.ForeignKey(
                                                                          "FK_Tracks_Genres_GenreId",
                                                                          x => x.GenreId,
                                                                          "Genres",
                                                                          "GenreId",
                                                                          onDelete:ReferentialAction.Cascade);
                                                     });

            migrationBuilder.CreateTable(
                                         "Subscriptions",
                                         table => new
                                                  {
                                                          SubscriptionId = table.Column<int>("int", nullable:false)
                                                                  .Annotation("SqlServer:Identity", "1, 1"),
                                                          Cost = table.Column<int>("int", nullable:false),
                                                          Period = table.Column<int>("int", nullable:false),
                                                          ServiceId = table.Column<int>("int", nullable:false),
                                                          Description =
                                                                  table.Column<string>("nvarchar(max)", nullable:true),
                                                          Link = table.Column<string>("nvarchar(max)", nullable:true)
                                                  },
                                         constraints:table =>
                                                     {
                                                         table.PrimaryKey("PK_Subscriptions", x => x.SubscriptionId);

                                                         table.ForeignKey(
                                                                          "FK_Subscriptions_Services_ServiceId",
                                                                          x => x.ServiceId,
                                                                          "Services",
                                                                          "ServiceId",
                                                                          onDelete:ReferentialAction.Cascade);
                                                     });

            migrationBuilder.CreateTable(
                                         "ServiceSponsors",
                                         table => new
                                                  {
                                                          ServiceId = table.Column<int>("int", nullable:false),
                                                          SponsorId = table.Column<int>("int", nullable:false)
                                                  },
                                         constraints:table =>
                                                     {
                                                         table.ForeignKey(
                                                                          "FK_ServiceSponsors_Services_ServiceId",
                                                                          x => x.ServiceId,
                                                                          "Services",
                                                                          "ServiceId",
                                                                          onDelete:ReferentialAction.Cascade);

                                                         table.ForeignKey(
                                                                          "FK_ServiceSponsors_Sponsors_SponsorId",
                                                                          x => x.SponsorId,
                                                                          "Sponsors",
                                                                          "SponsorId",
                                                                          onDelete:ReferentialAction.Cascade);
                                                     });

            migrationBuilder.CreateTable(
                                         "ServiceTracks",
                                         table => new
                                                  {
                                                          ServiceId = table.Column<int>("int", nullable:false),
                                                          TrackId = table.Column<int>("int", nullable:false)
                                                  },
                                         constraints:table =>
                                                     {
                                                         table.ForeignKey(
                                                                          "FK_ServiceTracks_Services_ServiceId",
                                                                          x => x.ServiceId,
                                                                          "Services",
                                                                          "ServiceId",
                                                                          onDelete:ReferentialAction.Cascade);

                                                         table.ForeignKey(
                                                                          "FK_ServiceTracks_Tracks_TrackId",
                                                                          x => x.TrackId,
                                                                          "Tracks",
                                                                          "TrackId",
                                                                          onDelete:ReferentialAction.Cascade);
                                                     });

            migrationBuilder.CreateIndex(
                                         "IX_ServiceSponsors_ServiceId",
                                         "ServiceSponsors",
                                         "ServiceId");

            migrationBuilder.CreateIndex(
                                         "IX_ServiceSponsors_SponsorId",
                                         "ServiceSponsors",
                                         "SponsorId");

            migrationBuilder.CreateIndex(
                                         "IX_ServiceTracks_ServiceId",
                                         "ServiceTracks",
                                         "ServiceId");

            migrationBuilder.CreateIndex(
                                         "IX_ServiceTracks_TrackId",
                                         "ServiceTracks",
                                         "TrackId");

            migrationBuilder.CreateIndex(
                                         "IX_Subscriptions_ServiceId",
                                         "Subscriptions",
                                         "ServiceId");

            migrationBuilder.CreateIndex(
                                         "IX_Tracks_ArtistId",
                                         "Tracks",
                                         "ArtistId");

            migrationBuilder.CreateIndex(
                                         "IX_Tracks_GenreId",
                                         "Tracks",
                                         "GenreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                                       "ServiceSponsors");

            migrationBuilder.DropTable(
                                       "ServiceTracks");

            migrationBuilder.DropTable(
                                       "Subscriptions");

            migrationBuilder.DropTable(
                                       "Sponsors");

            migrationBuilder.DropTable(
                                       "Tracks");

            migrationBuilder.DropTable(
                                       "Services");

            migrationBuilder.DropTable(
                                       "Artists");

            migrationBuilder.DropTable(
                                       "Genres");
        }
    }
}