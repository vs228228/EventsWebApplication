namespace EventsWebApplication.Server.Domain.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public  DateTime DateAndTime{ get; set; }
        public string Place {  get; set; } // не присутстует в бд
        public string Type { get; set; }
        public int MaxPacticipants { get; set; }
        public ICollection<EventParticipant> Participants { get; set; }

        // добавить фотку
    }
}
