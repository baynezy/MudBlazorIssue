using FluentValidation.Results;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazorIssue.Model;
using MudBlazorIssue.Validators;

namespace MudBlazorIssue.Pages;

public partial class Heartbeat : ComponentBase
{
    [Inject] private AddHeartbeatSettingViewModelValidator AddHeartbeatSettingViewModelValidator { get; set; } = null!;
    private const Color SubmitButtonColor = Color.Primary;
    private const Color AddPesterButtonColor = Color.Secondary;
    private MudForm _form = null!;
    private string[] _errors = [];
    private List<ValidationFailure> _generalErrors = [];
    private string _addPesterButtonLabel = "Add Interval";
    private string _submitButtonLabel = "Create Heartbeat Setting";
    protected readonly AddHeartbeatSettingViewModel AddHeartbeatSettingViewModel = new();
    private bool IsSaving;
    
    private bool HasGeneralErrors() => _generalErrors.Count > 0;
    
    private void HandleAddPester()
    {
        AddHeartbeatSettingViewModel.Pester.Intervals.Add(new FrequencyViewModel());
    }

    private async Task HandleSubmit()
    {
        await _form.Validate();
        var validationResult = await AddHeartbeatSettingViewModelValidator.ValidateAsync(AddHeartbeatSettingViewModel);
        _generalErrors = validationResult.Errors.FindAll(error => error.PropertyName == string.Empty);
    }
}