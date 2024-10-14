using AutoMapper;
using EventsWebApplication.Server.Application.DTOs;

public class DateOnlyMappingProfile : Profile
{
    public DateOnlyMappingProfile()
    {
        CreateMap<DateOnlyDto, DateOnly>();
        CreateMap<DateOnly, DateOnlyDto>();
    }
}
