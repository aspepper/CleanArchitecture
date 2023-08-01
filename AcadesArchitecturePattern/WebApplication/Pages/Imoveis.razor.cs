using System;
using System.Collections.Generic;
using static WebApplication.Pages.Analise;

namespace WebApplication.Pages
{
    public partial class Imoveis
    {
        public List<Imovel> ListaImoveis { get; set; } = new List<Imovel>();

        public PaginatedList<Imovel> ImoveisPaginado { get; set; }

        protected override void OnInitialized()
        {
            ListaImoveis = new List<Imovel>
        {
            new Imovel {descricao = "Certidão do Ibama",situacao = "positivo",tipo = "positivo",garantia = "positivo",cafir = "positivo"},
            new Imovel {descricao = "Certidão do Ibama",situacao = "negativo",tipo = "negativo",garantia = "negativo",cafir = "negativo"},
            new Imovel {descricao = "Certidão do Ibama",situacao = "atencao",tipo = "",garantia = "",cafir = "atencao"},
            new Imovel {descricao = "Certidão do Ibama",situacao = "positivo",tipo = "positivo",garantia = "positivo",cafir = "positivo"},
            new Imovel {descricao = "Certidão do Ibama",situacao = "negativo",tipo = "negativo",garantia = "negativo",cafir = "negativo"},
            new Imovel {descricao = "Certidão do Ibama",situacao = "atencao",tipo = "",garantia = "",cafir = "atencao"},
            new Imovel {descricao = "Certidão do Ibama",situacao = "positivo",tipo = "positivo",garantia = "positivo",cafir = "positivo"},
            new Imovel {descricao = "Certidão do Ibama",situacao = "negativo",tipo = "negativo",garantia = "negativo",cafir = "negativo"},
            new Imovel {descricao = "Certidão do Ibama",situacao = "atencao",tipo = "",garantia = "",cafir = "atencao"},

            // Outros imóveis...
        };

            // Define a paginação inicial
            ImoveisPaginado = GetPagedData(ListaImoveis, 1, 10);
        }

        public class Imovel
        {
            public string descricao { get; set; }
            public string situacao { get; set; }
            public string tipo { get; set; }
            public string garantia { get; set; }
            public string cafir { get; set; }
        }

        public class PaginatedList<T>
        {
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public int TotalCount { get; set; }
            public List<T> Items { get; set; }

            public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
            {
                PageIndex = pageIndex;
                PageSize = pageSize;
                TotalCount = count;
                Items = items;
            }

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
            ImoveisPaginado = GetPagedData(ListaImoveis, newPageIndex, 10); // Substitua o valor '10' pelo tamanho de página desejado
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
}

