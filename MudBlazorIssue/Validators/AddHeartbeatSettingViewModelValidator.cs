using FluentValidation;
using MudBlazorIssue.Model;

namespace MudBlazorIssue.Validators;

public class AddHeartbeatSettingViewModelValidator : BlazorFormValidator<AddHeartbeatSettingViewModel>
{
    public AddHeartbeatSettingViewModelValidator(IValidator<FrequencyViewModel> frequencyViewModelValidator)
    {
        RuleFor(x => x.ProfileId)
            .NotNull()
            .WithMessage("ProfileId is required.")
            .Must(BeValidGuid)
            .WithMessage("Invalid ProfileId.");
        
        When(x => x.IsEnabled, () =>
        {
            RuleFor(x => x.Frequency)
                .SetValidator(frequencyViewModelValidator);
            
            RuleForEach(x => x.Pester.Intervals)
                .SetValidator(frequencyViewModelValidator);
        });
        
            
        
    }

    private static bool BeValidGuid(Guid? guid) => guid != Guid.Empty;
}