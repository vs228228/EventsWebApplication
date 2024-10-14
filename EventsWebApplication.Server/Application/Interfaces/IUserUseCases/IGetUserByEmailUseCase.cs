using EventsWebApplication.Server.Application.DTOs.UserDTOs;

namespace EventsWebApplication.Server.Application.Interfaces.IUserUseCases
{
    public interface IGetUserByEmailUseCase
    {
        public Task<UserResponseDto> ExecuteAsync(string email);
    }
}
