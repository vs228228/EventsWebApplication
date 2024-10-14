using AutoMapper;
using EventsWebApplication.Server.Application.DTOs.UserDTOs;
using EventsWebApplication.Server.Application.Interfaces;
using EventsWebApplication.Server.Application.Interfaces.IUserUseCases;
using EventsWebApplication.Server.Application.Validators;
using EventsWebApplication.Server.Domain.Entities;
using EventsWebApplication.Server.Domain.Interfaces;
using FluentValidation;

namespace EventsWebApplication.Server.Application.UseCases.UserUseCases
{
    public class TryAddUserUseCase : ITryAddUserUseCase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITokenManager _tokenManager;
        private readonly IPasswordHasher _passwordHasher;
        private readonly UserCreateDtoValidator _validator;

        public TryAddUserUseCase(IUnitOfWork unitOfWork, IMapper mapper, ITokenManager tokenManager, IPasswordHasher passwordHasher, UserCreateDtoValidator validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tokenManager = tokenManager;
            _passwordHasher = passwordHasher;
            _validator = validator;
        }

        public async Task ExecuteAsync(UserCreateResponseDto userCreateDto)
        {
            
            var user = _mapper.Map<User>(userCreateDto);
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
