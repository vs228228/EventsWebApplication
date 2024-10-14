using EventsWebApplication.Application.DTOs.UserDTOs;

namespace EventsWebApplication.Application.Interfaces.IUserUseCases
{
    public interface IUpdateUserUseCase
    {
        public Task ExecuteAsync(UserUpdateRequestDto userUpdateDto);
    }
}
