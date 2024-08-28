using EventsWebApplication.Server.Domain.Interfaces;
using EventsWebApplication.Server.Infrastructure.Data;

namespace EventsWebApplication.Server.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Events = new EventRepository(_context);
            Users = new UserRepository(_context);
            Notifications = new NotificationRepository(_context);
        }

        public IEventRepository Events { get; set; }
        public IUserRepository Users { get; set; }
        public INotificationRepository Notifications { get; set; }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
