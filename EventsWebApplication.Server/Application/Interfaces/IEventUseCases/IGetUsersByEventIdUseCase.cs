using EventsWebApplication.Server.Application.DTOs;

namespace EventsWebApplication.Server.Application.Interfaces.IEventUseCases
{
    public interface IGetUsersByEventIdUseCase
    {
        public Task<IEnumerable<UserDto>> ExecuteAsync(int eventId);
    }
}
