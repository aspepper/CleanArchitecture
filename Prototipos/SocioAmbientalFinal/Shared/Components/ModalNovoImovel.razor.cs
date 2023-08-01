
   
namespace SocioAmbientalFinal.Shared.Components
{
    public partial class ModalNovoImovel
    {
        public class NovoImovelModel
        {
            public string TipoImovel { get; set; }
            public string AreaTotal { get; set; }
            public bool Garantia { get; set; }
            public string Denominacao { get; set; }
            public string Localizacao { get; set; }
            public string Numero { get; set; }
            public string Complemento { get; set; }
            public string Bairro { get; set; }
            public string CEP { get; set; }
            public string Municipio { get; set; }
            public string Estado { get; set; }
            public string Pais { get; set; }
        }

        private NovoImovelModel NovoImovel = new NovoImovelModel();

        private void SalvarImovel()
        {
            // Lógica para salvar o novo imóvel
            // Você pode acessar os dados do formulário através das propriedades em NovoImovel
        }

        private void LimpaModal()
        {
            // Lógica para limpar o modal
        }

    }
}
