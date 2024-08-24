using EventsWebApplication.Server.Domain.Entities;

namespace EventsWebApplication.Server.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByEmailAsync(string email);
        Task<IEnumerable<Event>> GetRegisteredEventsAsync(int userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
    }
}
