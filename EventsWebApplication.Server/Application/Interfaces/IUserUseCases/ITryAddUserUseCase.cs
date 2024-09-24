using EventsWebApplication.Server.Application.DTOs;

namespace EventsWebApplication.Server.Application.Interfaces.IUserUseCases
{
    public interface ITryAddUserUseCase
    {
        public Task ExecuteAsync(UserCreateDto userCreateDto);
    }
}
