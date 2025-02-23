namespace WebApplicationImoveis.Pages;

public partial class Emissao
   {
    public List<Situacao> Situa { get; set; } = [];
    public List<Emiss> GEE { get; set; } = [];

    protected override void OnInitialized()
    {
        Situa =
        [
            new Situacao { Descricao = "Absorções (diretas) realizadas nos últimos 12 meses", Status = "Avaliação realizada pela instituição financeira", Quantidade = 1132535000000.00 },
            new Situacao { Descricao = "Absorções (diretas) realizadas nos últimos 12 meses", Status = "Avaliação realizada pela instituição financeira", Quantidade = 1132535000000.00 },
            new Situacao { Descricao = "Absorções (diretas) realizadas nos últimos 12 meses", Status = "Avaliação realizada pela instituição financeira", Quantidade = 1132535000000.00 }
        ];

        GEE =
        [
            new Emiss { Descricao = "Absorções (diretas) realizadas nos últimos 12 meses", Situacao = "Avaliação realizada pela instituição financeira", Quantidade = 1132535000000.00 },
            new Emiss { Descricao = "Absorções (diretas) realizadas nos últimos 12 meses", Situacao = "Avaliação realizada pela instituição financeira", Quantidade = 1132535000000.00 },
            new Emiss { Descricao = "Absorções (diretas) realizadas nos últimos 12 meses", Situacao = "Avaliação realizada pela instituição financeira", Quantidade = 1132535000000.00 }
        ];
    }

    public class Situacao
    {
        public string Descricao { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public double Quantidade { get; set; }
    }

    public class Emiss
    {
        public string Descricao { get; set; } = string.Empty;
        public string Situacao { get; set; } = string.Empty;
        public double Quantidade { get; set; }
    }
}
