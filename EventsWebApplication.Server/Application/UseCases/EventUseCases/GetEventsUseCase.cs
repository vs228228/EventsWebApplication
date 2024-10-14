using AutoMapper;
using EventsWebApplication.Server.Application.DTOs.EventDTOs;
using EventsWebApplication.Server.Application.DTOs.EventDTOs.Responses;
using EventsWebApplication.Server.Application.Interfaces.IEventUseCases;
using EventsWebApplication.Server.Application.Pagination;
using EventsWebApplication.Server.Domain.Interfaces;

namespace EventsWebApplication.Server.Application.UseCases.EventUseCases
{
    public class GetEventsUseCase : IGetEventsUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEventsUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResult<EventResponseDto>> ExecuteAsync(int pageNumber, int pageSize, string searchString)
        {
            var events = await _unitOfWork.Events.GetPagedAsync(pageNumber, pageSize, searchString);
            PagedResult<EventResponseDto> result = new PagedResult<EventResponseDto>
            {
                Items = _mapper.Map<IEnumerable<EventResponseDto>>(events.Key),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = events.Value
            };

            return result;
        }
    }
}
