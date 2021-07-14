using System;
using System.Runtime.InteropServices;

namespace Tef.Dominio.CliSiTef
{
    public class SitefDllMapper
    {
        [DllImport("C:\\Users\\Renee\\source\\repos\\ZFront_Prod\\ZFront_Prod\\ZFront.TEF\\CliSiTef32I.dll")]
        public static extern int ConfiguraIntSiTefInterativo(string host, string idLoja, string idTerminal, string reservado);

        [DllImport("C:\\Users\\Renee\\source\\repos\\ZFront_Prod\\ZFront_Prod\\ZFront.TEF\\CliSiTef32I.dll")]
        public static extern int ConfiguraIntSiTefInterativoEx(string host, string idLoja, string idTerminal, string reservado, string parametrosAdicionais);

        [DllImport("C:\\Users\\Renee\\source\\repos\\ZFront_Prod\\ZFront_Prod\\ZFront.TEF\\CliSiTef32I.dll")]
        public static extern int IniciaFuncaoSiTefInterativo(int funcao, string valor, string cupomFiscal, string dataFiscal, string horaFiscal, string operador, string parametrosAdicionais);

        [DllImport("C:\\Users\\Renee\\source\\repos\\ZFront_Prod\\ZFront_Prod\\ZFront.TEF\\CliSiTef32I.dll")]
        public static extern int ContinuaFuncaoSiTefInterativo(out int comando, out long tipoCampo, out short tamMinimo, out short tamMaximo, byte[] buffer, int tamBuffer, int continua);

        [DllImport("C:\\Users\\Renee\\source\\repos\\ZFront_Prod\\ZFront_Prod\\ZFront.TEF\\CliSiTef32I.dll")]
        public static extern int FinalizaFuncaoSiTefInterativo(short confirma, string cupomFiscal, string dataFiscal, string horaFiscal, string parametrosAdicionais);

        [DllImport("C:\\Users\\Renee\\source\\repos\\ZFront_Prod\\ZFront_Prod\\ZFront.TEF\\CliSiTef32I.dll")]
        public static extern int ObtemQuantidadeTransacoesPendentes(string dataFiscal, string cupomFiscal);

        [DllImport("C:\\Users\\Renee\\source\\repos\\ZFront_Prod\\ZFront_Prod\\ZFront.TEF\\CliSiTef32I.dll")]
        public static extern int VerificaPresencaPinPad();

        [DllImport("C:\\Users\\Renee\\source\\repos\\ZFront_Prod\\ZFront_Prod\\ZFront.TEF\\CliSiTef32I.dll")]
        public static extern int KeepAlivePinPad();

        [DllImport("C:\\Users\\Renee\\source\\repos\\ZFront_Prod\\ZFront_Prod\\ZFront.TEF\\CliSiTef32I.dll")]
        public static extern int EscreveMensagemPermanentePinPad(string mensagem);

        [DllImport("C:\\Users\\Renee\\source\\repos\\ZFront_Prod\\ZFront_Prod\\ZFront.TEF\\CliSiTef32I.dll")]
        public static extern int LeTrilha3(string mensagem);

        [DllImport("C:\\Users\\Renee\\source\\repos\\ZFront_Prod\\ZFront_Prod\\ZFront.TEF\\CliSiTef32I.dll")]
        public static extern int LeCartaoSeguro(string mensagem);

        [DllImport("C:\\Users\\Renee\\source\\repos\\ZFront_Prod\\ZFront_Prod\\ZFront.TEF\\CliSiTef32I.dll")]
        public static extern int LeSenhaInterativo(string mensagem);

        [DllImport("C:\\Users\\Renee\\source\\repos\\ZFront_Prod\\ZFront_Prod\\ZFront.TEF\\CliSiTef32I.dll")]
        public static extern int LeSenhaDireto(string chaveSeguranca, string senhaCliente);

        [DllImport("C:\\Users\\Renee\\source\\repos\\ZFront_Prod\\ZFront_Prod\\ZFront.TEF\\CliSiTef32I.dll")]
        public static extern int LeSimNaoPinPad(string mensagem);

        [DllImport("C:\\Users\\Renee\\source\\repos\\ZFront_Prod\\ZFront_Prod\\ZFront.TEF\\CliSiTef32I.dll")]
        public static extern int CorrespondenteBancarioSiTefInterativo(string cupomFiscal, string dataFiscal, string horario, string pperador, string parametrosAdicionais);

        [DllImport("C:\\Users\\Renee\\source\\repos\\ZFront_Prod\\ZFront_Prod\\ZFront.TEF\\CliSiTef32I.dll")]
        public static extern int ValidaCampoCodigoEmBarras(string dados, out short tipo);

        [DllImport("C:\\Users\\Renee\\source\\repos\\ZFront_Prod\\ZFront_Prod\\ZFront.TEF\\CliSiTef32I.dll")]
        public static extern int ObtemVersao(out string versaoCliSiTef, out string versaoCliSiTefI);

        [DllImport("C:\\Users\\Renee\\source\\repos\\ZFront_Prod\\ZFront_Prod\\ZFront.TEF\\CliSiTef32I.dll")]
        public static extern int DescarregaMensagens();

        [DllImport("C:\\Users\\Renee\\source\\repos\\ZFront_Prod\\ZFront_Prod\\ZFront.TEF\\CliSiTef32I.dll")]
        public static extern int ObtemInformacoesPinPad(string infoPinPad);
    }
}
