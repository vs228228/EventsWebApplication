using EventsWebApplication.Application.DTOs.UserDTOs;
using EventsWebApplication.Application.Interfaces.IEventUseCases;
using EventsWebApplication.Domain.Interfaces;
using System.Data.SqlTypes;

namespace EventsWebApplication.Application.UseCases.EventUseCases
{
    public class RegisterUserForEventUseCase : IRegisterUserForEventUseCase
    {

        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserForEventUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(UserEventIdDto userEventInfo)
        {
            var currentEvent = await _unitOfWork.Events.GetByIdAsync(userEventInfo.EventId);
            if (currentEvent == null) throw new ArgumentException("Мероприятие не найдено");
            var currentUser = await _unitOfWork.Users.GetByIdAsync(userEventInfo.UserId);
            if (currentUser == null) throw new ArgumentException("Пользователь не найден");
            if (currentEvent.CountOfParticipants >= currentEvent.MaxParticipants) throw new ArgumentException("Максимальное кол-во участников");
            await _unitOfWork.Events.RegisterUserForEventAsync(userEventInfo.UserId, userEventInfo.EventId);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
