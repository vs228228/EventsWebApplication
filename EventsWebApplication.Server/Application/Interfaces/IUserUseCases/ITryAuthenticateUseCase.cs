using EventsWebApplication.Server.Application.DTOs;

namespace EventsWebApplication.Server.Application.Interfaces.IUserUseCases
{
    public interface ITryAuthenticateUseCase
    {
        public Task<string> ExecuteAsync(UserAuthDto loginDto);
    }
}
