namespace EventsWebApplication.Application.DTOs.UserDTOs
{
    public class UserCreateRequestDto : UserDtoBase
    {
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
