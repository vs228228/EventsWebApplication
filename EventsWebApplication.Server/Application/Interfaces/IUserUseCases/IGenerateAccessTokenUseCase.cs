using EventsWebApplication.Server.Application.DTOs;

namespace EventsWebApplication.Server.Application.Interfaces.IUserUseCases
{
    public interface IGenerateAccessTokenUseCase
    {
        public Task<TokenDto> ExecuteAsync(GetTokenDto getTokenDto);
    }
}
