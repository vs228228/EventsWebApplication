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
    public class UpdateEventUseCase : IUpdateEventUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly EventUpdateDtoValidator _validator;

        public UpdateEventUseCase(IUnitOfWork unitOfWork, IFileService fileService, IMapper mapper, EventUpdateDtoValidator validator)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task ExecuteAsync(EventUpdateDto eventObject, IFormFile photo)
        {
            var validationResult = _validator.Validate(eventObject);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            Event oldEvent = await _unitOfWork.Events.GetByIdAsync(eventObject.Id);
            if (oldEvent == null)
            {
                throw new KeyNotFoundException();
            }
            if (photo != null)
            {
                var photoPath = await _fileService.SaveFileAsync(photo
                    );
                oldEvent.ImagePath = photoPath;
            }
            _mapper.Map(eventObject, oldEvent);
            await _unitOfWork.Events.UpdateAsync(oldEvent);
            string message = $"Мероприятие {oldEvent.Title} было изменено.";
            await NotifyUsersOfChange(oldEvent.Id, message);
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task NotifyUsersOfChange(int eventId, string message)
        {
            var users = await _unitOfWork.Events.GetUsersByEventIdAsync(eventId);
            Notification notification = new Notification()
            {
                Message = message,
                CreatedAt = DateTime.Now
            };
            foreach (var user in users)
            {
                notification.User = user;
                notification.UserId = user.Id;
                await _unitOfWork.Notifications.AddAsync(notification);
            }
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
