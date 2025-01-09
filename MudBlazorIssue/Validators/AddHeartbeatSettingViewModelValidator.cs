using FluentValidation;
using MudBlazorIssue.Model;

namespace MudBlazorIssue.Validators;

public class AddHeartbeatSettingViewModelValidator : BlazorFormValidator<AddHeartbeatSettingViewModel>
{
    public AddHeartbeatSettingViewModelValidator(IValidator<FrequencyViewModel> frequencyViewModelValidator)
    {
        When(x => x.IsEnabled, () =>
        {
            RuleFor(x => x.Frequency)
                .SetValidator(frequencyViewModelValidator);
            
            RuleForEach(x => x.Pester.Intervals)
                .SetValidator(frequencyViewModelValidator);
        });
    }
}