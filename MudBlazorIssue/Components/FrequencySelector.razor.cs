using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace MudBlazorIssue.Components;

public partial class FrequencySelector : ComponentBase
{
    /// <summary>
    /// The frequency
    /// <remarks>REQUIRED</remarks>
    /// </summary>
    [Parameter, EditorRequired] public int Frequency { get; set; }
    /// <summary>
    /// The frequency changed event
    /// </summary>
    [Parameter] public EventCallback<int> FrequencyChanged { get; set; }
    /// <summary>
    /// The time unit
    /// <remarks>REQUIRED</remarks>
    /// </summary>
    [Parameter, EditorRequired] public int TimeUnit { get; set; }
    /// <summary>
    /// The time unit changed event
    /// </summary>
    [Parameter] public EventCallback<int> TimeUnitChanged { get; set; }
    /// <summary>
    /// The disabled state for the component
    /// </summary>
    [Parameter] public bool Disabled { get; set; }
    /// <summary>
    /// The model field representing validation results for the frequency property
    /// </summary>
    [Parameter] public Expression<Func<int>>? ForFrequency { get; set; }
    /// <summary>
    /// The model field representing validation results for the time unit property
    /// </summary>
    [Parameter] public Expression<Func<int>>? ForTimeUnit { get; set; }
    
    private string _frequencyLabel = Model.TimeUnit.FromValue(0).ToString();
    private const int MinFrequency = 0;

    private async Task UpdateFrequency(int value)
    {
        Frequency = value;
        await FrequencyChanged.InvokeAsync(Frequency);
    }

    private async Task UpdateTimeUnit(int value)
    {
        if (!Model.TimeUnit.TryFromValue(value, out var timeUnit)) return;

        TimeUnit = timeUnit;
        _frequencyLabel = timeUnit.ToString();
        await TimeUnitChanged.InvokeAsync(TimeUnit);
    }
}