using EventsWebApplication.Server.Application.Pagination;
using EventsWebApplication.Server.Domain.Entities;

namespace EventsWebApplication.Server.Domain.Interfaces
{
    public interface INotificationRepository
    {
        Task<PagedResult<Notification>> GetNotificationsAsync(int userId, int pageNumber, int pageSize);
        Task<Notification> GetNotificationByIdAsync(int id);
        Task AddNotificationAsync(Notification notification);
        Task DeleteNotificationAsync(int id);
    }

}
