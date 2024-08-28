using EventsWebApplication.Server.Domain.Entities;

namespace EventsWebApplication.Server.Application.DTOs
{
    public class UserCreateDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateOnlyDto Birthday { get; set; }
        public bool IsAdmin { get; set; }
    }
}
