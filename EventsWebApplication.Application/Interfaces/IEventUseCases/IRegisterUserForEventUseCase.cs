using EventsWebApplication.Application.DTOs.UserDTOs;

namespace EventsWebApplication.Application.Interfaces.IEventUseCases
{
    public interface IRegisterUserForEventUseCase
    {
        public Task ExecuteAsync(UserEventIdDto userEventInfo);
    }
}
