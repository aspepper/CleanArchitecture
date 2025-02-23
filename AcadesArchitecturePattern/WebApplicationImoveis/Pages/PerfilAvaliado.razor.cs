using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace WebApplicationImoveis.Pages;

public partial class PerfilAvaliado
{

    protected override async Task OnInitializedAsync()
    {
        await Task.Delay(TimeSpan.FromSeconds(3));
        //await JSRuntime.InvokeVoidAsync("graficoRisco");
    }

    [Inject]
    public required IJSRuntime JSRuntime { get; set; }

    internal string nomeIdAbrirModal = "AbrirModal";
    internal bool botaoAtivado = false;

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

    private string GetMainCssClass() => IsDark ? "dark-mode" : "light-mode";

    private Task OnDarkModeChanged(bool isDark)
    {
        IsDark = isDark;
        StateHasChanged(); // Atualiza o componente para refletir a alteração do modo escuro/claro
        return Task.CompletedTask;
    }


}
