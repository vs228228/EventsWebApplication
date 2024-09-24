using AutoMapper;
using EventsWebApplication.Server.Application.DTOs;
using EventsWebApplication.Server.Application.Interfaces.IUserUseCases;
using EventsWebApplication.Server.Domain.Interfaces;

namespace EventsWebApplication.Server.Application.UseCases.UserUseCases
{
    public class GetUserByIdUseCase : IGetUserByIdUseCase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserDto> ExecuteAsync(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if(user == null) throw new KeyNotFoundException();
            return _mapper.Map<UserDto>(user);
        }
    }
}
