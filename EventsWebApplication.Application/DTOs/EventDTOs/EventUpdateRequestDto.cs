using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Application.DTOs.EventDTOs
{
    public class EventUpdateRequestDto : EventDtoBase
    {
        public int Id { get; set; }
    }
}
