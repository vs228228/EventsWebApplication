using EventsWebApplication.Server.Application.DTOs.UserDTOs;
using EventsWebApplication.Server.Application.Interfaces.IEventUseCases;
using EventsWebApplication.Server.Domain.Interfaces;

namespace EventsWebApplication.Server.Application.UseCases.EventUseCases
{
    public class CheckUserRegistetForEventUseCase : ICheckUserRegisterForEventUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CheckUserRegistetForEventUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> ExecuteAsync(UserEventIdDto userEventInfo)
        {
            var user = await _unitOfWork.Events.IsUserRegisterToEvent(userEventInfo.EventId, userEventInfo.UserId);
            if (user == null) return false;
            return true;
        }
    }
}
