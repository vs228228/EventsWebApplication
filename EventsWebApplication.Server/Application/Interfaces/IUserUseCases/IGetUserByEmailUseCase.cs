using EventsWebApplication.Server.Application.DTOs;

namespace EventsWebApplication.Server.Application.Interfaces.IUserUseCases
{
    public interface IGetUserByEmailUseCase
    {
        public Task<UserDto> ExecuteAsync(string email);
    }
}
