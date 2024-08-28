using EventsWebApplication.Server.Domain.Entities;

namespace EventsWebApplication.Server.Application.DTOs
{
    public class UserUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateOnlyDto Birthday { get; set; }
    }
}
