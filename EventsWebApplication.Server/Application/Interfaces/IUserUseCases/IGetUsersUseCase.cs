using EventsWebApplication.Server.Application.DTOs;
using EventsWebApplication.Server.Domain.Pagination;

namespace EventsWebApplication.Server.Application.Interfaces.IUserUseCases
{
    public interface IGetUsersUseCase
    {
        public Task<PagedResult<UserDto>> ExecuteAsync(int pageNumber, int pageSize);
    }
}
