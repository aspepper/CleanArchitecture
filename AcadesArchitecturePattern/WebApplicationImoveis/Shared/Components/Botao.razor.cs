using Microsoft.AspNetCore.Components;

namespace WebApplicationImoveis.Shared.Components;

public partial class Botao
{
    [Parameter]
    public int Number { get; set; }
    [Parameter]
    public bool Separador { get; set; }
    [Parameter]
    public string Icon { get; set; } = string.Empty;
    [Parameter]
    public string Text { get; set; } = string.Empty;
    [Parameter]
    public string SetClass { get; set; } = string.Empty;
    [Parameter]
    public string Img { get; set; } = string.Empty;
    [Parameter]
    public string Id { get; set; } = string.Empty;
    [Parameter]
    public string IdModal { get; set; } = string.Empty;

    [Parameter]
    public EventCallback<string> OnClick { get; set; }
    [Parameter]
    public string Page { get; set; } = string.Empty;


    private async Task PassaParametro()
    {
        await OnClick.InvokeAsync(Id);
    }
}
