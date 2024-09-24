using EventsWebApplication.Server.Application.DTOs;

namespace EventsWebApplication.Server.Application.Interfaces.IEventUseCases
{
    public interface IRegisterUserForEventUseCase
    {
        public Task ExecuteAsync(UserEventIdDto userEventInfo);
    }
}
