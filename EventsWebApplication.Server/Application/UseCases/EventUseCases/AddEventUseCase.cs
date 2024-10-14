using AutoMapper;
using EventsWebApplication.Server.Application.DTOs.EventDTOs;
using EventsWebApplication.Server.Application.Interfaces;
using EventsWebApplication.Server.Application.Interfaces.IEventUseCases;
using EventsWebApplication.Server.Application.Validators;
using EventsWebApplication.Server.Domain.Entities;
using EventsWebApplication.Server.Domain.Interfaces;

namespace EventsWebApplication.Server.Application.UseCases.EventUseCases
{
    public class AddEventUseCase : IAddEventUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public AddEventUseCase(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileSerivce)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileSerivce;
        }

        public async Task ExecuteAsync(EventCreateRequestDto eventObject, IFormFile photo)
        {
            
            Event eventEntity = _mapper.Map<Event>(eventObject);
            var photoPath = await _fileService.SaveFileAsync(photo);
            eventEntity.ImagePath = photoPath;
            await _unitOfWork.Events.AddAsync(eventEntity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
