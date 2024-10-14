using EventsWebApplication.Server.Application.DTOs.UserDTOs;

namespace EventsWebApplication.Server.Application.Interfaces.IUserUseCases
{
    public interface ITryAddUserUseCase
    {
        public Task ExecuteAsync(UserCreateResponseDto userCreateDto);
    }
}
