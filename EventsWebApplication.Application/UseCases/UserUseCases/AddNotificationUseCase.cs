using EventsWebApplication.Application.DTOs;
using EventsWebApplication.Application.Interfaces.IUserUseCases;
using EventsWebApplication.Application.Services;
using EventsWebApplication.Domain.Entities;
using EventsWebApplication.Domain.Interfaces;

namespace EventsWebApplication.Application.UseCases.UserUseCases
{
    public class AddNotificationUseCase : IAddNotificationUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperService _mapper;

        public AddNotificationUseCase(IUnitOfWork unitOfWork, IMapperService mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task ExecuteAsync(NotificationCreateDto notificationDto)
        {
            User user = await _unitOfWork.Users.GetByIdAsync(notificationDto.UserId);
            if (user == null) throw new ArgumentException("Пользователь не найден");
            var notification = await _mapper.Map<NotificationCreateDto,Notification>(notificationDto);
            await _unitOfWork.Notifications.AddAsync(notification);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
