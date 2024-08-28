﻿using EventsWebApplication.Server.Domain.Entities;

namespace EventsWebApplication.Server.Application.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateOnly Birthday { get; set; }
        public bool IsAdmin { get; set; }
        public string NotificationString { get; set; } = "";
        public ICollection<EventParticipant> Events { get; set; }
    }
}
