using EventsWebApplication.Server.Application.DTOs;
using FluentValidation;

namespace EventsWebApplication.Server.Application.Validators
{
    public class EventUpdateDtoValidator : AbstractValidator<EventUpdateDto>
    {
        public EventUpdateDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id не должно быть пустым");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Название мероприятия не может быть пустым")
                .MaximumLength(100).WithMessage("Название не может быть больше 100 символов");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Описание не может быть пустым")
                .MinimumLength(50).WithMessage("Описание не может быть меньше 100 символов")
                .MaximumLength(2500).WithMessage("Описание не может быть больше 2500 символов"); // это примерно 420 слов и одна страница, так что этого должно хватить

            RuleFor(x => x.DateAndTime)
                .NotEmpty().WithMessage("Дата не может быть пустой")
                .GreaterThan(DateTime.Now.AddDays(1)).WithMessage("Мероприятие можно добавлять не позднее, чем за день начала");

            RuleFor(x => x.Place)
                .NotEmpty().WithMessage("Место проведения должно быть заполнено");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Тип мероприятия не должен быть пустым");

            RuleFor(x => x.MaxPacticipants)
                .NotEmpty().WithMessage("Количество участников не должно быть пустым")
                .GreaterThan(1).WithMessage("Количество участников должно быть больше одного");
        }
    }
}
