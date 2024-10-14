using EventsWebApplication.Application.DTOs.UserDTOs;
using EventsWebApplication.Application.Interfaces;
using EventsWebApplication.Application.Interfaces.IUserUseCases;
using EventsWebApplication.Application.Services;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Interfaces;

namespace EventsWebApplication.Application.UseCases.UserUseCases
{
    public class TryAuthenticateUseCase : ITryAuthenticateUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private ITokenManager _tokenManager;

        public TryAuthenticateUseCase(IUnitOfWork unitOfWork, IMapperService mapper, IPasswordHasher passwordHasher, ITokenManager tokenManager)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _tokenManager = tokenManager;
        }

        public async Task<string> ExecuteAsync(UserAuthDto loginDto)
        {
            User user = await _unitOfWork.Users.GetUserByEmailAsync(loginDto.UserEmail);
            if (user == null)
            {
                throw new ArgumentException("Wrong email");
            }
            if (!await _passwordHasher.VerifyPassword(user.Password, loginDto.UserPassword))
            {
                throw new ArgumentException("Wrong password");
            }
            var token = _tokenManager.GenerateRefreshToken();
            user.RefreshToken = token.Token;
            user.Expiration = token.Expiration;
            await _unitOfWork.SaveChangesAsync();
            return token.Token;
        }
    }
}
