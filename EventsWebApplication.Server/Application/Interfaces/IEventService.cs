using EventsWebApplication.Server.Application.DTOs.EventDTOs;
using EventsWebApplication.Server.Application.DTOs.EventDTOs.Responses;
using EventsWebApplication.Server.Application.DTOs.UserDTOs;
using EventsWebApplication.Server.Application.Pagination;
using EventsWebApplication.Server.Domain.Entities;

namespace EventsWebApplication.Server.Application.Interfaces
{
    public interface IEventService
    {
        Task<EventResponseDto> GetEventByIdAsync(int id);
        Task<IEnumerable<EventResponseDto>> GetAllEventsAsync();
        Task<IEnumerable<UserResponseDto>> GetUsersByEventIdAsync(int eventId);
        Task<PagedResult<EventResponseDto>> GetEventsAsync(int pageNumber, int pageSize, string searchString);
        Task<bool> IsUserRegisterToEvent(UserEventIdDto userEventId);
        Task<bool> RegisterUserForEventAsync(UserEventIdDto userEventInfo);
        Task UnregisterUserFromEventAsync(UserEventIdDto userEventInfo);
        Task AddEventAsync(EventCreateRequestDto eventObject, IFormFile photo);
        Task UpdateEventAsync(EventUpdateRequestDto eventObject, IFormFile photo);
        Task DeleteEventAsync(int id);
    }
}
