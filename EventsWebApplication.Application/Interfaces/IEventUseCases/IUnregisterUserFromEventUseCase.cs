using EventsWebApplication.Application.DTOs.UserDTOs;

namespace EventsWebApplication.Application.Interfaces.IEventUseCases
{
    public interface IUnregisterUserFromEventUseCase
    {
        public Task ExecuteAsync(UserEventIdDto userEventInfo);
    }
}
