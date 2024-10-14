using EventsWebApplication.Domain.Entities;
namespace EventsWebApplication.Application.DTOs.UserDTOs
{
    public class UserResponseDto : UserDtoBase
    {
        public int Id { get; set; }
        public bool IsAdmin { get; set; }
    }
}
