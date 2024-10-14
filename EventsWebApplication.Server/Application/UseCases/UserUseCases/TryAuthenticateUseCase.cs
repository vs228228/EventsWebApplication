using AutoMapper;
using EventsWebApplication.Server.Application.DTOs.UserDTOs;
using EventsWebApplication.Server.Application.Interfaces;
using EventsWebApplication.Server.Application.Interfaces.IUserUseCases;
using EventsWebApplication.Server.Domain.Entities;
using EventsWebApplication.Server.Domain.Interfaces;

namespace EventsWebApplication.Server.Application.UseCases.UserUseCases
{
    public class TryAuthenticateUseCase : ITryAuthenticateUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private ITokenManager _tokenManager;

        public TryAuthenticateUseCase(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher passwordHasher, ITokenManager tokenManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
