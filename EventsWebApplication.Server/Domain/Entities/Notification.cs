namespace EventsWebApplication.Server.Domain.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
