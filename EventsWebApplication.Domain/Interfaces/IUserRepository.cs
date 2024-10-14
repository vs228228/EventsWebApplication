using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        //   Task<User> GetByIdAsync(int id);
        Task<User> GetUserByEmailAsync(string email);
        Task<IEnumerable<Event>> GetRegisteredEventsAsync(int userId);
        //   Task<IEnumerable<User>> GetAllAsync();
        Task<KeyValuePair<IEnumerable<User>, int>> GetPagedAsync(int pageNumber, int pageSize);
        //   Task AddAsync(User user);
        //   Task UpdateAsync(User user);
        //   Task DeleteAsync(int id);
    }
}
