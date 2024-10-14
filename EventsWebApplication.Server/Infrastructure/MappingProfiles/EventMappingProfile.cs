using AutoMapper;
using EventsWebApplication.Server.Application.DTOs.EventDTOs;
using EventsWebApplication.Server.Application.DTOs.EventDTOs.Responses;
using EventsWebApplication.Server.Application.Pagination;
using EventsWebApplication.Server.Domain.Entities;

public class EventMappingProfile : Profile
{
    public EventMappingProfile()
    {
        CreateMap<Event, EventResponseDto>();
        CreateMap<EventResponseDto, Event>();
        CreateMap<EventUpdateRequestDto, Event>();
        CreateMap<EventCreateRequestDto, Event>();
        CreateMap<PagedResult<Event>, PagedResult<EventResponseDto>>();
    }
}
