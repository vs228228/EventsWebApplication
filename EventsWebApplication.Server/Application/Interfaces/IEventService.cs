using EventsWebApplication.Server.Application.DTOs;
using EventsWebApplication.Server.Domain.Entities;

namespace EventsWebApplication.Server.Application.Interfaces
{
    public interface IEventService
    {
        Task<Event> GetEventByIdAsync(int id);
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task RegisterUserToEventAsync(UserEventIdDto userEventInfo);
        Task UnregisterUserFromEventAsync(UserEventIdDto userEventInfo);
        Task AddEventAsync(Event eventObject);
        Task UpdateEventAsync(Event eventObject);
        Task DeleteEventAsync(int id);
    }
}
