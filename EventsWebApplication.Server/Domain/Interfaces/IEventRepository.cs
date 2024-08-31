using EventsWebApplication.Server.Application.Pagination;
using EventsWebApplication.Server.Domain.Entities;

namespace EventsWebApplication.Server.Domain.Interfaces
{
    public interface IEventRepository
    {
        // к сожалению слово event зарезервированно, потому приходится вот так выкручиваться
        Task<Event> GetEventByIdAsync(int id);
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<PagedResult<Event>> GetEvensAsync(int pageNumber, int pageSize);
        Task<IEnumerable<User>> GetUsersByEventIdAsync(int eventId);
        Task RegisterUserForEventAsync(int userId, int eventId);
        Task UnregisterUserFromEventAsync(int userId, int eventId);
        Task AddEventAsync(Event eventObject);
        Task UpdateEventAsync(Event eventOjbect);
        Task DeleteEventAsync(int id);
    }

}
