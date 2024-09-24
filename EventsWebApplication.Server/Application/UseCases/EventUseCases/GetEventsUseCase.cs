using AutoMapper;
using EventsWebApplication.Server.Application.DTOs;
using EventsWebApplication.Server.Application.Interfaces.IEventUseCases;
using EventsWebApplication.Server.Domain.Interfaces;
using EventsWebApplication.Server.Domain.Pagination;

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

        public async Task<PagedResult<EventDto>> ExecuteAsync(int pageNumber, int pageSize, string searchString)
        {
            var events = await _unitOfWork.Events.GetPagedAsync(pageNumber, pageSize, searchString);
            return _mapper.Map<PagedResult<EventDto>>(events);
        }
    }
}
