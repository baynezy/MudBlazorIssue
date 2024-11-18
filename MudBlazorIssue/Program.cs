using FluentValidation;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazorIssue;
using MudBlazor.Services;
using MudBlazorIssue.Model;
using MudBlazorIssue.Validators;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
builder.Services.AddMudServices();

builder.Services.AddTransient<IValidator<FrequencyViewModel>, FrequencyViewModelValidator>();
builder.Services.AddTransient<AddHeartbeatSettingViewModelValidator>();

await builder.Build().RunAsync();