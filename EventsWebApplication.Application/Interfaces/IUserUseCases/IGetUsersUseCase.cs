using EventsWebApplication.Application.DTOs.UserDTOs;
using EventsWebApplication.Application.Pagination;

namespace EventsWebApplication.Application.Interfaces.IUserUseCases
{
    public interface IGetUsersUseCase
    {
        public Task<PagedResult<UserResponseDto>> ExecuteAsync(int pageNumber, int pageSize);
    }
}
