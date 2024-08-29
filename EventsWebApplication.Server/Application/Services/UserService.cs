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

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
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
            return "Yes yes yes"; // а тут выдавать токен
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
    }
}
