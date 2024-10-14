using EventsWebApplication.Application.DTOs.EventDTOs;
using Microsoft.AspNetCore.Http;

namespace EventsWebApplication.Application.Interfaces.IEventUseCases
{
    public interface IUpdateEventUseCase
    {
        public Task ExecuteAsync(EventUpdateRequestDto eventObject, IFormFile photo);
    }
}
