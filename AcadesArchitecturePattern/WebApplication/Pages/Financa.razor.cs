namespace WebApplication.Pages
{
    public partial class Financa
    {
        public List<ItemFinanca> Financeiros { get; set; }
        public List<Socio> Adminis { get; set; }
        public List<ParticipacaoSocietaria> Coligada { get; set; }

        protected override void OnInitialized()
        {
            Financeiros = new List<ItemFinanca>
            {
                new ItemFinanca { Faturamento2022 = 1000.0, Faturamento2021 = 900.0, CapitalSocial = 5000.0, Credito= 800.0, Tvm = 500.0 , Oca= 1500},

            };

            Adminis = new List<Socio>
            {
                new Socio { Nome = "John Doe",   Documento = 123456.789, Tipo = "", Particip = 50 },
                new Socio { Nome = "Jane Smith", Documento = 987654.321, Tipo = "", Particip = 40 },
                new Socio { Nome = "John Satriani", Documento = 123456.789, Tipo = "", Particip = 20 },
                new Socio { Nome = "Jimi Hendrix", Documento = 987654.321, Tipo = "", Particip = 100 },
                new Socio { Nome = "Jim Morrison", Documento = 123456.789, Tipo = "", Particip = 10 },
                new Socio { Nome = "Ian Cuts", Documento = 987654.321, Tipo = "", Particip = 5 },
                new Socio { Nome = "Joey Ramones", Documento = 123456.789, Tipo = "", Particip = 10 },

            };

            Coligada = new List<ParticipacaoSocietaria>
            {
                new ParticipacaoSocietaria { Nome = "Joey Ramone", Relacionamento = "Coligada", AtividadeEconomica = "Indústria" },
                new ParticipacaoSocietaria { Nome = "Ian Curtis", Relacionamento = "Sócia", AtividadeEconomica = "Comércio" },
                new ParticipacaoSocietaria { Nome = "Jim Morrison", Relacionamento = "Coligada", AtividadeEconomica = "Indústria" },
                new ParticipacaoSocietaria { Nome = "Jane Smith", Relacionamento = "Sócia", AtividadeEconomica = "Comércio" },
            };
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
            public string Nome { get; set; }
            public double Documento { get; set; }
            public string Tipo { get; set; }
            public double Particip { get; set; }
        }

        public class ParticipacaoSocietaria
        {
            public string Nome { get; set; }
            public string Relacionamento { get; set; }
            public string AtividadeEconomica { get; set; }
        }
    }
}

