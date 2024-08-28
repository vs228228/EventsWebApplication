using AutoMapper;
using EventsWebApplication.Server.Application.DTOs;
using EventsWebApplication.Server.Domain.Entities;

namespace EventsWebApplication.Server.Infrastructure.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        { 
            CreateMap<Event, EventDto>();
            CreateMap<EventDto, Event>();
            CreateMap<EventCreateDto, Event>();
            CreateMap<EventUpdateDto, Event>();
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<DateOnlyDto, DateOnly>();
            CreateMap<DateOnly, DateOnlyDto>();
            CreateMap<Notification, NotificationDto>();
            CreateMap<NotificationDto, Notification>();
        }
    }
}
