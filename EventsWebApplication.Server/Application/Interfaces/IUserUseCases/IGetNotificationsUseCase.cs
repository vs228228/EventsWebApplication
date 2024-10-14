using EventsWebApplication.Server.Application.DTOs;
using EventsWebApplication.Server.Application.Pagination;

namespace EventsWebApplication.Server.Application.Interfaces.IUserUseCases
{
    public interface IGetNotificationsUseCase
    {
        public Task<PagedResult<NotificationDto>> ExecuteAsync(int userId, int pageNumber, int pageSize);
    }
}
