using AutoMapper;
using EventsWebApplication.Server.Application.DTOs;
using EventsWebApplication.Server.Application.Interfaces;
using EventsWebApplication.Server.Application.Pagination;
using EventsWebApplication.Server.Domain.Entities;
using EventsWebApplication.Server.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventsWebApplication.Server.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        private readonly ITokenManager _tokenManager;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher passwordHasher, ITokenManager tokenManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _tokenManager = tokenManager;
        }
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _unitOfWork.Users.GetAllUsersAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<IEnumerable<EventDto>> GetRegisteredEventsAsync(int userId)
        {
            var events = await _unitOfWork.Users.GetRegisteredEventsAsync(userId);
            return _mapper.Map<IEnumerable<EventDto>>(events);
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var user = await _unitOfWork.Users.GetUserByEmailAsync(email);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _unitOfWork.Users.GetUserByIdAsync(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<PagedResult<UserDto>> GetUsersAsync(int pageNumber, int pageSize)
        {
            var users = await _unitOfWork.Users.GetUsersAsync(pageNumber, pageSize);
            return _mapper.Map<PagedResult<UserDto>>(users);
        }

        public async Task<string> TryAuthenticateAsync(UserAuthDto loginDto) // тут нужно будет с токенами поработать
        {
            User user = await _unitOfWork.Users.GetUserByEmailAsync(loginDto.UserEmail);
            if(user == null)
            {
                return "Wrong email";
            }
            if(!await _passwordHasher.VerifyPassword(user.Password, loginDto.UserPassword)) {
                return "Wrong password"; 
            }
           var token =  _tokenManager.GenerateRefreshToken();
            user.RefreshToken = token.Token;
            user.Expiration = token.Expiration;
           await  _unitOfWork.SaveChangesAsync();
           return token.Token;
            
        //    throw new NotImplementedException();
        }

        public async Task<string> TryAddUserAsync(UserCreateDto userCreateDto)
        {
            var user = _mapper.Map<User>(userCreateDto);
            if(await _unitOfWork.Users.GetUserByEmailAsync(user.Email) == null)
            {
                user.Password = await _passwordHasher.HashPassword(user.Password);
                await _unitOfWork.Users.AddUserAsync(user);
                await _unitOfWork.SaveChangesAsync();
                return "OK";
            }
            return "NOT OK";
        }

        public async Task UpdateUserAsync(UserUpdateDto userUpdateDto)
        {
            /*var user = _mapper.Map<User>(userUpdateDto);
            user.Password = oldUser.Password;*/
            var entity = await _unitOfWork.Users.GetUserByIdAsync(userUpdateDto.Id);
            var user = _mapper.Map(userUpdateDto, entity);
            await _unitOfWork.Users.UpdateUserAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var events = await _unitOfWork.Users.GetRegisteredEventsAsync(id);
            foreach (var item in events)
            {
                await _unitOfWork.Events.UnregisterUserFromEventAsync(id, item.Id);
            }
            await _unitOfWork.Users.DeleteUserAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PagedResult<NotificationDto>> GetNotificationsAsync(int userId, int pageNumber, int pageSize)
        {
            var notifications = await _unitOfWork.Notifications.GetNotificationsAsync(userId, pageNumber, pageSize);
            return _mapper.Map<PagedResult<NotificationDto>>(notifications);
        }

        public async Task AddNotificationAsync(NotificationDto notificationDto)
        {
            var notification = _mapper.Map<Notification>(notificationDto);
            await _unitOfWork.Notifications.AddNotificationAsync(notification);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteNotificationAsync(int notificationId)
        {
            await _unitOfWork.Notifications.DeleteNotificationAsync(notificationId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<TokenDto> GenerateAccessToken(GetTokenDto getTokenDto)
        {
            var user = await _unitOfWork.Users.GetUserByIdAsync(getTokenDto.userId);
            if (user == null || user.RefreshToken != getTokenDto.RefreshToken || user.Expiration < DateTime.Now)
            {
                return null;
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
