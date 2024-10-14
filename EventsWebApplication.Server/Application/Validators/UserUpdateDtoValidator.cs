using EventsWebApplication.Server.Application.DTOs.UserDTOs;
using FluentValidation;

namespace EventsWebApplication.Server.Application.Validators
{
    public class UserUpdateDtoValidator : AbstractValidator<UserUpdateRequestDto>
    {
        public UserUpdateDtoValidator()
        {
            RuleFor(x => x.Birthday)
                .SetValidator(new DateOnlyDtoValidator());

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Имя не заполнено")
                .MaximumLength(50).WithMessage("Имя не может быть больше 50 символов");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Фамилия не заполнена")
                .MaximumLength(50).WithMessage("Фамилия не может быть больше 50 символов");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email не заполнен")
                .EmailAddress().WithMessage("Неверный email");
        }
    }
}
