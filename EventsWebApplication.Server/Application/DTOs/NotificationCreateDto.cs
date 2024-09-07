namespace EventsWebApplication.Server.Application.DTOs
{
    public class NotificationCreateDto
    {
        public int UserId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
