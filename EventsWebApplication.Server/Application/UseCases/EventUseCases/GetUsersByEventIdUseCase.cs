using AutoMapper;
using EventsWebApplication.Server.Application.DTOs;
using EventsWebApplication.Server.Application.Interfaces.IEventUseCases;
using EventsWebApplication.Server.Domain.Interfaces;

namespace EventsWebApplication.Server.Application.UseCases.EventUseCases
{
    public class GetUsersByEventIdUseCase : IGetUsersByEventIdUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetUsersByEventIdUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserDto>> ExecuteAsync(int eventId)
        {
            var users = await _unitOfWork.Events.GetUsersByEventIdAsync(eventId);
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }
    }
}
