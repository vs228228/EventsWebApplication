namespace EventsWebApplication.Server.Domain.Entities
{
    public class User
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateOnly Birthday { get; set; }
        public bool IsAdmin { get; set; }
        public string NotificationString { get; set; } = "";
        public ICollection<EventParticipant> Events { get; set; }

        
    }
}
