namespace EventsWebApplication.Server.Application.DTOs.UserDTOs
{
    public class UserCreateResponseDto : UserDtoBase
    {
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
