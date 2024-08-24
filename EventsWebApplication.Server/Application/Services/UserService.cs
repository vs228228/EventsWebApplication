using EventsWebApplication.Server.Application.DTOs;
using EventsWebApplication.Server.Application.Interfaces;
using EventsWebApplication.Server.Domain.Entities;
using EventsWebApplication.Server.Domain.Interfaces;

namespace EventsWebApplication.Server.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _unitOfWork.Users.GetAllUsersAsync();
        }

        public async Task<IEnumerable<Event>> GetRegisteredEventsAsync(int userId)
        {
            return await _unitOfWork.Users.GetRegisteredEventsAsync(userId);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _unitOfWork.Users.GetUserByEmailAsync(email);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _unitOfWork.Users.GetUserByIdAsync(id);
        }

        public async Task<string> TryAuthenticateAsync(UserAuthDto loginDto) // тут нужно будет с токенами поработать
        {
            User user = await _unitOfWork.Users.GetUserByEmailAsync(loginDto.UserEmail);
            if(user == null)
            {
                return "Wrong email";
            }
            if(!_passwordHasher.VerifyPassword(user.Password, loginDto.UserPassword)) {
                return "Wrong password"; 
            }
            return ""; // а тут выдавать токен
            throw new NotImplementedException();
        }

        public async Task<string> TryAddUserAsync(User user)
        {
            if(await _unitOfWork.Users.GetUserByEmailAsync(user.Email) == null)
            {
                user.Password = _passwordHasher.HashPassword(user.Password);
                await _unitOfWork.Users.AddUserAsync(user);
                await _unitOfWork.SaveChangesAsync();
                return "OK";
            }
            return "NOT OK";
        }

        public async Task UpdateUserAsync(User user)
        {
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
    }
}
