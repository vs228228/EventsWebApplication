using EventsWebApplication.Server.Application.DTOs;

namespace EventsWebApplication.Server.Application.Interfaces.IEventUseCases
{
    public interface IGetEventByIdUseCase
    {
        public Task<EventDto> ExecuteAsync(int id);
    }
}
