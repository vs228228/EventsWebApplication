using EventsWebApplication.Domain.Entities;
namespace EventsWebApplication.Application.DTOs.UserDTOs
{
    public class UserUpdateRequestDto : UserDtoBase
    {
        public int Id { get; set; }
    }
}
