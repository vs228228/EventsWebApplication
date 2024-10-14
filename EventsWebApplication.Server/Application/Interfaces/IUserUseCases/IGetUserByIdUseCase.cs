using EventsWebApplication.Server.Application.DTOs.UserDTOs;

namespace EventsWebApplication.Server.Application.Interfaces.IUserUseCases
{
    public interface IGetUserByIdUseCase
    {
        public Task<UserResponseDto> ExecuteAsync(int id);
    }
}
