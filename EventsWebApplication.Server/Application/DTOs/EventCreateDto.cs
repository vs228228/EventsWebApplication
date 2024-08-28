using EventsWebApplication.Server.Domain.Entities;

namespace EventsWebApplication.Server.Application.DTOs
{
    public class EventCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateAndTime { get; set; }
        public string Place { get; set; }
        public string Type { get; set; }
        public int MaxPacticipants { get; set; }
    }
}
