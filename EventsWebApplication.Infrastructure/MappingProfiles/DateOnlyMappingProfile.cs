using AutoMapper;
using EventsWebApplication.Application.DTOs;

namespace EventsWebApplication.Infrastructure.MappingProfile
{
    public class DateOnlyMappingProfile : Profile
    {
        public DateOnlyMappingProfile()
        {
            CreateMap<DateOnlyDto, DateOnly>();
            CreateMap<DateOnly, DateOnlyDto>();
        }
    }
}
