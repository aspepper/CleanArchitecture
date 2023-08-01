namespace SocioAmbientalFinal.Pages
{
    public partial class Agravantes
    {
        public List<Agravante> Agravant { get; set; }
        public List<Mitigador> Mitigadores { get; set; }


        protected override void OnInitialized()
        {
            Agravant = new List<Agravante>
            {
        new Agravante { descricao = "Certidão do Ibama", situacao = "negativo", validade = new DateTime(2022, 12, 01), risco = "negativo" },
        new Agravante { descricao = "Certidão do Ibama", situacao = "positivo", validade = new DateTime(2022, 12, 01), risco = "positivo" },
        new Agravante { descricao = "Certidão do Ibama", situacao = "negativo", validade = new DateTime(2022, 12, 01), risco = "alerta" },
        new Agravante { descricao = "Certidão do Ibama", situacao = "positivo", validade = new DateTime(2022, 12, 01), risco = "negativo" },
        new Agravante { descricao = "Certidão do Ibama", situacao = "negativo", validade = new DateTime(2022, 12, 01), risco = "analise" },
        new Agravante { descricao = "Certidão do Ibama", situacao = "positivo", validade = new DateTime(2022, 12, 01), risco = "alerta" },

    };

            Mitigadores = new List<Mitigador>
    {
        new Mitigador {  descricao = "Certidão do Ibama",situacao = "positivo",validade = new DateTime(2022, 11, 10),risco = "atencao" },
        new Mitigador {  descricao = "Certidão do Ibama",situacao = "negativo",validade = new DateTime(2022, 11, 10),risco = "alerta"},
        new Mitigador {  descricao = "Certidão do Ibama",situacao = "positivo",validade = new DateTime(2022, 11, 10),risco = "positivo"},
        new Mitigador {  descricao = "Certidão do Ibama",situacao = "positivo",validade = new DateTime(2022, 11, 10),risco = "negativo" },
        new Mitigador {  descricao = "Certidão do Ibama",situacao = "negativo",validade = new DateTime(2022, 11, 10),risco = "analise" },
        new Mitigador {  descricao = "Certidão do Ibama",situacao = "positivo",validade = new DateTime(2022, 11, 10),risco = "atencao" },

         };
        }

        public class Agravante
        {
            public string descricao { get; set; }
            public string situacao { get; set; }
            public DateTime validade { get; set; }
            public string risco { get; set; }
        }

        public class Mitigador
        {
            public string descricao { get; set; }
            public string situacao { get; set; }
            public DateTime validade { get; set; }
            public string risco { get; set; }
        }


    }
}
