using EventsWebApplication.Application.DTOs.EventDTOs;
using EventsWebApplication.Application.Interfaces.IUserUseCases;
using EventsWebApplication.Application.Services;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Interfaces;

namespace EventsWebApplication.Application.UseCases.UserUseCases
{
    public class GetRegisteredEventsUseCase : IGetRegisteredEventsUseCase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperService _mapper;

        public GetRegisteredEventsUseCase(IUnitOfWork unitOfWork, IMapperService mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EventResponseDto>> ExecuteAsync(int userId)
        {
            var events = await _unitOfWork.Users.GetRegisteredEventsAsync(userId);
            return await _mapper.Map<IEnumerable<Event>, IEnumerable<EventResponseDto>>(events);
        }
    }
}
