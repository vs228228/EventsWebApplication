using EventsWebApplication.Server.Application.DTOs.UserDTOs;

namespace EventsWebApplication.Server.Application.Interfaces.IEventUseCases
{
    public interface IUnregisterUserFromEventUseCase
    {
        public Task ExecuteAsync(UserEventIdDto userEventInfo);
    }
}
