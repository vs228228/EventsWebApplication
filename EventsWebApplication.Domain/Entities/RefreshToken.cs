﻿namespace EventsWebApplication.Domain.Entities
{
    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
