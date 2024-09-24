using EventsWebApplication.Server.Application.DTOs;

namespace EventsWebApplication.Server.Application.Interfaces.IUserUseCases
{
    public interface IGetRegisteredEventsUseCase
    {
        public Task<IEnumerable<EventDto>> ExecuteAsync(int userId);
    }
}
