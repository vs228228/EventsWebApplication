using EventsWebApplication.Application.DTOs.EventDTOs;
using Microsoft.AspNetCore.Http;

namespace EventsWebApplication.Application.Interfaces.IEventUseCases
{
    public interface IAddEventUseCase
    {
        public Task ExecuteAsync(EventCreateRequestDto eventObject, IFormFile photo);
    }
}
