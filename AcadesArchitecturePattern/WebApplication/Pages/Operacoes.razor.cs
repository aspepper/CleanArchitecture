using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace WebApplication.Pages
{
    public partial class Operacoes : ComponentBase
    {
        public List<Exposicao> Opera { get; set; }
        public List<Operar> Tvm { get; set; }
        public List<Exposicoes> Posit { get; set; }

        protected override void OnInitialized()
        {
            Opera = new List<Exposicao>
            {
                new Exposicao { ipoc = 2342342345023, sicor = "SIM", valor = 7344002340123.00, analise = "", risco = "positivo" },
                new Exposicao { ipoc = 2342342345023, sicor = "NÃO", valor = 7344002340123.00, analise = "", risco = "alerta" },
                new Exposicao { ipoc = 2342342345023, sicor = "SIM", valor = 7344002340123.00, analise = "", risco = "negativo" },
                new Exposicao { ipoc = 2342342345023, sicor = "SIM", valor = 7344002340123.00, analise = "", risco = "atencao" },
            };

            Tvm = new List<Operar>
            {
                new Operar { codigo = 215421547802, registro = "", titulo = 1132535000000.00, tipo = "", analisado = "", riscos = "atencao" },
                new Operar { codigo = 215421547802, registro = "", titulo = 1132535000000.00, tipo = "", analisado = "", riscos = "negativo" },
                new Operar { codigo = 215421547802, registro = "", titulo = 1132535000000.00, tipo = "", analisado = "", riscos = "positivo" },
                new Operar { codigo = 215421547802, registro = "", titulo = 1132535000000.00, tipo = "", analisado = "", riscos = "analise" }
            };

            Posit = new List<Exposicoes>
            {
                new Exposicoes { situacoes = "Enquadramento Total", saldo = 6475000005220.00, tvm = 1000000000000.00 },
                new Exposicoes { situacoes = "Enquadramento Total", saldo = 6475000005220.00, tvm = 1000000000000.00 },
                new Exposicoes { situacoes = "Enquadramento Total", saldo = 6475000005220.00, tvm = 1000000000000.00 }
            };
        }

        public class Exposicao
        {
            public long ipoc { get; set; }
            public string sicor { get; set; }
            public double valor { get; set; }
            public string analise { get; set; }
            public string risco { get; set; }
        }

        public class Operar
        {
            public long codigo { get; set; }
            public string registro { get; set; }
            public double titulo { get; set; }
            public string tipo { get; set; }
            public string analisado { get; set; }
            public string riscos { get; set; }
        }

        public class Exposicoes
        {
            public string situacoes { get; set; }
            public double saldo { get; set; }
            public double tvm { get; set; }
        }
    }
}
