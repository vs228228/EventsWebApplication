using EventsWebApplication.Server.Application.DTOs;
using FluentValidation;

namespace EventsWebApplication.Server.Application.Validators
{
    public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
    {
        public UserCreateDtoValidator()
        {
            RuleFor(x => x.Birthday)
                .SetValidator(new DateOnlyDtoValidator());
        }
    }
    
    
}
