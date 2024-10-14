using EventsWebApplication.Server.Application.DTOs.EventDTOs;

namespace EventsWebApplication.Server.Application.Interfaces.IEventUseCases
{
    public interface IUpdateEventUseCase
    {
        public Task ExecuteAsync(EventUpdateRequestDto eventObject, IFormFile photo);
    }
}
