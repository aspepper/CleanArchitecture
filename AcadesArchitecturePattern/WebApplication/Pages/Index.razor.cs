using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace WebApplication.Pages;

public partial class Index
{

    bool PerfilAvaliadoAtivo = true;
    bool AgravanteMitigadoresAtivo = false;
    bool EmissaoGEEAtivo = false;
    bool AnaliseParecerAtivo = false;
    bool OperacoesAtivo = false;
    bool ImoveisAtivo = false;
    bool AnexosAtivo = false;
    bool QuadroResumoAtivo = false;

    string classDinamica1 = "line-ativa";
    string classDinamica2 = "line";
    string classDinamica3 = "line";
    string classDinamica4 = "line";
    string classDinamica5 = "line";
    string classDinamica6 = "line";
    string nomeIdClips = "Clips";
    string nomeIdSeta = "Seta";

    string mensagem = "Nenhuma opção selecionada";

    void AtivarPerfilAvaliado()
    {
        PerfilAvaliadoAtivo = true;
        AgravanteMitigadoresAtivo = false;
        EmissaoGEEAtivo = false;
        AnaliseParecerAtivo = false;
        OperacoesAtivo = false;
        ImoveisAtivo = false;
        AnexosAtivo = false;
        QuadroResumoAtivo = false;

        mensagem = "Perfil do Avaliado";

        classDinamica1 = PerfilAvaliadoAtivo == true ? "line-ativa" : "line";
        classDinamica2 = AgravanteMitigadoresAtivo == true ? "line-ativa" : "line";
        classDinamica3 = EmissaoGEEAtivo == true ? "line-ativa" : "line";
        classDinamica4 = AnaliseParecerAtivo == true ? "line-ativa" : "line";
        classDinamica5 = OperacoesAtivo == true ? "line-ativa" : "line";
        classDinamica6 = ImoveisAtivo == true ? "line-ativa" : "line";
        nomeIdClips = AnexosAtivo == true ? "line-ativa" : "line";
        nomeIdSeta = QuadroResumoAtivo == true ? "line-ativa" : "line";
    }

    void AtivarAgravatesMitigadores()
    {
        PerfilAvaliadoAtivo = false;
        AgravanteMitigadoresAtivo = true;
        EmissaoGEEAtivo = false;
        AnaliseParecerAtivo = false;
        OperacoesAtivo = false;
        ImoveisAtivo = false;
        AnexosAtivo = false;
        QuadroResumoAtivo = false;

        mensagem = "Agravantes e Mitigadores ";

        classDinamica1 = PerfilAvaliadoAtivo == true ? "line-ativa" : "line";
        classDinamica2 = AgravanteMitigadoresAtivo == true ? "line-ativa" : "line";
        classDinamica3 = EmissaoGEEAtivo == true ? "line-ativa" : "line";
        classDinamica4 = AnaliseParecerAtivo == true ? "line-ativa" : "line";
        classDinamica5 = OperacoesAtivo == true ? "line-ativa" : "line";
        classDinamica6 = ImoveisAtivo == true ? "line-ativa" : "line";
        nomeIdClips = AnexosAtivo == true ? "line-ativa" : "line";
        nomeIdSeta = QuadroResumoAtivo == true ? "line-ativa" : "line";
    }

    void AtivarEmissaoGEE()
    {
        PerfilAvaliadoAtivo = false;
        AgravanteMitigadoresAtivo = false;
        EmissaoGEEAtivo = true;
        AnaliseParecerAtivo = false;
        OperacoesAtivo = false;
        ImoveisAtivo = false;
        AnexosAtivo = false;
        QuadroResumoAtivo = false;

        mensagem = "Emissão de GEE";

        classDinamica1 = PerfilAvaliadoAtivo == true ? "line-ativa" : "line";
        classDinamica2 = AgravanteMitigadoresAtivo == true ? "line-ativa" : "line";
        classDinamica3 = EmissaoGEEAtivo == true ? "line-ativa" : "line";
        classDinamica4 = AnaliseParecerAtivo == true ? "line-ativa" : "line";
        classDinamica5 = OperacoesAtivo == true ? "line-ativa" : "line";
        classDinamica6 = ImoveisAtivo == true ? "line-ativa" : "line";
        nomeIdClips = AnexosAtivo == true ? "line-ativa" : "line";
        nomeIdSeta = QuadroResumoAtivo == true ? "line-ativa" : "line";
    }
    void AtivarAnaliseParecer()
    {
        PerfilAvaliadoAtivo = false;
        AgravanteMitigadoresAtivo = false;
        EmissaoGEEAtivo = false;
        AnaliseParecerAtivo = true;
        OperacoesAtivo = false;
        ImoveisAtivo = false;
        AnexosAtivo = false;
        QuadroResumoAtivo = false;

        mensagem = "Análise & Parecer";

        classDinamica1 = PerfilAvaliadoAtivo == true ? "line-ativa" : "line";
        classDinamica2 = AgravanteMitigadoresAtivo == true ? "line-ativa" : "line";
        classDinamica3 = EmissaoGEEAtivo == true ? "line-ativa" : "line";
        classDinamica4 = AnaliseParecerAtivo == true ? "line-ativa" : "line";
        classDinamica5 = OperacoesAtivo == true ? "line-ativa" : "line";
        classDinamica6 = ImoveisAtivo == true ? "line-ativa" : "line";
        nomeIdClips = AnexosAtivo == true ? "line-ativa" : "line";
        nomeIdSeta = QuadroResumoAtivo == true ? "line-ativa" : "line";
    }
    void AtivarOperacoes()
    {
        PerfilAvaliadoAtivo = false;
        AgravanteMitigadoresAtivo = false;
        EmissaoGEEAtivo = false;
        AnaliseParecerAtivo = false;
        OperacoesAtivo = true;
        ImoveisAtivo = false;
        AnexosAtivo = false;
        QuadroResumoAtivo = false;

        mensagem = "Operações";

        classDinamica1 = PerfilAvaliadoAtivo == true ? "line-ativa" : "line";
        classDinamica2 = AgravanteMitigadoresAtivo == true ? "line-ativa" : "line";
        classDinamica3 = EmissaoGEEAtivo == true ? "line-ativa" : "line";
        classDinamica4 = AnaliseParecerAtivo == true ? "line-ativa" : "line";
        classDinamica5 = OperacoesAtivo == true ? "line-ativa" : "line";
        classDinamica6 = ImoveisAtivo == true ? "line-ativa" : "line";
        nomeIdClips = AnexosAtivo == true ? "line-ativa" : "line";
        nomeIdSeta = QuadroResumoAtivo == true ? "line-ativa" : "line";
    }
    void AtivarImoveis()
    {
        PerfilAvaliadoAtivo = false;
        AgravanteMitigadoresAtivo = false;
        EmissaoGEEAtivo = false;
        AnaliseParecerAtivo = false;
        OperacoesAtivo = false;
        ImoveisAtivo = true;
        AnexosAtivo = false;
        QuadroResumoAtivo = false;

        mensagem = "Imoveis";

        classDinamica1 = PerfilAvaliadoAtivo == true ? "line-ativa" : "line";
        classDinamica2 = AgravanteMitigadoresAtivo == true ? "line-ativa" : "line";
        classDinamica3 = EmissaoGEEAtivo == true ? "line-ativa" : "line";
        classDinamica4 = AnaliseParecerAtivo == true ? "line-ativa" : "line";
        classDinamica5 = OperacoesAtivo == true ? "line-ativa" : "line";
        classDinamica6 = ImoveisAtivo == true ? "line-ativa" : "line";
        nomeIdClips = AnexosAtivo == true ? "line-ativa" : "line";
        nomeIdSeta = QuadroResumoAtivo == true ? "line-ativa" : "line";
    }

    void AtivarClips()
    {
        PerfilAvaliadoAtivo = false;
        AgravanteMitigadoresAtivo = false;
        EmissaoGEEAtivo = false;
        AnaliseParecerAtivo = false;
        OperacoesAtivo = false;
        ImoveisAtivo = false;
        AnexosAtivo = true;
        QuadroResumoAtivo = false;

        mensagem = "Clips";

        classDinamica1 = PerfilAvaliadoAtivo == true ? "line-ativa" : "line";
        classDinamica2 = AgravanteMitigadoresAtivo == true ? "line-ativa" : "line";
        classDinamica3 = EmissaoGEEAtivo == true ? "line-ativa" : "line";
        classDinamica4 = AnaliseParecerAtivo == true ? "line-ativa" : "line";
        classDinamica5 = OperacoesAtivo == true ? "line-ativa" : "line";
        classDinamica6 = ImoveisAtivo == true ? "line-ativa" : "line";
        nomeIdClips = AnexosAtivo == true ? "line-ativa" : "line";
        nomeIdSeta = QuadroResumoAtivo == true ? "line-ativa" : "line";
    }

    void AtivarSetas()
    {
        PerfilAvaliadoAtivo = false;
        AgravanteMitigadoresAtivo = false;
        EmissaoGEEAtivo = false;
        AnaliseParecerAtivo = false;
        OperacoesAtivo = false;
        ImoveisAtivo = false;
        AnexosAtivo = false;
        QuadroResumoAtivo = true;

        mensagem = "Setas";

        classDinamica1 = PerfilAvaliadoAtivo == true ? "line-ativa" : "line";
        classDinamica2 = AgravanteMitigadoresAtivo == true ? "line-ativa" : "line";
        classDinamica3 = EmissaoGEEAtivo == true ? "line-ativa" : "line";
        classDinamica4 = AnaliseParecerAtivo == true ? "line-ativa" : "line";
        classDinamica5 = OperacoesAtivo == true ? "line-ativa" : "line";
        classDinamica6 = ImoveisAtivo == true ? "line-ativa" : "line";
        nomeIdClips = AnexosAtivo == true ? "line-ativa" : "line";
        nomeIdSeta = QuadroResumoAtivo == true ? "line-ativa" : "line";
    }

    bool botaoAtivado = false;

    [Inject]
    public IJSRuntime JSRuntime { get; set; }

    async Task AlternarBotao(string id)
    {
        Console.WriteLine(id);
        await JSRuntime.InvokeVoidAsync("eval", $"document.getElementById('{id}').classList.add('botao-ativado')");
    }
}


