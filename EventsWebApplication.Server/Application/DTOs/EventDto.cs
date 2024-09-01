using EventsWebApplication.Server.Domain.Entities;

namespace EventsWebApplication.Server.Application.DTOs
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateAndTime { get; set; }
        public string Place { get; set; }
        public string Type { get; set; }
        public int MaxParticipants { get; set; }
        public int CountOfParticipants { get; set; }
        public string ImagePath { get; set; }
    }
}
