using AutoMapper;
using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Application.Pagination;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.MappingProfile
{
    public class NotificationMappingProfile : Profile
    {
        public NotificationMappingProfile()
        {
            CreateMap<Notification, NotificationDto>();
            CreateMap<NotificationDto, Notification>();
            CreateMap<NotificationCreateDto, Notification>();
            CreateMap<PagedResult<Notification>, PagedResult<NotificationDto>>();
        }
    }
}
