namespace EventsWebApplication.Server.Application.DTOs.EventDTOs.Responses
{
    public class EventResponseDto : EventDtoBase
    {
        public int Id { get; set; }
        public int CountOfParticipants { get; set; }
        public string ImagePath { get; set; }
    }
}
