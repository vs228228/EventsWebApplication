using EventsWebApplication.Application.DTOs.UserDTOs;

namespace EventsWebApplication.Application.Interfaces.IEventUseCases
{
    public interface IGetUsersByEventIdUseCase
    {
        public Task<IEnumerable<UserResponseDto>> ExecuteAsync(int eventId);
    }
}
