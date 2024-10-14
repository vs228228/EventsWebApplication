using EventsWebApplication.Application.DTOs.EventDTOs;
using EventsWebApplication.Application.Interfaces.IEventUseCases;
using EventsWebApplication.Application.Pagination;
using EventsWebApplication.Application.Services;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Interfaces;

namespace EventsWebApplication.Application.UseCases.EventUseCases
{
    public class GetEventsUseCase : IGetEventsUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperService _mapper;

        public GetEventsUseCase(IUnitOfWork unitOfWork, IMapperService mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResult<EventResponseDto>> ExecuteAsync(int pageNumber, int pageSize, string searchString)
        {
            var events = await _unitOfWork.Events.GetPagedAsync(pageNumber, pageSize, searchString);
            PagedResult<EventResponseDto> result = new PagedResult<EventResponseDto>
            {
                Items = await _mapper.Map<IEnumerable<Event>, IEnumerable<EventResponseDto>>(events.Key),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = events.Value
            };

            return result;
        }
    }
}
