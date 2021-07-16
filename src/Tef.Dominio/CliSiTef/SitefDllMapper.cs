using System.Runtime.InteropServices;

namespace Tef.Dominio.CliSiTef
{
    public class SitefDllMapper
    {
        [DllImport("..\\..\\..\\..\\..\\Tef\\src\\Tef.Dominio\\CliSiTef\\Dlls\\CliSiTef32I.dll")]
        static extern int ConfiguraIntSiTefInterativo(string host, string idLoja, string idTerminal, string reservado);

        [DllImport("..\\..\\..\\..\\..\\Tef\\src\\Tef.Dominio\\CliSiTef\\Dlls\\CliSiTef32I.dll")]
        public static extern int ConfiguraIntSiTefInterativoEx(string host, string idLoja, string idTerminal, string reservado, string parametrosAdicionais);

        [DllImport("..\\..\\..\\..\\..\\Tef\\src\\Tef.Dominio\\CliSiTef\\Dlls\\CliSiTef32I.dll")]
        public static extern int IniciaFuncaoSiTefInterativo(int funcao, string valor, string cupomFiscal, string dataFiscal, string horaFiscal, string operador, string parametrosAdicionais);

        [DllImport("..\\..\\..\\..\\..\\Tef\\src\\Tef.Dominio\\CliSiTef\\Dlls\\CliSiTef32I.dll")]
        public static extern int ContinuaFuncaoSiTefInterativo(out int comando, out long tipoCampo, out short tamMinimo, out short tamMaximo, byte[] buffer, int tamBuffer, int continua);

        [DllImport("..\\..\\..\\..\\..\\Tef\\src\\Tef.Dominio\\CliSiTef\\Dlls\\CliSiTef32I.dll")]
        public static extern int FinalizaFuncaoSiTefInterativo(short confirma, string cupomFiscal, string dataFiscal, string horaFiscal, string parametrosAdicionais);

        [DllImport("..\\..\\..\\..\\..\\Tef\\src\\Tef.Dominio\\CliSiTef\\Dlls\\CliSiTef32I.dll")]
        public static extern int ObtemQuantidadeTransacoesPendentes(string dataFiscal, string cupomFiscal);

        [DllImport("..\\..\\..\\..\\..\\Tef\\src\\Tef.Dominio\\CliSiTef\\Dlls\\CliSiTef32I.dll")]
        public static extern int VerificaPresencaPinPad();

        [DllImport("..\\..\\..\\..\\..\\Tef\\src\\Tef.Dominio\\CliSiTef\\Dlls\\CliSiTef32I.dll")]
        public static extern int KeepAlivePinPad();

        [DllImport("..\\..\\..\\..\\..\\Tef\\src\\Tef.Dominio\\CliSiTef\\Dlls\\CliSiTef32I.dll")]
        public static extern int EscreveMensagemPermanentePinPad(string mensagem);

        [DllImport("..\\..\\..\\..\\..\\Tef\\src\\Tef.Dominio\\CliSiTef\\Dlls\\CliSiTef32I.dll")]
        public static extern int LeTrilha3(string mensagem);

        [DllImport("..\\..\\..\\..\\..\\Tef\\src\\Tef.Dominio\\CliSiTef\\Dlls\\CliSiTef32I.dll")]
        public static extern int LeCartaoSeguro(string mensagem);

        [DllImport("..\\..\\..\\..\\..\\Tef\\src\\Tef.Dominio\\CliSiTef\\Dlls\\CliSiTef32I.dll")]
        public static extern int LeSenhaInterativo(string mensagem);

        [DllImport("..\\..\\..\\..\\..\\Tef\\src\\Tef.Dominio\\CliSiTef\\Dlls\\CliSiTef32I.dll")]
        public static extern int LeSenhaDireto(string chaveSeguranca, string senhaCliente);

        [DllImport("..\\..\\..\\..\\..\\Tef\\src\\Tef.Dominio\\CliSiTef\\Dlls\\CliSiTef32I.dll")]
        public static extern int LeSimNaoPinPad(string mensagem);

        [DllImport("..\\..\\..\\..\\..\\Tef\\src\\Tef.Dominio\\CliSiTef\\Dlls\\CliSiTef32I.dll")]
        public static extern int CorrespondenteBancarioSiTefInterativo(string cupomFiscal, string dataFiscal, string horario, string pperador, string parametrosAdicionais);

        [DllImport("..\\..\\..\\..\\..\\Tef\\src\\Tef.Dominio\\CliSiTef\\Dlls\\CliSiTef32I.dll")]
        public static extern int ValidaCampoCodigoEmBarras(string dados, out short tipo);

        [DllImport("..\\..\\..\\..\\..\\Tef\\src\\Tef.Dominio\\CliSiTef\\Dlls\\CliSiTef32I.dll")]
        public static extern int ObtemVersao(out string versaoCliSiTef, out string versaoCliSiTefI);

        [DllImport("..\\..\\..\\..\\..\\Tef\\src\\Tef.Dominio\\CliSiTef\\Dlls\\CliSiTef32I.dll")]
        public static extern int DescarregaMensagens();

        [DllImport("..\\..\\..\\..\\..\\Tef\\src\\Tef.Dominio\\CliSiTef\\Dlls\\CliSiTef32I.dll")]
        public static extern int ObtemInformacoesPinPad(string infoPinPad);
    }
}
