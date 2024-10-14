using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Application.Pagination;

namespace EventsWebApplication.Application.Interfaces.IUserUseCases
{
    public interface IGetNotificationsUseCase
    {
        public Task<PagedResult<NotificationDto>> ExecuteAsync(int userId, int pageNumber, int pageSize);
    }
}
