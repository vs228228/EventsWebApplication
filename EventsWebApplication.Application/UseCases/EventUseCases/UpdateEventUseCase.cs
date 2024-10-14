using EventsWebApplication.Application.DTOs.EventDTOs;
using EventsWebApplication.Application.Interfaces;
using EventsWebApplication.Application.Interfaces.IEventUseCases;
using EventsWebApplication.Application.Services;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace EventsWebApplication.Application.UseCases.EventUseCases
{
    public class UpdateEventUseCase : IUpdateEventUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        private readonly IMapperService _mapper;

        public UpdateEventUseCase(IUnitOfWork unitOfWork, IFileService fileService, IMapperService mapper)
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
            oldEvent = await _mapper.Update<EventUpdateRequestDto, Event>(eventObject, oldEvent);
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
