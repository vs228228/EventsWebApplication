using EventsWebApplication.Application.DTOs.UserDTOs;

namespace EventsWebApplication.Application.Interfaces.IEventUseCases
{
    public interface ICheckUserRegisterForEventUseCase
    {
        public Task<bool> ExecuteAsync(UserEventIdDto userEventInfo);
    }
}
