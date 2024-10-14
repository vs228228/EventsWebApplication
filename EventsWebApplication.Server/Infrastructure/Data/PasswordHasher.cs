using EventsWebApplication.Server.Application.Interfaces;
using EventsWebApplication.Server.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace EventsWebApplication.Server.Infrastructure.Data
{
    public class PasswordHasher : IPasswordHasher
    {
        PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();
        public async Task<string> HashPassword(string password)
        {
           password =  _passwordHasher.HashPassword(null, password);
            return password;
        }

        public async Task<bool> VerifyPassword(string hashedPassword, string providedPassword)
        {
            var info = _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
            if (info != PasswordVerificationResult.Failed)
            {
                return true;
            }
            return false;
        }
    }
}
