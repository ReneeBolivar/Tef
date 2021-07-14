using System;
using System.Collections.Generic;

namespace Tef.Dominio.CliSiTef
{
    public class SiTefTransacao
    {
        public SiTefTransacao()
        {
            Identificador = Guid.NewGuid();
        }

        public Guid Identificador { get; set; }
        public string DocumentoVinculado { get; set; }
        public decimal ValorTransacao { get; set; }
        
        private List<TefRetorno> retornos;
        public List<TefRetorno> Retornos
        {
            get 
            {
                if (retornos == null) 
                    retornos = new List<TefRetorno>();

                return retornos; 
            }
        }
    }
}
