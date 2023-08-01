using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace AcadesUserInterfacePattern.Server.Pages
{
    public partial class FetchData
    {
        private WeatherForecast[]? forecasts;

        protected override async Task OnInitializedAsync()
        {
            forecasts = await Http.GetFromJsonAsync<WeatherForecast[]>("sample-data/weather.json");
        }

        public class WeatherForecast
        {
            public DateOnly Date { get; set; }

            public int TemperatureC { get; set; }

            public string? Summary { get; set; }

            public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        }

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
