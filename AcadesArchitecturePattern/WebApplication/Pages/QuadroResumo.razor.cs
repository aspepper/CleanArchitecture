namespace WebApplication.Pages;

public partial class QuadroResumo
{
    public List<Quadro> Resumo { get; set; } = [];

    protected override void OnInitialized()
    {
        Resumo =
        [
            new Quadro { Descricao = "Nós últimos 36 mess, foram identificações", Situacao = "positivo", Tipo = "SetaP",Justifica = "" },
            new Quadro { Descricao = "Foram identificado informações relevantes", Situacao = "negativo", Tipo = "SetaN",Justifica = "" },
            new Quadro { Descricao = "Cliente está relacionado em listas negativas", Situacao = "negativo", Tipo = "SetaN",Justifica = "" },
            new Quadro { Descricao = "Cliente possui metas ou...", Situacao = "positivo", Tipo = "SetaP",Justifica = "" }
        ];
    }

    public class Quadro
    {
        public string Descricao { get; set; } = string.Empty;
        public string Situacao { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string Justifica { get; set; } = string.Empty;
    }

}
