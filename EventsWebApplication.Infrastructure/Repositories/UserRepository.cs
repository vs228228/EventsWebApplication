using EventsWebApplication.Infrastructure.Data;
using EventsWebApplication.Application.Pagination;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventsWebApplication.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
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

        public async Task<KeyValuePair<IEnumerable<User>, int>> GetPagedAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _context.Users.CountAsync();
            var users = await _context.Users
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new KeyValuePair<IEnumerable<User>, int>(users, totalCount);

        }

    }
}
