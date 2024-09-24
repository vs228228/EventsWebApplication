using AutoMapper;
using EventsWebApplication.Server.Application.DTOs;
using EventsWebApplication.Server.Application.Interfaces.IUserUseCases;
using EventsWebApplication.Server.Domain.Entities;
using EventsWebApplication.Server.Domain.Interfaces;
using FluentValidation;

namespace EventsWebApplication.Server.Application.UseCases.UserUseCases
{
    public class AddNotificationUseCase : IAddNotificationUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddNotificationUseCase(IUnitOfWork unitOfWork, IMapper mapper) { 
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task ExecuteAsync(NotificationCreateDto notificationDto)
        {
            if(notificationDto.CreatedAt == null || notificationDto.Message == null || notificationDto.UserId == null) throw new ValidationException("Необходимо заполнить все поля");
            var notification = _mapper.Map<Notification>(notificationDto);
            await _unitOfWork.Notifications.AddAsync(notification);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
