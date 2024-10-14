using EventsWebApplication.Server.Application.DTOs.UserDTOs;

namespace EventsWebApplication.Server.Application.Interfaces.IEventUseCases
{
    public interface IGetUsersByEventIdUseCase
    {
        public Task<IEnumerable<UserResponseDto>> ExecuteAsync(int eventId);
    }
}
