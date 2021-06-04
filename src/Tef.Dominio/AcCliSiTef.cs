using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tef.Dominio
{
    internal class AcCliSiTef : ITef
    {


        public RespostaAdm Adm()
        {
            throw new NotImplementedException();
        }

        public RespostaAtv Atv()
        {
            throw new NotImplementedException();
        }

        public RespostaCnc Cnc(string rede, string nsu, DateTime transacaoEm, decimal valor)
        {
            throw new NotImplementedException();
        }

        public void Cnf(string redeAdquirente, string nsu, string codigoControle, string nomeAutomacao, string versaoAutomacao, string registroCertificacao)
        {
            throw new NotImplementedException();
        }

        public RespostaCrt ConfirmarCrt(AutorizaDfeEventArgs autorizaDfeEventArgs)
        {
            throw new NotImplementedException();
        }

        public RespostaCrt Crt(decimal valor, string documentoVinculado, bool confirmarManual = false)
        {
            throw new NotImplementedException();
        }

        public void Inicializa()
        {
            throw new NotImplementedException();
        }

        public void Ncn(string redeAdquirente, string codigoControle)
        {
            throw new NotImplementedException();
        }

        public void VerificaSeTefEstaAtivo(TefLinhaLista requisicao)
        {
            throw new NotImplementedException();
        }
    }
}
