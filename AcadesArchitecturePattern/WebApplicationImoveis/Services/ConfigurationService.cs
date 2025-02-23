using Blazored.LocalStorage;
using System.Text.Json;

namespace WebApplicationImoveis.Services
{

    #region Interface
    public interface IConfigurationService
    {
        Task Get();
    }
    #endregion

    public class ConfigurationService: IConfigurationService
    {

        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;
        private readonly ILocalStorageService localStorage;

        public ConfigurationService(IConfiguration configuration, ILocalStorageService localStorage)
        {
            this.configuration = configuration;
            this.localStorage = localStorage;

            var urlProto = this.configuration["APIConfigProtocol"];
            var urlServr = this.configuration["APIConfigServer"];
            var urlLinkr = this.configuration["APIConfigURL"];
            var urlVersi = this.configuration["APIConfigVersion"];

            httpClient = new HttpClient()
            {
                BaseAddress = new Uri($"{urlProto}://{urlServr}/{urlLinkr}/api/{urlVersi}/GetAdvConfig/")
            };
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task Get()
        {
            var APISyst = configuration["APIConfigSystem"];
            var APIComp = configuration["APIConfigCompany"];
            HttpResponseMessage response = await httpClient.GetAsync($"getConfig/{APISyst}/{APIComp}");

            if (!response.IsSuccessStatusCode)
            { throw new Exception("Configuração não encontrada"); }

            var result = JsonSerializer.Deserialize<JsonElement>(await response.Content.ReadAsStringAsync());
            result.TryGetProperty("advLinkWebApi", out var advLinkWebApi);
            await localStorage.SetItemAsStringAsync("advLinkWebApi", advLinkWebApi.ToString());

        }

    }
}
