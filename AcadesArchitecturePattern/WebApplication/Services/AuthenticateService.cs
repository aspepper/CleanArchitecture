using Blazored.LocalStorage;
using System;

namespace WebApplication.Services
{

    #region Interface
    public interface IAuthenticateService
    {
        void Authenticate();
        void Authorization(int ruleId);

    }
    #endregion

    public class AuthenticateService : IAuthenticateService
    {

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILocalStorageService _localStorage;

        public AuthenticateService(IConfiguration configuration, ILocalStorageService localStorage)
        {
            _configuration = configuration;
            _localStorage = localStorage;

            var apiURL = _localStorage.GetItemAsStringAsync("advLinkWebApi").Result ?? throw new Exception("URL da API não carregada da configuração");

            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(apiURL + "/")
            };
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void Authenticate()
        {
            throw new NotImplementedException();
        }

        public void Authorization(int ruleId)
        {
            throw new NotImplementedException();
        }
    }
}
