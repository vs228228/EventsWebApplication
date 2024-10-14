using AutoMapper;
using EventsWebApplication.Server.Application.DTOs.UserDTOs;
using EventsWebApplication.Server.Application.Pagination;
using EventsWebApplication.Server.Domain.Entities;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<UserResponseDto, User>();
        CreateMap<User, UserResponseDto>();
        CreateMap<IEnumerable<User>, IEnumerable<UserResponseDto>>();
        CreateMap<UserCreateResponseDto, User>();
        CreateMap<UserUpdateRequestDto, User>();
    }
}
