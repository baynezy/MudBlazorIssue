using FluentValidation;
using MudBlazorIssue.Model;

namespace MudBlazorIssue.Validators;

public class FrequencyViewModelValidator : AbstractValidator<FrequencyViewModel>
{
    public FrequencyViewModelValidator()
    {
        RuleFor(x => x.Value)
            .GreaterThan(0)
            .WithMessage("Value must be greater than 0.");

        RuleFor(x => x.TimeUnit)
            .Must(BeValidTimeUnit)
            .WithMessage("Time unit is invalid.");
    }

    private static bool BeValidTimeUnit(int input) => TimeUnit.TryFromValue(input, out _);
}