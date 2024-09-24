using EventsWebApplication.Server.Application.DTOs;
using EventsWebApplication.Server.Domain.Pagination;

namespace EventsWebApplication.Server.Application.Interfaces.IUserUseCases
{
    public interface IGetNotificationsUseCase
    {
        public Task<PagedResult<NotificationDto>> ExecuteAsync(int userId, int pageNumber, int pageSize);
    }
}
