using EventsWebApplication.Application.DTOs;
using FluentValidation;

namespace EventsWebApplication.Infrastructure.Validators
{
    public class AddNotificationValidator : AbstractValidator<NotificationCreateDto>
    {
        public AddNotificationValidator()
        {
            RuleFor(x => x.CreatedAt).NotEmpty().WithMessage("Дата не должна быть пустой");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId не должен быть пустым");
            RuleFor(x => x.Message).NotEmpty().WithMessage("Сообщение не должно быть пустым");
        }
    }
}

