using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.Interfaces.IUserUseCases
{
    public interface IGenerateAccessTokenUseCase
    {
        public Task<TokenDto> ExecuteAsync(GetTokenDto getTokenDto);
    }
}
