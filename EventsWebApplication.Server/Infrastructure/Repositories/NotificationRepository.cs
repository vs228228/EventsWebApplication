using EventsWebApplication.Server.Application.Pagination;
using EventsWebApplication.Server.Domain.Entities;
using EventsWebApplication.Server.Domain.Interfaces;
using EventsWebApplication.Server.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EventsWebApplication.Server.Infrastructure.Repositories
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        private readonly ApplicationDbContext _context;
        public NotificationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<KeyValuePair<IEnumerable<Notification>, int>> GePagedAsync(int userId, int pageNumber, int pageSize)
        {
            var query = _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderBy(n => n.CreatedAt);

            var totalCount = await query.CountAsync();

            var notifications = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new KeyValuePair<IEnumerable<Notification>, int>(notifications, totalCount);
        }

        public Task UpdateAsync(Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
