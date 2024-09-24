using EventsWebApplication.Server.Application.DTOs;

namespace EventsWebApplication.Server.Application.Interfaces.IEventUseCases
{
    public interface ICheckUserRegisterForEventUseCase
    {
        public Task<bool> ExecuteAsync(UserEventIdDto userEventInfo);
    }
}
