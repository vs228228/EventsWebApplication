using EventsWebApplication.Application.DTOs.UserDTOs;
using FluentValidation;

namespace EventsWebApplication.Infrastructure.Validators
{
    public class UserCreateDtoValidator : AbstractValidator<UserCreateRequestDto>
    {
        public UserCreateDtoValidator()
        {
            RuleFor(x => x.Birthday)
                .SetValidator(new DateOnlyDtoValidator());

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Поле имя не заполнено")
                .MaximumLength(50).WithMessage("Имя не может быть больше 50 символов");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Поле фамилия не заполнено")
                .MaximumLength(50).WithMessage("Фамилия не может быть больше 50 символов");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Поле email не заполнено")
                .EmailAddress().WithMessage("Неверный email");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Поле пароль не заполнено")
                .MinimumLength(8).WithMessage("Пароль должен быть не  меньше 8 символов")
                .Matches(@"[A-Z]").WithMessage("Пароль должен содержать хотя бы одну заглавную букву")
                .Matches(@"[0-9]").WithMessage("Пароль должен содержать хотя бы одну цифру");
        }
    }


}
