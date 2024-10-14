using EventsWebApplication.Application.DTOs.UserDTOs;
using EventsWebApplication.Application.Interfaces.IEventUseCases;
using EventsWebApplication.Application.Services;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Interfaces;

namespace EventsWebApplication.Application.UseCases.EventUseCases
{
    public class GetUsersByEventIdUseCase : IGetUsersByEventIdUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperService _mapper;
        public GetUsersByEventIdUseCase(IUnitOfWork unitOfWork, IMapperService mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserResponseDto>> ExecuteAsync(int eventId)
        {
            var users = await _unitOfWork.Events.GetUsersByEventIdAsync(eventId);
            return await _mapper.Map<IEnumerable<User>, IEnumerable<UserResponseDto>>(users);
        }
    }
}
