using EventsWebApplication.Server.Application.DTOs;

namespace EventsWebApplication.Server.Application.Interfaces.IUserUseCases
{
    public interface IGetUserByIdUseCase
    {
        public Task<UserDto> ExecuteAsync(int id);
    }
}
