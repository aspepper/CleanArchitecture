using static System.Net.WebRequestMethods;

namespace AdviceCompliance.WebApplication.Pages
{
    public partial class Index
    {

        private Dictionary<string, object>? Parametro;
        private readonly string PageTitle = "Home";
        private readonly string TituloPagina = "Home";
        private readonly string CorpoPagina = "Olá visitante!";

        protected override async Task OnInitializedAsync()
        {
            Parametro = new Dictionary<string, object> {
                { "Title", "Bem vindos ao aplicativo da Advice Compliance." }
            };
        }
    }
}
