using EventsWebApplication.Application.DTOs.UserDTOs;
using EventsWebApplication.Application.Interfaces;
using EventsWebApplication.Application.Interfaces.IUserUseCases;
using EventsWebApplication.Application.Services;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Interfaces;

namespace EventsWebApplication.Application.UseCases.UserUseCases
{
    public class TryAddUserUseCase : ITryAddUserUseCase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperService _mapper;
        private readonly ITokenManager _tokenManager;
        private readonly IPasswordHasher _passwordHasher;

        public TryAddUserUseCase(IUnitOfWork unitOfWork, IMapperService mapper, ITokenManager tokenManager, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tokenManager = tokenManager;
            _passwordHasher = passwordHasher;
        }

        public async Task ExecuteAsync(UserCreateRequestDto userCreateDto)
        {

            var user = await _mapper.Map<UserCreateRequestDto,User>(userCreateDto);
            if (await _unitOfWork.Users.GetUserByEmailAsync(user.Email) == null)
            {
                user.Password = await _passwordHasher.HashPassword(user.Password);
                var token = _tokenManager.GenerateRefreshToken();
                user.RefreshToken = token.Token;
                user.Expiration = token.Expiration;
                await _unitOfWork.Users.AddAsync(user);
                await _unitOfWork.SaveChangesAsync();
                return;
            }
            throw new ArgumentException("Пользователь с таким email уже существует");
        }
    }
}
