using EventsWebApplication.Application.DTOs.EventDTOs;
using FluentValidation;

namespace EventsWebApplication.Infrastructure.Validators
{
    public class EventUpdateDtoValidator : AbstractValidator<EventUpdateRequestDto>
    {
        public EventUpdateDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id не должно быть пустым");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Название мероприятия не может быть пустым")
                .MaximumLength(100).WithMessage("Название не может быть больше 100 символов");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Описание не может быть пустым")
                .MaximumLength(2500).WithMessage("Описание не может быть больше 2500 символов"); // это примерно 420 слов и одна страница, так что этого должно хватить

            RuleFor(x => x.DateAndTime)
                .NotEmpty().WithMessage("Дата не может быть пустой")
                .GreaterThan(DateTime.Now.AddDays(1)).WithMessage("Мероприятие можно добавлять не позднее, чем за день начала");

            RuleFor(x => x.Place)
                .NotEmpty().WithMessage("Место проведения должно быть заполнено");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Тип мероприятия не должен быть пустым");

            RuleFor(x => x.MaxParticipants)
                .NotEmpty().WithMessage("Количество участников не должно быть пустым")
                .GreaterThan(1).WithMessage("Количество участников должно быть больше одного");
        }
    }
}
