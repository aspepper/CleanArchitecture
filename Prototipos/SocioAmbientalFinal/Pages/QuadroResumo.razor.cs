using System.Reflection.Metadata;

namespace SocioAmbientalFinal.Pages
{
    public partial class QuadroResumo
    {
        public List<Quadro> Resumo { get; set; }

        protected override void OnInitialized()
        {
            Resumo = new List<Quadro>
            {
                new Quadro { Descricao = "Nós últimos 36 mess, foram identificações", Situacao = "positivo", Tipo = "SetaP",Justifica = "" },
                new Quadro { Descricao = "Foram identificado informações relevantes", Situacao = "negativo", Tipo = "SetaN",Justifica = "" },
                new Quadro { Descricao = "Cliente está relacionado em listas negativas", Situacao = "negativo", Tipo = "SetaN",Justifica = "" },
                new Quadro { Descricao = "Cliente possui metas ou...", Situacao = "positivo", Tipo = "SetaP",Justifica = "" }
            };
        }

        public class Quadro
        {
            public string Descricao { get; set; }
            public string Situacao { get; set; }
            public string Tipo { get; set; }
            public string Justifica { get; set; }   
        }

    }
}
