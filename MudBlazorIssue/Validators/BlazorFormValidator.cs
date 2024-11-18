using FluentValidation;

namespace MudBlazorIssue.Validators;

public abstract class BlazorFormValidator<TModel> : AbstractValidator<TModel>
{
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<TModel>.CreateWithOptions((TModel)model, x => x.IncludeProperties(propertyName)));
        return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
    };
}