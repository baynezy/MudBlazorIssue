using Microsoft.AspNetCore.Components;
using MudBlazorIssue.Model;

namespace MudBlazorIssue.Pages;

public partial class AltHearbeat : ComponentBase
{
    private readonly AddHeartbeatSettingViewModel AddHeartbeatSettingViewModel = new();
    private string _frequencyLabel = TimeUnit.FromValue(0).ToString();

    private void TimeUnitChanged()
    {
        if (TimeUnit.TryFromValue(AddHeartbeatSettingViewModel.Frequency.TimeUnit, out var timeUnit))
        {
            _frequencyLabel = timeUnit.ToString();
            Console.WriteLine("bob");
        }
    }

    private void FormSubmitted()
    {
        Console.WriteLine("Form submitted");
    }
    
    private void FormInvalid()
    {
        Console.WriteLine("Form invalid");
    }
}