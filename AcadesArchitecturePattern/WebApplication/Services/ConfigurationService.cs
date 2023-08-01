using Blazored.LocalStorage;
using System.Text.Json;

namespace WebApplication.Services
{

    #region Interface
    public interface IConfigurationService
    {
        Task Get();
    }
    #endregion

    public class ConfigurationService: IConfigurationService
    {

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILocalStorageService _localStorage;

        public ConfigurationService(IConfiguration configuration, ILocalStorageService localStorage)
        {
            _configuration = configuration;
            _localStorage = localStorage;

            var urlProto = _configuration["APIConfigProtocol"];
            var urlServr = _configuration["APIConfigServer"];
            var urlLinkr = _configuration["APIConfigURL"];
            var urlVersi = _configuration["APIConfigVersion"];

            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri($"{urlProto}://{urlServr}/{urlLinkr}/api/{urlVersi}/GetAdvConfig/")
            };
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task Get()
        {
            var APISyst = _configuration["APIConfigSystem"];
            var APIComp = _configuration["APIConfigCompany"];
            HttpResponseMessage response = await _httpClient.GetAsync($"getConfig/{APISyst}/{APIComp}");

            if (!response.IsSuccessStatusCode)
            { throw new Exception("Configuração não encontrada"); }

            var result = JsonSerializer.Deserialize<JsonElement>(await response.Content.ReadAsStringAsync());
            result.TryGetProperty("advLinkWebApi", out var advLinkWebApi);
            await _localStorage.SetItemAsStringAsync("advLinkWebApi", advLinkWebApi.ToString());

        }

    }
}
