﻿using Tef.Dominio.CliSiTef.Args;

namespace Tef.Dominio.CliSiTef
{
    public delegate void NotificarMensagemSitef(string mensagem, int timeoutMilisegundos);
    public delegate CliSitefCamposEventArgs SolicitarCamposSitef(CliSitefCamposEventArgs cliSitefCamposEventArgs);
    public delegate void AdicionarRetorno(TefLinha tefLinha);
    public delegate void ComprovanteGerado(string cupom);

    public interface IRequisicaoSitef
    {
        NotificarMensagemSitef AoNotificarMensagemSitef { get; set; }
        SolicitarCamposSitef AoSolicitarCamposSitef { get; set; }
        AdicionarRetorno AoAdicionarRetorno { get; set; }
        ComprovanteGerado AoGerarComprovante { get; set; }

        SiTefTransacao SiTefTransacao { get; set; }

        int IniciarRequisicao(OperacoesTEF funcao, decimal valor = 0, string documento = "", string paramAdicionais = "", string operador = "");
        int ContinuarRequisicao();
        void Salvar(string chave, string valor, int indice);
    }
}
