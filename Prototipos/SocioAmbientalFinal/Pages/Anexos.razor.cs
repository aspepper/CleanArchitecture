using System;
using System.Collections.Generic;

namespace SocioAmbientalFinal.Pages
{
    public partial class Anexos
    {
        public List<Anexo> Anex { get; set; }

        protected override void OnInitialized()
        {
            Anex = new List<Anexo>
            {
                new Anexo {descricao = "Certidão do Ibama", origem = "Anexos", data = DateTime.Now},
                new Anexo {descricao = "Certidão do Ibama", origem = "Análise de Imóvel", data = DateTime.Now},
                new Anexo {descricao = "Certidão do Ibama", origem = "Análise de Imóvel", data = DateTime.Now},
                new Anexo {descricao = "Certidão do Ibama", origem = "Anexos", data = DateTime.Now},
                new Anexo {descricao = "Certidão do Ibama", origem = "Visualizar", data = DateTime.Now},
                new Anexo {descricao = "Certidão do Ibama", origem = "pendente", data = DateTime.Now},
                new Anexo {descricao = "Certidão do Ibama", origem = "negado", data = DateTime.Now},
                new Anexo {descricao = "Certidão do Ibama", origem = "Anexos", data = DateTime.Now},
                new Anexo {descricao = "Certidão do Ibama", origem = "Anexos", data = DateTime.Now}
            };
        }

        public class Anexo
        {
            public string descricao { get; set; }
            public string origem { get; set; }
            public DateTime data { get; set; }
        }
    }
}

