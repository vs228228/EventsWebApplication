using AutoMapper;
using EventsWebApplication.Server.Application.DTOs;
using EventsWebApplication.Server.Application.Pagination;
using EventsWebApplication.Server.Domain.Entities;

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
