namespace WebApplicationImoveis.Pages;

public partial class Agravantes
{
    public List<Agravante> Agravant { get; set; } = [];
    public List<Mitigador> Mitigadores { get; set; } = [];


    protected override void OnInitialized()
    {
        Agravant =
        [
            new Agravante { Descricao = "Certidão do Ibama", Situacao = "negativo", Validade = new DateTime(2022, 12, 01), Risco = "negativo" },
            new Agravante { Descricao = "Certidão do Ibama", Situacao = "positivo", Validade = new DateTime(2022, 12, 01), Risco = "positivo" },
            new Agravante { Descricao = "Certidão do Ibama", Situacao = "negativo", Validade = new DateTime(2022, 12, 01), Risco = "alerta" },
            new Agravante { Descricao = "Certidão do Ibama", Situacao = "positivo", Validade = new DateTime(2022, 12, 01), Risco = "negativo" },
            new Agravante { Descricao = "Certidão do Ibama", Situacao = "negativo", Validade = new DateTime(2022, 12, 01), Risco = "Analise" },
            new Agravante { Descricao = "Certidão do Ibama", Situacao = "positivo", Validade = new DateTime(2022, 12, 01), Risco = "alerta" },

        ];

        Mitigadores =
        [
            new Mitigador {  Descricao = "Certidão do Ibama",Situacao = "positivo",Validade = new DateTime(2022, 11, 10),Risco = "atencao" },
            new Mitigador {  Descricao = "Certidão do Ibama",Situacao = "negativo",Validade = new DateTime(2022, 11, 10),Risco = "alerta"},
            new Mitigador {  Descricao = "Certidão do Ibama",Situacao = "positivo",Validade = new DateTime(2022, 11, 10),Risco = "positivo"},
            new Mitigador {  Descricao = "Certidão do Ibama",Situacao = "positivo",Validade = new DateTime(2022, 11, 10),Risco = "negativo" },
            new Mitigador {  Descricao = "Certidão do Ibama",Situacao = "negativo",Validade = new DateTime(2022, 11, 10),Risco = "Analise" },
            new Mitigador {  Descricao = "Certidão do Ibama",Situacao = "positivo",Validade = new DateTime(2022, 11, 10),Risco = "atencao" },

        ];
    }

    public class Agravante
    {
        public string Descricao { get; set; } = string.Empty;
        public string Situacao { get; set; } = string.Empty;
        public DateTime Validade { get; set; }
        public string Risco { get; set; } = string.Empty;
    }

    public class Mitigador
    {
        public string Descricao { get; set; } = string.Empty;
        public string Situacao { get; set; } = string.Empty;
        public DateTime Validade { get; set; }
        public string Risco { get; set; } = string.Empty;
    }


}
