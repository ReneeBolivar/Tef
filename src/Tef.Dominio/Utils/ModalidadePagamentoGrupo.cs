using System.Collections.Generic;

namespace Tef.Dominio.Utils
{
    public static class ModalidadePagamentoGrupo
    {
        private static Dictionary<string, string> Grupos = new Dictionary<string, string>();

        static ModalidadePagamentoGrupo()
        {
            Grupos.Add("00", "CHEQUE");
            Grupos.Add("01", "CARTAO DE DEBITO");
            Grupos.Add("02", "CARTAO DE CREDITO");
            Grupos.Add("03", "CARTAO TIPO VOUCHER");
            Grupos.Add("05", "CARTAO FIDELIDADE");
            Grupos.Add("98", "DINHEIRO");
            Grupos.Add("99", "OUTRO TIPO DE CARTAO");
        }

        public static string ObterModalidadePagamento(string chave)
        {
            Grupos.TryGetValue(chave, out var valor);

            return valor;
        }
    }
}
