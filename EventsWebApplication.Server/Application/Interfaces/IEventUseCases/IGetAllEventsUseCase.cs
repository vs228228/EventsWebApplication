using EventsWebApplication.Server.Application.DTOs;

namespace EventsWebApplication.Server.Application.Interfaces.IEventUseCases
{
    public interface IGetAllEventsUseCase
    {
        public Task<IEnumerable<EventDto>> ExecuteAsync();
    }
}
