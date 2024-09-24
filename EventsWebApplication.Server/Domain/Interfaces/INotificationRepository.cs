using EventsWebApplication.Server.Domain.Entities;
using EventsWebApplication.Server.Domain.Pagination;

namespace EventsWebApplication.Server.Domain.Interfaces
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Task<PagedResult<Notification>> GePagedAsync(int userId, int pageNumber, int pageSize);
        /*Task<Notification> GetByIdAsync(int id);
        Task<IEnumerable<Notification>> GetAllAsync();
        Task AddAsync(Notification notification);
        Task UpdateAsync(Notification notification);
        Task DeleteAsync(int id); */
    }

}
