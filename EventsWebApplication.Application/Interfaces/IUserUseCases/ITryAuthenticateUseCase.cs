using EventsWebApplication.Application.DTOs.UserDTOs;

namespace EventsWebApplication.Application.Interfaces.IUserUseCases
{
    public interface ITryAuthenticateUseCase
    {
        public Task<string> ExecuteAsync(UserAuthDto loginDto);
    }
}
