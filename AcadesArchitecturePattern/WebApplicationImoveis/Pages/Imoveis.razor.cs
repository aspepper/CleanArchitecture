namespace WebApplicationImoveis.Pages;

public partial class Imoveis
{
    public List<Imovel> ListaImoveis { get; set; } = [];

    public PaginatedList<Imovel> ImoveisPaginado { get; set; }

    protected override void OnInitialized()
    {
        ListaImoveis =
        [
            new Imovel {Descricao = "Certidão do Ibama",Situacao = "positivo",Tipo = "positivo",Garantia = "positivo",Cafir = "positivo"},
            new Imovel {Descricao = "Certidão do Ibama",Situacao = "negativo",Tipo = "negativo",Garantia = "negativo",Cafir = "negativo"},
            new Imovel {Descricao = "Certidão do Ibama",Situacao = "atencao",Tipo = "",Garantia = "",Cafir = "atencao"},
            new Imovel {Descricao = "Certidão do Ibama",Situacao = "positivo",Tipo = "positivo",Garantia = "positivo",Cafir = "positivo"},
            new Imovel {Descricao = "Certidão do Ibama",Situacao = "negativo",Tipo = "negativo",Garantia = "negativo",Cafir = "negativo"},
            new Imovel {Descricao = "Certidão do Ibama",Situacao = "atencao",Tipo = "",Garantia = "",Cafir = "atencao"},
            new Imovel {Descricao = "Certidão do Ibama",Situacao = "positivo",Tipo = "positivo",Garantia = "positivo",Cafir = "positivo"},
            new Imovel {Descricao = "Certidão do Ibama",Situacao = "negativo",Tipo = "negativo",Garantia = "negativo",Cafir = "negativo"},
            new Imovel {Descricao = "Certidão do Ibama",Situacao = "atencao",Tipo = "",Garantia = "",Cafir = "atencao"},
        ];

        // Define a paginação inicial
        ImoveisPaginado = GetPagedData(ListaImoveis, 1, 10);
    }

    public class Imovel
    {
        public string Descricao { get; set; } = string.Empty;
        public string Situacao { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string Garantia { get; set; } = string.Empty;
        public string Cafir { get; set; } = string.Empty;
    }

    public class PaginatedList<T>(List<T> items, int count, int pageIndex, int pageSize)
    {
        public int PageIndex { get; set; } = pageIndex;
        public int PageSize { get; set; } = pageSize;
        public int TotalCount { get; set; } = count;
        public List<T> Items { get; set; } = items;

        public bool HasPreviousPage
        {
            get { return PageIndex > 1; }
        }

        public bool HasNextPage
        {
            get { return PageIndex < TotalPages; }
        }

        public int TotalPages
        {
            get { return (int)Math.Ceiling(TotalCount / (double)PageSize); }
        }
    }

    public PaginatedList<T> GetPagedData<T>(List<T> allData, int pageIndex, int pageSize)
    {
        var totalCount = allData.Count;
        var items = allData.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

        return new PaginatedList<T>(items, totalCount, pageIndex, pageSize);
    }

    protected void OnAmbientalPageIndexChanged(int newPageIndex)
    {
        ImoveisPaginado = GetPagedData(ListaImoveis, newPageIndex, 10); // Substitua o Valor '10' pelo tamanho de página desejado
    }

    private int currentPageIndex = 1;

    private void NextPage()
    {
        currentPageIndex++;
        OnAmbientalPageIndexChanged(currentPageIndex);
    }

    private void PreviousPage()
    {
        currentPageIndex--;
        OnAmbientalPageIndexChanged(currentPageIndex);
    }
}

