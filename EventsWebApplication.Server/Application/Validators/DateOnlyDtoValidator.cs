using EventsWebApplication.Server.Application.DTOs;
using FluentValidation;

namespace EventsWebApplication.Server.Application.Validators
{
    public class DateOnlyDtoValidator : AbstractValidator<DateOnlyDto>
    {
        public DateOnlyDtoValidator()
        {
            RuleFor(x => x.Year)
                .InclusiveBetween(1, 9999)
                .WithMessage("Год должен быть от 1 до 9999");

            RuleFor(x => x.Month)
                .InclusiveBetween(1, 12)
                .WithMessage("Месяц должен быть от 1 до 12");

            RuleFor(x => x.Day)
                .InclusiveBetween(1, 31)
                .WithMessage("Число должно быть от 1 до 31");

            RuleFor(x => x)
                .Must(dto => IsValidDate(dto.Year, dto.Month, dto.Day))
                .WithMessage("Неверно указана дата");
        }

        private bool IsValidDate(int year, int month, int day)
        {
            if (year < 1 || year > 9999 || month < 1 || month > 12 || day < 1 || day > 31)
                return false;

            try
            {
                return day <= DateTime.DaysInMonth(year, month);
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }
        }
    }
}
