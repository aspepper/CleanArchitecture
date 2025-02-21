namespace WebApplication.Shared.Components;

public partial class ModalNovoImovel
{
    public class NovoImovelModel
    {
        public string TipoImovel { get; set; } = string.Empty;
        public string AreaTotal { get; set; } = string.Empty;
        public bool Garantia { get; set; }
        public string Denominacao { get; set; } = string.Empty;
        public string Localizacao { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string CEP { get; set; } = string.Empty;
        public string Municipio { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;
    }

    private NovoImovelModel novoImovel = new();

    private void SalvarImovel()
    {
        // Lógica para salvar o novo imóvel
        // Você pode acessar os dados do formulário através das propriedades em novoImovel
    }

    private void LimpaModal()
    {
        // Lógica para limpar o modal
    }

}
