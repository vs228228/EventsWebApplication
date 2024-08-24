using EventsWebApplication.Server.Domain.Entities;
using EventsWebApplication.Server.Domain.Interfaces;
using EventsWebApplication.Server.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EventsWebApplication.Server.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            User user = await _context.Users.FindAsync(id);
             _context.Users.Remove(user);
        }
        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return  _context.Users.ToList();
        }

        public async Task<IEnumerable<Event>> GetRegisteredEventsAsync(int userId)
        {
            return await _context.Participants
                .Where(ep => ep.UserId == userId)
                .Select(ep => ep.Event)
                .ToListAsync();
        }

        public Task<User> GetUserByEmailAsync(string email)
        {
            return _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}
