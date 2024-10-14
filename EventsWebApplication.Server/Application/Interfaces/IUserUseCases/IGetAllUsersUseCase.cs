using EventsWebApplication.Server.Application.DTOs.UserDTOs;

namespace EventsWebApplication.Server.Application.Interfaces.IUserUseCases
{
    public interface IGetAllUsersUseCase
    {
        public Task<IEnumerable<UserResponseDto>> ExecuteAsync();
    }
}
