using AutoMapper;
using EventsWebApplication.Server.Application.DTOs.EventDTOs;
using EventsWebApplication.Server.Application.Interfaces;
using EventsWebApplication.Server.Application.Interfaces.IEventUseCases;
using EventsWebApplication.Server.Application.Validators;
using EventsWebApplication.Server.Domain.Entities;
using EventsWebApplication.Server.Domain.Interfaces;

namespace EventsWebApplication.Server.Application.UseCases.EventUseCases
{
    public class UpdateEventUseCase : IUpdateEventUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public UpdateEventUseCase(IUnitOfWork unitOfWork, IFileService fileService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(EventUpdateRequestDto eventObject, IFormFile photo)
        {
            

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
