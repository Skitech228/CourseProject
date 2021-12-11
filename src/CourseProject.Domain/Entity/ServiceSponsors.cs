#region Using derectives

using Microsoft.EntityFrameworkCore;

#endregion

namespace CourseProject.Domain.Entity
{
    [Keyless]
    public class ServiceSponsors
    {
        public int ServiceId { get; set; }
        public int SponsorId { get; set; }
        public Service Service { get; set; }
        public Sponsor Sponsor { get; set; }
    }
}