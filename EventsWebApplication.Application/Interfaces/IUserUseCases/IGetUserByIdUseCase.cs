using EventsWebApplication.Application.DTOs.UserDTOs;

namespace EventsWebApplication.Application.Interfaces.IUserUseCases
{
    public interface IGetUserByIdUseCase
    {
        public Task<UserResponseDto> ExecuteAsync(int id);
    }
}
