using EventsWebApplication.Application.DTOs.EventDTOs;
using EventsWebApplication.Application.Pagination;

namespace EventsWebApplication.Application.Interfaces.IEventUseCases
{
    public interface IGetEventsUseCase
    {
        public Task<PagedResult<EventResponseDto>> ExecuteAsync(int pageNumber, int pageSize, string searchString);
    }
}
