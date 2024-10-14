using EventsWebApplication.Application.DTOs.EventDTOs;

namespace EventsWebApplication.Application.Interfaces.IEventUseCases
{
    public interface IGetEventByIdUseCase
    {
        public Task<EventResponseDto> ExecuteAsync(int id);
    }
}
