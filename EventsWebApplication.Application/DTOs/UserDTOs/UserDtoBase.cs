using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Application.DTOs.UserDTOs
{
    public class UserDtoBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateOnlyDto Birthday { get; set; }
    }
}
