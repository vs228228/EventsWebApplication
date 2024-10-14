using EventsWebApplication.Application.DTOs.UserDTOs;
using EventsWebApplication.Application.Interfaces.IUserUseCases;
using EventsWebApplication.Application.Services;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Interfaces;

namespace EventsWebApplication.Application.UseCases.UserUseCases
{
    public class UpdateUserUseCase : IUpdateUserUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperService _mapper;

        public UpdateUserUseCase(IUnitOfWork unitOfWork, IMapperService mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(UserUpdateRequestDto userUpdateDto)
        {

            var entity = await _unitOfWork.Users.GetByIdAsync(userUpdateDto.Id);
            var user = await _mapper.Update<UserUpdateRequestDto, User>(userUpdateDto, entity);
            await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
