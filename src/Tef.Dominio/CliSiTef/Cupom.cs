using System.Collections.Generic;

namespace Tef.Dominio.CliSiTef
{
    public class Cupom
    {
        public string TipoOperacao { get; set; }
        public string DocumentoVinculado { get; set; }
        public decimal ValorTotal { get; set; }
        
        private List<SiTefTransacao> transacaosTef;
        public List<SiTefTransacao> TransacoesTEF
        {
            get 
            {
                if (transacaosTef == null)
                    transacaosTef = new List<SiTefTransacao>();

                return transacaosTef; 
            }
        }

    }
}
