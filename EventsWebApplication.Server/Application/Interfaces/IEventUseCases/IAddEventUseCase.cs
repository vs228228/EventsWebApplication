using EventsWebApplication.Server.Application.DTOs;

namespace EventsWebApplication.Server.Application.Interfaces.IEventUseCases
{
    public interface IAddEventUseCase
    {
        public Task ExecuteAsync(EventCreateDto eventObject, IFormFile photo);
    }
}
