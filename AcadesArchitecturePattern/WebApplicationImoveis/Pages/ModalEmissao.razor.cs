using Microsoft.JSInterop;

namespace WebApplicationImoveis.Pages;

public partial class ModalEmissao
{

    public void DesativaBotao()
    {
        _ = JSRunTime.InvokeVoidAsync("eval", "document.querySelectorAll('.botao-ativado').forEach(element => element.classList.remove('botao-ativado'));");
    }
}
