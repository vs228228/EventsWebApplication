using EventsWebApplication.Application.DTOs.EventDTOs;
using EventsWebApplication.Application.Interfaces.IEventUseCases;
using EventsWebApplication.Application.Services;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Interfaces;


namespace EventsWebApplication.Application.UseCases.EventUseCases
{
    public class GetAllEventsUseCase : IGetAllEventsUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperService _mapper;
        public GetAllEventsUseCase(IUnitOfWork unitOfWork, IMapperService mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EventResponseDto>> ExecuteAsync()
        {
            var events = await _unitOfWork.Events.GetAllAsync();
            return await _mapper.Map<IEnumerable<Event>, IEnumerable<EventResponseDto>>(events);
        }
    }
}
