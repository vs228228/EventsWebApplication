﻿namespace EventsWebApplication.Application.DTOs
{
    public class GetTokenDto
    {
        public string RefreshToken { get; set; }
        public int userId { get; set; }
    }
}
