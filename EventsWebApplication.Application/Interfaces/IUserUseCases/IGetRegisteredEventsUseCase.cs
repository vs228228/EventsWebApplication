using EventsWebApplication.Application.DTOs.EventDTOs;

namespace EventsWebApplication.Application.Interfaces.IUserUseCases
{
    public interface IGetRegisteredEventsUseCase
    {
        public Task<IEnumerable<EventResponseDto>> ExecuteAsync(int userId);
    }
}
