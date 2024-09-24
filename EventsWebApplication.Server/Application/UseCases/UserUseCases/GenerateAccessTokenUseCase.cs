using EventsWebApplication.Server.Application.DTOs;
using EventsWebApplication.Server.Application.Interfaces.IUserUseCases;
using EventsWebApplication.Server.Domain.Interfaces;
using FluentValidation;

namespace EventsWebApplication.Server.Application.UseCases.UserUseCases
{
    public class GenerateAccessTokenUseCase : IGenerateAccessTokenUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenManager _tokenManager;

        public GenerateAccessTokenUseCase(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _unitOfWork = unitOfWork;
            _tokenManager = tokenManager;
        }

        public async Task<TokenDto> ExecuteAsync(GetTokenDto getTokenDto)
        {
            if (getTokenDto.RefreshToken == null || getTokenDto.userId == null)
            {
                throw new ValidationException("Необходимо заполнить все поля");
            }
            var user = await _unitOfWork.Users.GetByIdAsync(getTokenDto.userId);
            if (user == null || user.RefreshToken != getTokenDto.RefreshToken || user.Expiration < DateTime.Now)
            {
                throw new UnauthorizedAccessException();
            }
            var refreshToken = _tokenManager.GenerateRefreshToken();
            user.RefreshToken = refreshToken.Token;
            user.Expiration = refreshToken.Expiration;
            var accesToken = _tokenManager.GenerateAccessToken(user);
            TokenDto tokenDto = new TokenDto()
            {
                AccessToken = accesToken,
                RefreshToken = refreshToken.Token
            };
            await _unitOfWork.SaveChangesAsync();
            return tokenDto;
        }
    }
}
