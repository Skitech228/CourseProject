namespace CourseProject.Domain.Entity
{
    public class Service
    {
        public int ServiceId { get; set; }
        public int UsersPerDay { get; set; }
        public int Rating { get; set; }
        public string ServiceName { get; set; }

        public string Logotype { get; set; }
    }
}