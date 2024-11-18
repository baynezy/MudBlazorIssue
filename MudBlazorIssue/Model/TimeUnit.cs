using Ardalis.SmartEnum;

namespace MudBlazorIssue.Model;

public class TimeUnit : SmartEnum<TimeUnit>
{
    public static readonly TimeUnit Days = new TimeUnit(nameof (Days), 0);
    public static readonly TimeUnit Weeks = new TimeUnit(nameof (Weeks), 1);

    private TimeUnit(string name, int value) : base(name, value)
    {
    }
}