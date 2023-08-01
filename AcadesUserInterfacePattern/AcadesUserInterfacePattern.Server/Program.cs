using AcadesUserInterfacePattern.Server;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddLocalization();

var host = builder.Build();

var jsInterop = host.Services.GetRequiredService<IJSRuntime>();
var result = await jsInterop.InvokeAsync<string>("AcadesComplianceCurrentCulture.get");
CultureInfo culture;
if (result != null)
{
    culture = new CultureInfo(result);
}
else
{
    culture = new CultureInfo("pt-BR");
    await jsInterop.InvokeVoidAsync("AcadesComplianceCurrentCulture.set", "pt-BR");
}
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

await host.RunAsync();

/***************************************************************************************
  -------------------------------     Documents     -----------------------------------
    https://www.puresourcecode.com/dotnet/blazor/svg-icons-and-flags-for-blazor/
    https://www.matblazor.com/
***************************************************************************************/
