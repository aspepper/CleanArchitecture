using Microsoft.AspNetCore.Components;

namespace SocioAmbientalFinal.Shared.Components
{
    public partial class Botao
    {
        [Parameter]
        public int Number { get; set; }
        [Parameter]
        public bool Separador { get; set; }
        [Parameter]
        public string Icon { get; set; }
        [Parameter]
        public string Text { get; set; }
        [Parameter]
        public string SetClass { get; set; }
        [Parameter]
        public string Img { get; set; }
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public string IdModal { get; set; }

        [Parameter]
        public EventCallback<string> OnClick { get; set; }
        [Parameter]
        public string Page{ get; set;}


        private async Task PassaParametro()
        {
            await OnClick.InvokeAsync(Id);
        }
    }
}
