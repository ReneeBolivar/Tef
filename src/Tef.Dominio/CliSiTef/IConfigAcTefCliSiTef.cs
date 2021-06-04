using System;
using System.Collections.Generic;

namespace Tef.Dominio.CliSiTef
{
    public interface IConfigAcTefCliSiTef
    {
        string Host { get; }
        string CodigoLoja { get; }
        string NumeroTerminal { get; }
        string Operador { get; }
        bool Reservado { get; }
        Dictionary<string, string> ParametrosAdicionais { get; }
        string Restricoes { get; }
        string DocumentoFiscal { get; }
        DateTime DataHoraFiscal { get; }
        bool ExibirErroRetorno { get; }
        string PathDll { get; }
    }

    public interface IOperacoesSiTef
    {
        int OperacaoATV { get; }
        int OperacaoADM { get; }
        int OperacaoCRT { get; }
        int OperacaoCHQ { get; }
        int OperacaoCNC { get; }
        int OperacaoPRE { get; }
        int OperacaoReImpressao { get; }
    }
}
