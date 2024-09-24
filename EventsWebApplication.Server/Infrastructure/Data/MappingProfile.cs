using AutoMapper;
using EventsWebApplication.Server.Application.DTOs;
using EventsWebApplication.Server.Domain.Entities;
using EventsWebApplication.Server.Domain.Pagination;

namespace EventsWebApplication.Server.Infrastructure.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        { 
            CreateMap<Event, EventDto>();
            CreateMap<EventDto, Event>();
            CreateMap<PagedResult<Event>, PagedResult<EventDto>>();
            CreateMap<EventUpdateDto, Event>();
            CreateMap<EventCreateDto, Event>();
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<PagedResult<User>, PagedResult<UserDto>>();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<DateOnlyDto, DateOnly>();
            CreateMap<DateOnly, DateOnlyDto>();
            CreateMap<Notification, NotificationDto>();
            CreateMap<NotificationDto, Notification>();
            CreateMap<NotificationCreateDto, Notification>();
            CreateMap<PagedResult<Notification>, PagedResult<NotificationDto>>();
        }
    }
}
