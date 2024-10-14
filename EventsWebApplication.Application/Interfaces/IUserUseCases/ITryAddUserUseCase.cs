using EventsWebApplication.Application.DTOs.UserDTOs;

namespace EventsWebApplication.Application.Interfaces.IUserUseCases
{
    public interface ITryAddUserUseCase
    {
        public Task ExecuteAsync(UserCreateRequestDto userCreateDto);
    }
}
