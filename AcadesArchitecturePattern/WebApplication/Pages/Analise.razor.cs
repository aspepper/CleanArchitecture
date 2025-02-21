using Microsoft.AspNetCore.Components;

namespace WebApplication.Pages;

public partial class Analise : ComponentBase
{
    public List<Risco> Ambiental { get; set; } = [];
    public List<Climatico> Transicao { get; set; } = [];
    public List<Parecer> Cliente { get; set; } = [];
    public List<Justica> Fisico { get; set; } = [];
    public List<AnaliseItem> Social { get; set; } = [];
    public PaginatedList<Risco>? AmbientalPaginado { get; set; }
    public PaginatedList<Climatico>? TransicaoPaginado { get; set; }

    protected override void OnInitialized()
    {
        Ambiental =
        [
            new Risco { Descricao = "Risco 1", RiscoTipo = "Analise", Justificacao = "" },
            new Risco { Descricao = "Risco 2", RiscoTipo = "positivo", Justificacao = "" },
            new Risco { Descricao = "Risco 3", RiscoTipo = "negativo", Justificacao = "" }
        ];

        Transicao =
        [
            new Climatico { Descricao = "Climático 1", RiscoTipo = "positivo", Justificacao = "" },
            new Climatico { Descricao = "Climático 2", RiscoTipo = "Analise", Justificacao = "" },
            new Climatico { Descricao = "Climático 3", RiscoTipo = "negativo", Justificacao = "" }
        ];

        Cliente =
        [
            new Parecer { Descricao = "Descrição 1", Usuario = "Victor da Cruz", Identificacao = "", DataHora = DateTime.Now, Tipo = "", Parece = "" },
            new Parecer { Descricao = "Descrição 2", Usuario = "Joselino Macabeu", Identificacao = "", DataHora = DateTime.Now, Tipo = "", Parece = "" },
            new Parecer { Descricao = "Descrição 3", Usuario = "Bob Marley Silva", Identificacao = "", DataHora = DateTime.Now, Tipo = "", Parece = "" }
        ];

        Fisico =
        [
            new Justica { Descricao = "Risco 1", RiscoTipo = "Analise", Justificacao = "" },
            new Justica { Descricao = "Risco 2", RiscoTipo = "negativo", Justificacao = "" },
            new Justica { Descricao = "Risco 3", RiscoTipo = "positivo", Justificacao = "" }
        ];

        Social =
        [
            new AnaliseItem { Descricao = "Risco 1", RiscoTipo = "negativo", Justificacao = "" },
            new AnaliseItem { Descricao = "Risco 2", RiscoTipo = "positivo", Justificacao = "" },
            new AnaliseItem { Descricao = "Risco 3", RiscoTipo = "Analise", Justificacao = "" }
        ];

        // Definir valores iniciais para a paginação
        int pageIndex = 1;
        int pageSize = 10;

        // Paginar a lista "Ambiental"
        AmbientalPaginado = GetPagedData(Ambiental, pageIndex, pageSize);

        // Paginar a lista "Transicao"
        TransicaoPaginado = GetPagedData(Transicao, pageIndex, pageSize);
    }

    public class Risco
    {
        public string Descricao { get; set; } = string.Empty;
        public string RiscoTipo { get; set; } = string.Empty;
        public string Justificacao { get; set; } = string.Empty;
    }

    public class Climatico
    {
        public string Descricao { get; set; } = string.Empty;
        public string RiscoTipo { get; set; } = string.Empty;
        public string Justificacao { get; set; } = string.Empty;
    }

    public class Parecer
    {
        public string Descricao { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
        public string Identificacao { get; set; } = string.Empty;
        public DateTime DataHora { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Parece { get; set; } = string.Empty;
    }

    public class Justica
    {
        public string Descricao { get; set; } = string.Empty;
        public string RiscoTipo { get; set; } = string.Empty;
        public string Justificacao { get; set; } = string.Empty;
    }

    public class AnaliseItem
    {
        public string Descricao { get; set; } = string.Empty;
        public string RiscoTipo { get; set; } = string.Empty;
        public string Justificacao { get; set; } = string.Empty;
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
        AmbientalPaginado = GetPagedData(Ambiental, newPageIndex, AmbientalPaginado.PageSize);
    }

    protected void OnTransicaoPageIndexChanged(int newPageIndex)
    {
        TransicaoPaginado = GetPagedData(Transicao, newPageIndex, TransicaoPaginado.PageSize);
    }
}
