namespace EventsWebApplication.Server.Application.DTOs.EventDTOs
{
    public class EventDtoBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateAndTime { get; set; }
        public string Place { get; set; }
        public string Type { get; set; }
        public int MaxParticipants { get; set; }
    }
}
