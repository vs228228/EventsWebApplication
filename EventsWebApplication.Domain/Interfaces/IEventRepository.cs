using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Domain.Interfaces
{
    public interface IEventRepository : IRepository<Event>
    {
        Task<KeyValuePair<IEnumerable<Event>, int>> GetPagedAsync(int pageNumber, int pageSize, string searchString);
        Task<IEnumerable<User>> GetUsersByEventIdAsync(int eventId);
        Task<User?> IsUserRegisterToEvent(int eventId, int userId);
        Task RegisterUserForEventAsync(int userId, int eventId);
        Task UnregisterUserFromEventAsync(int userId, int eventId);

    }

}
