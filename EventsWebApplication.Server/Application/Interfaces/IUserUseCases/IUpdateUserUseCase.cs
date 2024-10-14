using EventsWebApplication.Server.Application.DTOs.UserDTOs;

namespace EventsWebApplication.Server.Application.Interfaces.IUserUseCases
{
    public interface IUpdateUserUseCase
    {
        public Task ExecuteAsync(UserUpdateRequestDto userUpdateDto);
    }
}
