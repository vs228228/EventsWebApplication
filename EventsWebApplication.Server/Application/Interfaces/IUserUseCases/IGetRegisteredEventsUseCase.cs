using EventsWebApplication.Server.Application.DTOs.EventDTOs;
using EventsWebApplication.Server.Application.DTOs.EventDTOs.Responses;

namespace EventsWebApplication.Server.Application.Interfaces.IUserUseCases
{
    public interface IGetRegisteredEventsUseCase
    {
        public Task<IEnumerable<EventResponseDto>> ExecuteAsync(int userId);
    }
}
