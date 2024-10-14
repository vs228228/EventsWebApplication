using EventsWebApplication.Application.DTOs.UserDTOs;

namespace EventsWebApplication.Application.Interfaces.IUserUseCases
{
    public interface IGetUserByEmailUseCase
    {
        public Task<UserResponseDto> ExecuteAsync(string email);
    }
}
