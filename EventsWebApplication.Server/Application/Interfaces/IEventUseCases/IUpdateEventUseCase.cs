using EventsWebApplication.Server.Application.DTOs;

namespace EventsWebApplication.Server.Application.Interfaces.IEventUseCases
{
    public interface IUpdateEventUseCase
    {
        public Task ExecuteAsync(EventUpdateDto eventObject, IFormFile photo);
    }
}
