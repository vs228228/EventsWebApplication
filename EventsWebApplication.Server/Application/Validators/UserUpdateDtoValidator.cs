using EventsWebApplication.Server.Application.DTOs;
using FluentValidation;

namespace EventsWebApplication.Server.Application.Validators
{
    public class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
    {
        public UserUpdateDtoValidator()
        {
            RuleFor(x => x.Birthday)
                .SetValidator(new DateOnlyDtoValidator());
        }
    }
}
