using Microsoft.AspNetCore.Components;

namespace WebApplication.Pages;

public partial class Operacoes : ComponentBase
{
    public List<Exposicao> Opera { get; set; } = [];
    public List<Operar> Tvm { get; set; } = [];
    public List<Exposicoes> Posit { get; set; } = [];

    protected override void OnInitialized()
    {
        Opera =
        [
            new Exposicao { Ipoc = 2342342345023, Sicor = "SIM", Valor = 7344002340123.00, Analise = "", Risco = "positivo" },
            new Exposicao { Ipoc = 2342342345023, Sicor = "NÃO", Valor = 7344002340123.00, Analise = "", Risco = "alerta" },
            new Exposicao { Ipoc = 2342342345023, Sicor = "SIM", Valor = 7344002340123.00, Analise = "", Risco = "negativo" },
            new Exposicao { Ipoc = 2342342345023, Sicor = "SIM", Valor = 7344002340123.00, Analise = "", Risco = "atencao" },
        ];

        Tvm =
        [
            new Operar { Codigo = 215421547802, Registro = "", Titulo = 1132535000000.00, Tipo = "", Analisado = "", Riscos = "atencao" },
            new Operar { Codigo = 215421547802, Registro = "", Titulo = 1132535000000.00, Tipo = "", Analisado = "", Riscos = "negativo" },
            new Operar { Codigo = 215421547802, Registro = "", Titulo = 1132535000000.00, Tipo = "", Analisado = "", Riscos = "positivo" },
            new Operar { Codigo = 215421547802, Registro = "", Titulo = 1132535000000.00, Tipo = "", Analisado = "", Riscos = "Analise" }
        ];

        Posit =
        [
            new Exposicoes { Situacoes = "Enquadramento Total", Saldo = 6475000005220.00, Tvm = 1000000000000.00 },
            new Exposicoes { Situacoes = "Enquadramento Total", Saldo = 6475000005220.00, Tvm = 1000000000000.00 },
            new Exposicoes { Situacoes = "Enquadramento Total", Saldo = 6475000005220.00, Tvm = 1000000000000.00 }
        ];
    }

    public class Exposicao
    {
        public long Ipoc { get; set; }
        public string Sicor { get; set; } = string.Empty;
        public double Valor { get; set; }
        public string Analise { get; set; } = string.Empty;
        public string Risco { get; set; } = string.Empty;
    }

    public class Operar
    {
        public long Codigo { get; set; }
        public string Registro { get; set; } = string.Empty;
        public double Titulo { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Analisado { get; set; } = string.Empty;
        public string Riscos { get; set; } = string.Empty;
    }

    public class Exposicoes
    {
        public string Situacoes { get; set; } = string.Empty;
        public double Saldo { get; set; }
        public double Tvm { get; set; }
    }
}
