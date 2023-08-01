using AcadesArchitecturePattern.Domain.Entities;
using AcadesArchitecturePattern.Shared.Queries;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;

namespace AcadesArchitecturePattern.Domain.Queries.Users
{
    public class GetConfigurationByCompanyQuery : Notifiable<Notification>, IQuery, IRequest<GenericQueryResult>
    {
        public string Company { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract<Notification>()
                    .Requires()
                    .IsNotEmpty(Company, "Company", "O campo 'Company' não pode estar vazio")
                );
        }

        public class ConfigurationResult
        {
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
    }
}
