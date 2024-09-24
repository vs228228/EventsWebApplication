using AutoMapper;
using EventsWebApplication.Server.Application.DTOs;
using EventsWebApplication.Server.Application.Interfaces;
using EventsWebApplication.Server.Application.Interfaces.IEventUseCases;
using EventsWebApplication.Server.Application.Validators;
using EventsWebApplication.Server.Domain.Entities;
using EventsWebApplication.Server.Domain.Interfaces;
using FluentValidation;

namespace EventsWebApplication.Server.Application.UseCases.EventUseCases
{
    public class AddEventUseCase : IAddEventUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly EventCreateDtoValidator _validator;

        public AddEventUseCase(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileSerivce, EventCreateDtoValidator validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileSerivce;
            _validator = validator;
        }

        public async Task ExecuteAsync(EventCreateDto eventObject, IFormFile photo)
        {
            var validationResult = _validator.Validate(eventObject);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            Event eventEntity = _mapper.Map<Event>(eventObject);
            var photoPath = await _fileService.SaveFileAsync(photo);
            eventEntity.ImagePath = photoPath;
            await _unitOfWork.Events.AddAsync(eventEntity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
