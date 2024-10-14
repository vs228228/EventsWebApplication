using EventsWebApplication.Application.DTOs.EventDTOs;
using EventsWebApplication.Application.DTOs.UserDTOs;
using EventsWebApplication.Application.Pagination;
using EventsWebApplication.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace EventsWebApplication.Application.Interfaces
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
