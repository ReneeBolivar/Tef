using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tef.Dominio.Conversores;
using Tef.Dominio.Enums;
using Tef.Dominio.Utils;

namespace Tef.Dominio.CliSiTef
{
    internal class AcCliSiTef : ITef
    {
        private readonly IConfigAcTefCliSiTef _configuracaoCliSiTef;
        private readonly IOperacoesSiTef _operacoesSitef;

        bool inicializado;
        bool Inicializado
        {
            get { return inicializado; }
            set
            {
                if (value)
                    Inicializa();
                else
                    Desinicializa();
            }
        }

        public AcCliSiTef(IConfigAcTefCliSiTef configAcTefCliSiTef, IOperacoesSiTef operacoesSiTef)
        {
            _configuracaoCliSiTef = configAcTefCliSiTef;
            _operacoesSitef = operacoesSiTef;
        }

        public void Inicializa()
        {
            if (Inicializado) return;
            inicializado = true;

            var @params = _configuracaoCliSiTef.ParametrosAdicionais.Converter();

            var retorno = SitefDllMapper.ConfiguraIntSiTefInterativoEx(_configuracaoCliSiTef.Host,
                                                                       _configuracaoCliSiTef.CodigoLoja,
                                                                       _configuracaoCliSiTef.NumeroTerminal,
                                                                       _configuracaoCliSiTef.Reservado ? "1" : "0",
                                                                       @params);

            AcTefException.Quando(retorno > 0, ((ErrosSitef)retorno).ObterDescricao());
        }

        void Desinicializa()
        {
            inicializado = false;
        }


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
