using EventsWebApplication.Server.Domain.Entities;
using EventsWebApplication.Server.Domain.Pagination;

namespace EventsWebApplication.Server.Domain.Interfaces
{
    public interface IEventRepository : IRepository<Event>
    {
        // к сожалению слово event зарезервированно, потому приходится вот так выкручиваться
   //     Task<Event> GetByIdAsync(int id);
    //    Task<IEnumerable<Event>> GetAllAsync();
        Task<PagedResult<Event>> GetPagedAsync(int pageNumber, int pageSize, string searchString);
        Task<IEnumerable<User>> GetUsersByEventIdAsync(int eventId);
        Task<User?> IsUserRegisterToEvent(int eventId, int userId);
        Task RegisterUserForEventAsync(int userId, int eventId);
        Task UnregisterUserFromEventAsync(int userId, int eventId);
    //    Task AddAsync(Event eventObject);
    //    Task UpdateAsync(Event eventOjbect);
    //    Task DeleteAsync(int id);
    }

}
