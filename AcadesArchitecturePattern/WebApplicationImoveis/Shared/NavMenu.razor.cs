using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Globalization;

namespace WebApplicationImoveis.Shared;

public partial class NavMenu
{
    [Parameter]
    public EventCallback<bool> OnDarkModeChanged { get; set; }

    private bool isDark = false;


    [Inject]
    public required IJSRuntime JSRuntime { get; set; }

    private bool collapseNavMenu = true;

    private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

    private void ToggleNavMenu()
    {
        collapseNavMenu = !collapseNavMenu;
    }

    private async Task<CultureInfo> GetCurrentCulture()
    {
        var jsInterop = (IJSInProcessRuntime)JSRuntime;
        var result = await jsInterop.InvokeAsync<string>("AcadesComplianceCurrentCulture.get");
        if (result != null) { return new CultureInfo(result); }
        return new CultureInfo("pt-BR");
    }

    protected async Task OnChangeLanguageClickAsync(string value)
    {
        await Task.Delay(new TimeSpan(0, 0, 2));
        Console.WriteLine(value);
        CultureInfo cultureInfo = new(value);
        CultureInfo currentInfo = await GetCurrentCulture();
        if (currentInfo.Name != cultureInfo.Name)
        {
            var js = (IJSInProcessRuntime)JSRuntime;
            js.InvokeVoid("AcadesComplianceCurrentCulture.set", cultureInfo.Name);
            Console.WriteLine($"value.Name = {cultureInfo.Name}");
            Navigation.NavigateTo(Navigation.Uri, forceLoad: true);
        }
    }

    private void ToggleDarkMode()
    {
        isDark = !isDark;
        var js = (IJSInProcessRuntime)JSRuntime;
        js.InvokeVoid("AcadesComplianceCurrentTheme.set", isDark.ToString().ToLower());

        // Notifica a alteração do modo escuro/claro para o componente pai
        OnDarkModeChanged.InvokeAsync(isDark);
    }

}
