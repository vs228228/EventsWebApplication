using AutoMapper;
using EventsWebApplication.Application.DTOs.UserDTOs;
using EventsWebApplication.Application.Pagination;
using EventsWebApplication.Domain.Entities;

namespace EventsWebApplication.Infrastructure.MappingProfile
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserResponseDto, User>();
            CreateMap<User, UserResponseDto>();
            CreateMap<UserCreateRequestDto, User>();
            CreateMap<UserUpdateRequestDto, User>();
        }
    }
}
