using EventsWebApplication.Server.Application.DTOs.EventDTOs;

namespace EventsWebApplication.Server.Application.Interfaces.IEventUseCases
{
    public interface IAddEventUseCase
    {
        public Task ExecuteAsync(EventCreateRequestDto eventObject, IFormFile photo);
    }
}
