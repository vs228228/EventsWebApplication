using EventsWebApplication.Application.DTOs.UserDTOs;
using EventsWebApplication.Application.Interfaces.IUserUseCases;
using EventsWebApplication.Application.Services;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Interfaces;

namespace EventsWebApplication.Application.UseCases.UserUseCases
{
    public class GetUserByIdUseCase : IGetUserByIdUseCase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperService  _mapper;

        public GetUserByIdUseCase(IUnitOfWork unitOfWork, IMapperService mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserResponseDto> ExecuteAsync(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null) throw new KeyNotFoundException();
            return await _mapper.Map<User, UserResponseDto>(user);
        }
    }
}
