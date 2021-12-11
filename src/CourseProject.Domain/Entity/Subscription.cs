namespace CourseProject.Domain.Entity
{
    public class Subscription
    {
        public int SubscriptionId { get; set; }
        public int Cost { get; set; }
        public int Period { get; set; }
        public int ServiceId { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public Service Service { get; set; }
    }
}