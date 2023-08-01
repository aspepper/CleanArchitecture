using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace SocioAmbientalFinal.Pages
{
    public partial class PerfilAvaliado
    {

        protected override async Task OnInitializedAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(3));
            await JSRuntime.InvokeVoidAsync("graficoRisco");
        }
        string nomeIdAbrirModal = "AbrirModal";
        bool botaoAtivado = false;

        
        

      
    }
}
