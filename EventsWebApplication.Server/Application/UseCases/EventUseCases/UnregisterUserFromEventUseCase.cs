using EventsWebApplication.Server.Application.DTOs;
using EventsWebApplication.Server.Application.Interfaces.IEventUseCases;
using EventsWebApplication.Server.Domain.Interfaces;

namespace EventsWebApplication.Server.Application.UseCases.EventUseCases
{
    public class UnregisterUserFromEventUseCase : IUnregisterUserFromEventUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnregisterUserFromEventUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(UserEventIdDto userEventInfo)
        {
            await _unitOfWork.Events.UnregisterUserFromEventAsync(userEventInfo.UserId, userEventInfo.EventId);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
