using EventsWebApplication.Server.Application.DTOs.EventDTOs;
using EventsWebApplication.Server.Application.DTOs.EventDTOs.Responses;

namespace EventsWebApplication.Server.Application.Interfaces.IEventUseCases
{
    public interface IGetAllEventsUseCase
    {
        public Task<IEnumerable<EventResponseDto>> ExecuteAsync();
    }
}
