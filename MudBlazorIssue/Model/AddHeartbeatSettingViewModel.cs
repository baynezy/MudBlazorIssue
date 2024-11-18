namespace MudBlazorIssue.Model;

public class AddHeartbeatSettingViewModel
{
    public Guid? ProfileId { get; set; }
    public bool IsEnabled { get; set; }
    public FrequencyViewModel Frequency { get; set; } = new();
    public PesterViewModel Pester { get; set; } = new();
}

public class FrequencyViewModel
{
    public int Value { get; set; }
    public int TimeUnit { get; set; }
}

public class PesterViewModel
{
    public List<FrequencyViewModel> Intervals { get; set; } = [];
}