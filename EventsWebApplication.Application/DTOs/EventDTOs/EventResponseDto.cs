namespace EventsWebApplication.Application.DTOs.EventDTOs
{
    public class EventResponseDto : EventDtoBase
    {
        public int Id { get; set; }
        public int CountOfParticipants { get; set; }
        public string ImagePath { get; set; }
    }
}
