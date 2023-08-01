using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace AdviceUserInterfacePattern.Server.Shared
{
    public partial class MainLayout
    {
        [Inject]
        public required IJSRuntime JSRuntime { get; set; }

        private bool _isDarkTheme = true;
        public bool IsDark
        {
            get
            {
                var js = (IJSInProcessRuntime)JSRuntime;
                var result = js.Invoke<string>("adviceComplianceCurrentTheme.get");
                _isDarkTheme = bool.Parse(result ?? "false");
                return _isDarkTheme;
            }
            set
            {
                _isDarkTheme = value;
                var js = (IJSInProcessRuntime)JSRuntime;
                js.InvokeVoid("adviceComplianceCurrentTheme.set", _isDarkTheme.ToString());
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
