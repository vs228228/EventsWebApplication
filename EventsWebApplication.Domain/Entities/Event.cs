namespace EventsWebApplication.Domain.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateAndTime { get; set; }
        public string Place { get; set; } // не присутстует в бд
        public string Type { get; set; }
        public int MaxParticipants { get; set; }
        public int CountOfParticipants { get; set; }
        public ICollection<EventParticipant> Participants { get; set; }
        public string ImagePath { get; set; }
    }
}
