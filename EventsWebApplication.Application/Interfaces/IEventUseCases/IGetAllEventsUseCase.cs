using EventsWebApplication.Application.DTOs.EventDTOs;

namespace EventsWebApplication.Application.Interfaces.IEventUseCases
{
    public interface IGetAllEventsUseCase
    {
        public Task<IEnumerable<EventResponseDto>> ExecuteAsync();
    }
}
