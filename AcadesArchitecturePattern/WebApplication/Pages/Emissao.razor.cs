namespace WebApplication.Pages
{
    public partial class Emissao
       {
        public List<Situacao> Situa { get; set; }
        public List<Emiss> GEE { get; set; }

        protected override void OnInitialized()
        {
            Situa = new List<Situacao>
    {
        new Situacao { descricao = "Absorções (diretas) realizadas nos últimos 12 meses", situacao = "Avaliação realizada pela instituição financeira", quantidade = 1132535000000.00 },
        new Situacao { descricao = "Absorções (diretas) realizadas nos últimos 12 meses", situacao = "Avaliação realizada pela instituição financeira", quantidade = 1132535000000.00 },
        new Situacao { descricao = "Absorções (diretas) realizadas nos últimos 12 meses", situacao = "Avaliação realizada pela instituição financeira", quantidade = 1132535000000.00 }
    };

            GEE = new List<Emiss>
    {
        new Emiss { descricao = "Absorções (diretas) realizadas nos últimos 12 meses", situacao = "Avaliação realizada pela instituição financeira", quantidade = 1132535000000.00 },
        new Emiss { descricao = "Absorções (diretas) realizadas nos últimos 12 meses", situacao = "Avaliação realizada pela instituição financeira", quantidade = 1132535000000.00 },
        new Emiss { descricao = "Absorções (diretas) realizadas nos últimos 12 meses", situacao = "Avaliação realizada pela instituição financeira", quantidade = 1132535000000.00 }
    };
        }

        public class Situacao
        {
            public string descricao { get; set; }
            public string situacao { get; set; }
            public double quantidade { get; set; }
        }

        public class Emiss
        {
            public string descricao { get; set; }
            public string situacao { get; set; }
            public double quantidade { get; set; }
        }
    }
}
