namespace WebApplicationImoveis.Pages;

public partial class Anexos
{
    public List<Anexo> Anex { get; set; } = [];

    protected override void OnInitialized()
    {
        Anex =
        [
            new Anexo {Descricao = "Certidão do Ibama", Origem = "Anexos", Data = DateTime.Now},
            new Anexo {Descricao = "Certidão do Ibama", Origem = "Análise de Imóvel", Data = DateTime.Now},
            new Anexo {Descricao = "Certidão do Ibama", Origem = "Análise de Imóvel", Data = DateTime.Now},
            new Anexo {Descricao = "Certidão do Ibama", Origem = "Anexos", Data = DateTime.Now},
            new Anexo {Descricao = "Certidão do Ibama", Origem = "Visualizar", Data = DateTime.Now},
            new Anexo {Descricao = "Certidão do Ibama", Origem = "pendente", Data = DateTime.Now},
            new Anexo {Descricao = "Certidão do Ibama", Origem = "negado", Data = DateTime.Now},
            new Anexo {Descricao = "Certidão do Ibama", Origem = "Anexos", Data = DateTime.Now},
            new Anexo {Descricao = "Certidão do Ibama", Origem = "Anexos", Data = DateTime.Now}
        ];
    }

    public class Anexo
    {
        public string Descricao { get; set; } = string.Empty;
        public string Origem { get; set; } = string.Empty;
        public DateTime Data { get; set; }
    }
}

