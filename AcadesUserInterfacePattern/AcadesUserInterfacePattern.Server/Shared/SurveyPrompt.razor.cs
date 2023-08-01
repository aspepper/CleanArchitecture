using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace AcadesUserInterfacePattern.Server.Shared
{
    public partial class SurveyPrompt
    {
        // Demonstrates how a parent component can supply parameters
        [Parameter]
        public string? Title { get; set; }

        [Parameter]
        public string? Message { get; set; }

        public MarkupString HtmlMessage { get { return new(string.Format((Message ?? ""), "<a target=\"_blank\" class=\"font-weight-bold\" href=\"https://go.microsoft.com/fwlink/?linkid=2186157\">", "</a>")); } }

        [Inject]
        public required IJSRuntime JSRuntime { get; set; }

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
