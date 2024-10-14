using EventsWebApplication.Server.Domain.Entities;
namespace EventsWebApplication.Server.Application.DTOs.UserDTOs
{
    public class UserUpdateRequestDto : UserDtoBase
    {
        public int Id { get; set; }
    }
}
