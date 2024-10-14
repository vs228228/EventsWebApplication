using EventsWebApplication.Application.DTOs.UserDTOs;
using EventsWebApplication.Application.Interfaces.IUserUseCases;
using EventsWebApplication.Application.Services;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Interfaces;

namespace EventsWebApplication.Application.UseCases.UserUseCases
{
    public class GetUserByEmailUseCase : IGetUserByEmailUseCase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperService _mapper;

        public GetUserByEmailUseCase(IUnitOfWork unitOfWork, IMapperService mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserResponseDto> ExecuteAsync(string email)
        {
            var user = await _unitOfWork.Users.GetUserByEmailAsync(email);
            if (user == null) throw new KeyNotFoundException();
            return await _mapper.Map<User,UserResponseDto>(user);
        }
    }
}
