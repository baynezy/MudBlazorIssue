using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace MudBlazorIssue.Components;

public class FluentValidationValidator : ComponentBase
{
    [CascadingParameter] private EditContext? EditContext { get; set; }

    [Parameter] public Type? ValidatorType { get; set; }

    private IValidator Validator { get; set; }
    private ValidationMessageStore ValidationMessageStore;

    [Inject] private IServiceProvider ServiceProvider { get; set; }

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        var previousEditContext = EditContext;
        var previousValidatorType = ValidatorType;

        await base.SetParametersAsync(parameters);

        if (EditContext is null)
        {
            throw new InvalidOperationException(
                $"{nameof(FluentValidationValidator)} must be placed within an {nameof(EditForm)}");
        }

        if (ValidatorType is null)
        {
            throw new InvalidOperationException($"{nameof(ValidatorType)} must be specified");
        }

        if (!typeof(IValidator).IsAssignableFrom(ValidatorType))
        {
            throw new InvalidOperationException($"{ValidatorType.Name} must implement {typeof(IValidator).FullName}");
        }

        if (ValidatorType != previousValidatorType)
        {
            ValidatorTypeChanged();
        }

        if (EditContext != previousEditContext)
        {
            EditContextChanged();
        }
    }

    private void ValidatorTypeChanged()
    {
        var validator = ServiceProvider.GetService(ValidatorType!);
        Validator = (IValidator) validator;
    }

    private void EditContextChanged()
    {
        ValidationMessageStore = new ValidationMessageStore(EditContext!);
        HookUpEditContextEvents();
    }

    private void HookUpEditContextEvents()
    {
        EditContext!.OnValidationRequested += ValidationRequested;
        EditContext!.OnFieldChanged += FieldChanged;
    }

    [SuppressMessage("ReSharper", "AsyncVoidMethod")]
    private async void ValidationRequested(object? sender, ValidationRequestedEventArgs e)
    {
        ValidationMessageStore.Clear();
        var validationContext = new ValidationContext<object>(EditContext!.Model);
        var result = await Validator.ValidateAsync(validationContext);
        AddValidationResult(EditContext!.Model, result);
    }

    private void AddValidationResult(object model, ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            var fieldIdentifier = new FieldIdentifier(model, error.PropertyName);
            ValidationMessageStore.Add(fieldIdentifier, error.ErrorMessage);
        }

        EditContext!.NotifyValidationStateChanged();
    }

    [SuppressMessage("ReSharper", "AsyncVoidMethod")]
    private async void FieldChanged(object? sender, FieldChangedEventArgs args)
    {
        var fieldIdentifier = args.FieldIdentifier;
        ValidationMessageStore.Clear(fieldIdentifier);

        var propertiesToValidation = new[] {fieldIdentifier.FieldName};
        var validationContext = new ValidationContext<object>(
            EditContext!.Model, 
            new PropertyChain(),
            new MemberNameValidatorSelector(propertiesToValidation));

        var result = await Validator.ValidateAsync(validationContext);

        AddValidationResult(fieldIdentifier.Model, result);
    }
}