using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Tef.Dominio.CliSiTef
{
    public interface IConfigAcCliSiTef
    {
        string Operador { get; }
        bool Reservado { get; }
        Dictionary<string, string> ParametrosAdicionais { get; }
        string Restricoes { get; }
        string DocumentoFiscal { get; }
        DateTime DataHoraFiscal { get; }
        bool ExibirErroRetorno { get; }
        bool RetornaQRCode { get; }
        IConfigTef ConfiguracaoTef { get; }
    }

    public interface IConfigTef
    {
        string PathArquivos { get; set; }
        string IP { get; set; }
        string Empresa { get; set; }
        string CnpjEmpresa { get; set; }
        string Terminal { get; set; }
        string CnpjSoftwareHouse { get; set; }
        string PinPadPorta { get; set; }
        string PinPadMensagem { get; set; }
        bool PinPadVerificar { get; set; }
    }

    public enum OperacoesTEF
    {
        [Description("ATV")]
        OperacaoATV = 111,

        [Description("ADM")]
        OperacaoADM = 110,

        [Description("CRT")]
        OperacaoCRT = 0,

        [Description("CHQ")]
        OperacaoCHQ = 1,

        [Description("CNC")]
        OperacaoCNC = 200,

        [Description("ReImpressao")]
        OperacaoReImpressao = 112,

        [Description("CEL")]
        RecargaCelular = 300
    }
}
