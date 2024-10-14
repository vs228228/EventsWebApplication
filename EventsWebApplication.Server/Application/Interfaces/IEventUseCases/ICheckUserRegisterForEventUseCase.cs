using EventsWebApplication.Server.Application.DTOs.UserDTOs;

namespace EventsWebApplication.Server.Application.Interfaces.IEventUseCases
{
    public interface ICheckUserRegisterForEventUseCase
    {
        public Task<bool> ExecuteAsync(UserEventIdDto userEventInfo);
    }
}
