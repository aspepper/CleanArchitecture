using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace WebApplication.Shared;

public partial class MainLayout
{

    [Inject]
    public required IJSRuntime JSRuntime { get; set; }

    [Parameter]
    public EventCallback<bool> OnDarkModeChanged { get; set; }

    private bool isDark = false;

    private bool isDarkTheme = true;
    public bool IsDark
    {
        get
        {
            var js = (IJSInProcessRuntime)JSRuntime;
            var result = js.Invoke<string>("AcadesComplianceCurrentTheme.get");
            isDarkTheme = bool.Parse(result ?? "false");
            return isDarkTheme;
        }
        set
        {
            isDarkTheme = value;
            var js = (IJSInProcessRuntime)JSRuntime;
            js.InvokeVoid("AcadesComplianceCurrentTheme.set", isDarkTheme.ToString());
        }
    }

    private void HandleSubmit()
    {
        // Process the form
    }

    private void ToggleDarkMode()
    {
        isDark = !isDark;
        var js = (IJSInProcessRuntime)JSRuntime;
        js.InvokeVoid("AcadesComplianceCurrentTheme.set", isDark.ToString().ToLower());

        // Notifica a alteração do modo escuro/claro para o componente pai
        OnDarkModeChanged.InvokeAsync(isDark);
    }

    private string GetMainCssClass()
    {
        return IsDark ? "dark-mode" : "light-mode";
    }

    //private Task OnDarkModeChanged(bool isDark)
    //{
    //    IsDark = isDark;
    //    StateHasChanged(); // Atualiza o componente para refletir a alteração do modo escuro/claro
    //    return Task.CompletedTask;
    //}

}

