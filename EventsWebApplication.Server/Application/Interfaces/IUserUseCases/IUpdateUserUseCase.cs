using EventsWebApplication.Server.Application.DTOs;

namespace EventsWebApplication.Server.Application.Interfaces.IUserUseCases
{
    public interface IUpdateUserUseCase
    {
        public Task ExecuteAsync(UserUpdateDto userUpdateDto);
    }
}
