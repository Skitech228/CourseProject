namespace CourseProject.Domain.Entity
{
    public class Track
    {
        public int TrackId { get; set; }
        public string TrackName { get; set; }
        public int Cost { get; set; }
        public int DurationInSeconds { get; set; }
        public int GenreId { get; set; }
        public int ArtistId { get; set; }
        public string Path { get; set; }
        public Genre Genre { get; set; }
        public Artist Artist { get; set; }
    }
}