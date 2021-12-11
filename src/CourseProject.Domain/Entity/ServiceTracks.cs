#region Using derectives

using Microsoft.EntityFrameworkCore;

#endregion

namespace CourseProject.Domain.Entity
{
    [Keyless]
    public class ServiceTracks
    {
        public int ServiceId { get; set; }
        public int TrackId { get; set; }
        public Service Service { get; set; }
        public Track Track { get; set; }
    }
}