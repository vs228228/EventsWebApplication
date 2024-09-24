using EventsWebApplication.Server.Application.DTOs;
using EventsWebApplication.Server.Domain.Pagination;

namespace EventsWebApplication.Server.Application.Interfaces.IEventUseCases
{
    public interface IGetEventsUseCase
    {
        public Task<PagedResult<EventDto>> ExecuteAsync(int pageNumber, int pageSize, string searchString);
    }
}
