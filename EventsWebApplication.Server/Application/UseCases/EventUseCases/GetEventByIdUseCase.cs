using AutoMapper;
using EventsWebApplication.Server.Application.DTOs;
using EventsWebApplication.Server.Application.Interfaces.IEventUseCases;
using EventsWebApplication.Server.Domain.Entities;
using EventsWebApplication.Server.Domain.Interfaces;

namespace EventsWebApplication.Server.Application.UseCases.EventUseCases
{
    public class GetEventByIdUseCase: IGetEventByIdUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEventByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<EventDto> ExecuteAsync(int id)
        {
            Event eventObject = await _unitOfWork.Events.GetByIdAsync(id);
            if(eventObject == null) throw new KeyNotFoundException(); 
            return _mapper.Map<EventDto>(eventObject);
        }
    }
}
