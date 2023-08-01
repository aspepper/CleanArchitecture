using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace WebApplication.Pages
{
    public partial class PerfilAvaliado
    {

        protected override async Task OnInitializedAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(3));
            //await JSRuntime.InvokeVoidAsync("graficoRisco");
        }

        [Inject]
        public required IJSRuntime JSRuntime { get; set; }

        string nomeIdAbrirModal = "AbrirModal";
        bool botaoAtivado = false;

        private bool _isDarkTheme = true;
        public bool IsDark
        {
            get
            {
                var js = (IJSInProcessRuntime)JSRuntime;
                var result = js.Invoke<string>("AcadesComplianceCurrentTheme.get");
                _isDarkTheme = bool.Parse(result ?? "false");
                return _isDarkTheme;
            }
            set
            {
                _isDarkTheme = value;
                var js = (IJSInProcessRuntime)JSRuntime;
                js.InvokeVoid("AcadesComplianceCurrentTheme.set", _isDarkTheme.ToString());
            }
        }

        private string GetMainCssClass()
        {
            return IsDark ? "dark-mode" : "light-mode";
        }

        private Task OnDarkModeChanged(bool isDark)
        {
            IsDark = isDark;
            StateHasChanged(); // Atualiza o componente para refletir a alteração do modo escuro/claro
            return Task.CompletedTask;
        }


    }
}
