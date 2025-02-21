using Blazored.LocalStorage;

namespace WebApplication.Services;


#region Interface
public interface IAuthenticateService
{
    void Authenticate();
    void Authorization(int ruleId);

}
#endregion

public class AuthenticateService : IAuthenticateService
{

    private readonly HttpClient httpClient;
    private readonly IConfiguration configuration;
    private readonly ILocalStorageService localStorage;

    public AuthenticateService(IConfiguration configuration, ILocalStorageService localStorage)
    {
        this.configuration = configuration;
        this.localStorage = localStorage;

        var apiURL = this.localStorage.GetItemAsStringAsync("advLinkWebApi").Result ?? throw new Exception("URL da API não carregada da configuração");

        httpClient = new HttpClient()
        {
            BaseAddress = new Uri(apiURL + "/")
        };
        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
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
