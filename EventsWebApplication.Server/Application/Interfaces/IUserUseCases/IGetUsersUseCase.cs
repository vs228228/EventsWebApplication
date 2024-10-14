using EventsWebApplication.Server.Application.DTOs.UserDTOs;
using EventsWebApplication.Server.Application.Pagination;

namespace EventsWebApplication.Server.Application.Interfaces.IUserUseCases
{
    public interface IGetUsersUseCase
    {
        public Task<PagedResult<UserResponseDto>> ExecuteAsync(int pageNumber, int pageSize);
    }
}
