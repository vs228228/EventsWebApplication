using AutoMapper;
using EventsWebApplication.Application.DTOs.EventDTOs;
using EventsWebApplication.Application.Pagination;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.MappingProfile
{
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
}