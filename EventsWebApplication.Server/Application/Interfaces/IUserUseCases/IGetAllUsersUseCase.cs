using EventsWebApplication.Server.Application.DTOs;

namespace EventsWebApplication.Server.Application.Interfaces.IUserUseCases
{
    public interface IGetAllUsersUseCase
    {
        public Task<IEnumerable<UserDto>> ExecuteAsync();
    }
}
