using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Domain.Interfaces
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Task<KeyValuePair<IEnumerable<Notification>, int>> GetPagedAsync(int userId, int pageNumber, int pageSize);

    }

}
