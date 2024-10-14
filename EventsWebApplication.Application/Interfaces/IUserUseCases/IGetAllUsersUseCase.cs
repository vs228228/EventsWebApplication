using EventsWebApplication.Application.DTOs.UserDTOs;

namespace EventsWebApplication.Application.Interfaces.IUserUseCases
{
    public interface IGetAllUsersUseCase
    {
        public Task<IEnumerable<UserResponseDto>> ExecuteAsync();
    }
}
