namespace WebApplicationImoveis.Pages;

public partial class Financa
{
    public List<ItemFinanca> Financeiros { get; set; } = [];
    public List<Socio> Adminis { get; set; } = [];
    public List<ParticipacaoSocietaria> Coligada { get; set; } = [];

    protected override void OnInitialized()
    {
        Financeiros =
        [
            new ItemFinanca { Faturamento2022 = 1000.0, Faturamento2021 = 900.0, CapitalSocial = 5000.0, Credito= 800.0, Tvm = 500.0 , Oca= 1500},
        ];

        Adminis =
        [
            new Socio { Nome = "Alex Pimenta",   Documento = 123456.789, Tipo = "", Particip = 50 },
            new Socio { Nome = "Thatiane Gregório", Documento = 987654.321, Tipo = "", Particip = 40 },
            new Socio { Nome = "Estela G Pimenta", Documento = 123456.789, Tipo = "", Particip = 20 },
            new Socio { Nome = "Arthur G Pimenta", Documento = 987654.321, Tipo = "", Particip = 100 },
            new Socio { Nome = "Cinthia Santos", Documento = 123456.789, Tipo = "", Particip = 10 },
            new Socio { Nome = "Bruno Silvestre", Documento = 987654.321, Tipo = "", Particip = 5 },
            new Socio { Nome = "Patrick Moura", Documento = 123456.789, Tipo = "", Particip = 10 },

        ];

        Coligada =
        [
            new ParticipacaoSocietaria { Nome = "Reginaldo Álves", Relacionamento = "Coligada", AtividadeEconomica = "Indústria" },
            new ParticipacaoSocietaria { Nome = "Cássio Mendes", Relacionamento = "Sócia", AtividadeEconomica = "Comércio" },
            new ParticipacaoSocietaria { Nome = "Artur Dias", Relacionamento = "Coligada", AtividadeEconomica = "Indústria" },
            new ParticipacaoSocietaria { Nome = "Thiago Yamada", Relacionamento = "Sócia", AtividadeEconomica = "Comércio" },
        ];
    }

    public class ItemFinanca
    {
        public double Faturamento2022 { get; set; }
        public double Faturamento2021 { get; set; }
        public double CapitalSocial { get; set; }
        public double Credito { get; set; }
        public double Tvm { get; set; }
        public double Oca { get; set; }


    }

    public class Socio
    {
        public string Nome { get; set; } = string.Empty;
        public double Documento { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public double Particip { get; set; }
    }

    public class ParticipacaoSocietaria
    {
        public string Nome { get; set; } = string.Empty;
        public string Relacionamento { get; set; } = string.Empty;
        public string AtividadeEconomica { get; set; } = string.Empty;
    }
}

