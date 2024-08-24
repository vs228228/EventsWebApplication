using EventsWebApplication.Server.Application.DTOs;
using EventsWebApplication.Server.Domain.Entities;

namespace EventsWebApplication.Server.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByEmailAsync(string email);
        Task<IEnumerable<Event>> GetRegisteredEventsAsync(int userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<string> TryAuthenticateAsync(UserAuthDto loginDto);
        Task<string> TryAddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
    }
}
