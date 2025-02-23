using AcadesArchitecturePattern.Shared.Entities;
using Flunt.Notifications;
using Flunt.Validations;

namespace AcadesArchitecturePattern.Domain.Entities;

/// <summary>
/// Entidade para consumo de API, não é necessário configurá-la no Infra.Data
/// </summary>
public class Configuration : Base
{

    public Configuration()
    {
        AdvCorpLoginAcessoApi = string.Empty;
        AdvCorpLoginApi = string.Empty;
        AdvLinkWebApi = string.Empty;
        AdvWebApiCertificate = string.Empty;
        FiveIcon = string.Empty;
        LogoCliente = string.Empty;
        NomeEmpresa = string.Empty;
        PathAplicacao = string.Empty;
        Titulo = string.Empty;
        UrlApiRisc = string.Empty;
        VersaoApi = string.Empty;
    }

    /*
        returns:

        advCorpLoginAcessoApi: null
        advCorpLoginApi: "https://localhost:7246/Authenticate"
        advLinkWebApi: "http://advdes15/Acades.Corp.Rest"
        advWebApiCertificate: null
        fiveIcon: null
        logoCliente: null
        nomeEmpresa: null
        pathAplicacao: null
        titulo: null
        urlApiRisc: "http://192.168.0.238/API_ReLATORIO_TeSTE"
        versaoApi: "v1.0"

        */
    public Configuration(
        string advCorpLoginAcessoApi,
        string advCorpLoginApi,
        string advLinkWebApi,
        string advWebApiCertificate,
        string fiveIcon,
        string logoCliente,
        string nomeEmpresa,
        string pathAplicacao,
        string titulo,
        string urlApiRisc,
        string versaoApi)
    {
        AddNotifications(
            new Contract<Notification>()
        );

        AdvCorpLoginAcessoApi = advCorpLoginAcessoApi;
        AdvCorpLoginApi = advCorpLoginApi;
        AdvLinkWebApi = advLinkWebApi;
        AdvWebApiCertificate = advWebApiCertificate;
        FiveIcon = fiveIcon;
        LogoCliente = logoCliente;
        NomeEmpresa = nomeEmpresa;
        PathAplicacao = pathAplicacao;
        Titulo = titulo;
        UrlApiRisc = urlApiRisc;
        VersaoApi = versaoApi;
    }

    public string AdvCorpLoginAcessoApi { get; set; }
    public string AdvCorpLoginApi { get; set; }
    public string AdvLinkWebApi { get; set; }
    public string AdvWebApiCertificate { get; set; }
    public string FiveIcon { get; set; }
    public string LogoCliente { get; set; }
    public string NomeEmpresa { get; set; }
    public string PathAplicacao { get; set; }
    public string Titulo { get; set; }
    public string UrlApiRisc { get; set; }
    public string VersaoApi { get; set; }
}