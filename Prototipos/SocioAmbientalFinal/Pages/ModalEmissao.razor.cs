using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace SocioAmbientalFinal.Pages
{
    public partial class ModalEmissao
    {

        public void DesativaBotao()
        {
            JSRunTime.InvokeVoidAsync("eval", "document.querySelectorAll('.botao-ativado').forEach(element => element.classList.remove('botao-ativado'));");
        }
    }
}
