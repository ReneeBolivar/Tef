using System.Collections.Generic;

namespace Tef.Dominio.Utils
{
    public static class ComprovanteTipos
    {
        private static Dictionary<string, string> Comprovantes = new Dictionary<string, string>();

        static ComprovanteTipos()
        {
            Comprovantes.Add("00", "COMPROVANTE_COMPRAS");
            Comprovantes.Add("01", "COMPROVANTE_VOUCHER");
            Comprovantes.Add("02", "COMPROVANTE_CHEQUE");
            Comprovantes.Add("03", "COMPROVANTE_PAGAMENTO");
            Comprovantes.Add("04", "COMPROVANTE_GERENCIAL");
            Comprovantes.Add("05", "COMPROVANTE_CB");
            Comprovantes.Add("06", "COMPROVANTE_RECARGA_CELULAR");
            Comprovantes.Add("07", "COMPROVANTE_RECARGA_BONUS");
            Comprovantes.Add("08", "COMPROVANTE_RECARGA_PRESENTE");
            Comprovantes.Add("09", "COMPROVANTE_RECARGA_SP_TRANS");
            Comprovantes.Add("10", "COMPROVANTE_MEDICAMENTOS");
        }

        public static string ObterComprovante(string chave)
        {
            Comprovantes.TryGetValue(chave, out var comprovante);
            return $"{chave}|{comprovante}";
        }
    }
}
