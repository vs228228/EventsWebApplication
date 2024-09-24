using AutoMapper;
using EventsWebApplication.Server.Application.DTOs;
using EventsWebApplication.Server.Application.Interfaces.IUserUseCases;
using EventsWebApplication.Server.Application.Validators;
using EventsWebApplication.Server.Domain.Interfaces;
using FluentValidation;

namespace EventsWebApplication.Server.Application.UseCases.UserUseCases
{
    public class UpdateUserUseCase : IUpdateUserUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserUpdateDtoValidator _validator;

        public UpdateUserUseCase(IUnitOfWork unitOfWork, IMapper mapper, UserUpdateDtoValidator validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task ExecuteAsync(UserUpdateDto userUpdateDto)
        {
            var validationResult = _validator.Validate(userUpdateDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var entity = await _unitOfWork.Users.GetByIdAsync(userUpdateDto.Id);
            var user = _mapper.Map(userUpdateDto, entity);
            await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
