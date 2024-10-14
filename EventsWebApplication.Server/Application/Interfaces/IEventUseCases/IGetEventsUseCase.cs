using EventsWebApplication.Server.Application.DTOs.EventDTOs;
using EventsWebApplication.Server.Application.DTOs.EventDTOs.Responses;
using EventsWebApplication.Server.Application.Pagination;

namespace EventsWebApplication.Server.Application.Interfaces.IEventUseCases
{
    public interface IGetEventsUseCase
    {
        public Task<PagedResult<EventResponseDto>> ExecuteAsync(int pageNumber, int pageSize, string searchString);
    }
}
