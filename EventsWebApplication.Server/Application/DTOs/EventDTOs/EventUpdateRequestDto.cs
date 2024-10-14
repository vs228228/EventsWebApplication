using EventsWebApplication.Server.Domain.Entities;

namespace EventsWebApplication.Server.Application.DTOs.EventDTOs
{
    public class EventUpdateRequestDto : EventDtoBase
    {
        public int Id { get; set; }
    }
}
