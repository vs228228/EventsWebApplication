using EventsWebApplication.Server.Domain.Entities;

namespace EventsWebApplication.Server.Domain.Interfaces
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Task<KeyValuePair<IEnumerable<Notification>, int>> GePagedAsync(int userId, int pageNumber, int pageSize);
        
    }

}
