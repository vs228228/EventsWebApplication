using EventsWebApplication.Application.DTOs.EventDTOs;
using EventsWebApplication.Application.Interfaces;
using EventsWebApplication.Application.Interfaces.IEventUseCases;
using EventsWebApplication.Application.Services;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace EventsWebApplication.Application.UseCases.EventUseCases
{
    public class AddEventUseCase : IAddEventUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperService _mapper;
        private readonly IFileService _fileService;

        public AddEventUseCase(IUnitOfWork unitOfWork, IMapperService mapper, IFileService fileSerivce)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileSerivce;
        }

        public async Task ExecuteAsync(EventCreateRequestDto eventObject, IFormFile photo)
        {

            Event eventEntity = await _mapper.Map<EventCreateRequestDto, Event>(eventObject);
            var photoPath = await _fileService.SaveFileAsync(photo);
            eventEntity.ImagePath = photoPath;
            await _unitOfWork.Events.AddAsync(eventEntity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
