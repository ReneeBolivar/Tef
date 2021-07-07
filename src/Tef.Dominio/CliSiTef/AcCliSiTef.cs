using System;
using Tef.Dominio.Conversores;
using Tef.Dominio.Enums;
using Tef.Dominio.Utils;

namespace Tef.Dominio.CliSiTef
{
    internal class AcCliSiTef
    {
        private readonly IConfigAcCliSiTef _configuracaoCliSiTef;
        private readonly IRequisicaoSitef _requisicaoSitef;

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

        public AcCliSiTef(IConfigAcCliSiTef configAcTefCliSiTef,
                          IRequisicaoSitef requisicaoSitef)
        {
            _configuracaoCliSiTef = configAcTefCliSiTef;
            _requisicaoSitef = requisicaoSitef;
        }

        public void Inicializa()
        {
            if (Inicializado) return;
            
            var @params = _configuracaoCliSiTef.ParametrosAdicionais.Converter();

            var retorno = SitefDllMapper.ConfiguraIntSiTefInterativoEx(_configuracaoCliSiTef.ConfiguracaoTef.IP,
                                                                       _configuracaoCliSiTef.ConfiguracaoTef.CnpjEmpresa,
                                                                       _configuracaoCliSiTef.ConfiguracaoTef.Terminal,
                                                                       _configuracaoCliSiTef.Reservado ? "1" : "0",
                                                                       @params);

            if (ErrosSitef.Nothing == (ErrosSitef)retorno)
                inicializado = true;
            else
                AcTefException.Quando(retorno != 0, ((ErrosSitef)retorno).ObterDescricao());
        }

        void Desinicializa()
        {
            inicializado = false;
        }

        public RespostaAdm Adm(string documentoVinculado = "")
        {
            var ret = _requisicaoSitef.IniciarRequisicao(OperacoesSitef.OperacaoADM, 0, documentoVinculado);
            if ((int)RetornosSitef.Continue == ret)
            {
                _requisicaoSitef.SiTefTransacao = new SiTefTransacao()
                {
                    DocumentoVinculado = documentoVinculado,
                    ValorTransacao = 0m
                };
                SalvarTransacao(_requisicaoSitef.SiTefTransacao);

                _requisicaoSitef.Salvar("0", OperacoesSitef.OperacaoADM.ObterDescricao(), 0);

                _requisicaoSitef.Salvar("2", documentoVinculado, 0);

                _requisicaoSitef.Salvar("2", _requisicaoSitef.SiTefTransacao.Identificador.ToString(), 1);

                _requisicaoSitef.Salvar("4", "0", 0);

                _requisicaoSitef.Salvar("718", $"IP{_configuracaoCliSiTef.ConfiguracaoTef.Terminal}", 0);

                _requisicaoSitef.Salvar("719", _configuracaoCliSiTef.ConfiguracaoTef.Empresa, 0);

                ret = _requisicaoSitef.ContinuarRequisicao();
            }

            if ((int)RetornosSitef.Success == ret)
                Cnf();

            return null;
        }

        public RespostaAtv Atv()
        {
            var ret = _requisicaoSitef.IniciarRequisicao(OperacoesSitef.OperacaoATV);
            if ((int)RetornosSitef.Continue == ret)
                ret = _requisicaoSitef.ContinuarRequisicao();

            if (_configuracaoCliSiTef.ConfiguracaoTef.PinPadVerificar)
                SitefDllMapper.EscreveMensagemPermanentePinPad(_configuracaoCliSiTef.ConfiguracaoTef.PinPadMensagem);

            return null;
        }

        public RespostaCnc Cnc(string rede, string nsu, DateTime transacaoEm, decimal valor, string documentoVinculado = "")
        {
            var parametrosAdc = "";
            var ret = _requisicaoSitef.IniciarRequisicao(OperacoesSitef.OperacaoCNC, 0, "", parametrosAdc, _configuracaoCliSiTef.Operador);
            if((int)RetornosSitef.Continue == ret)
            {
                _requisicaoSitef.SiTefTransacao = new SiTefTransacao()
                {
                    DocumentoVinculado = documentoVinculado,
                    ValorTransacao = 0
                };
                SalvarTransacao(_requisicaoSitef.SiTefTransacao);

                _requisicaoSitef.Salvar("0", OperacoesSitef.OperacaoCNC.ObterDescricao(), 0);

                _requisicaoSitef.Salvar("2", documentoVinculado, 0);

                _requisicaoSitef.Salvar("2", _requisicaoSitef.SiTefTransacao.Identificador.ToString(), 1);

                _requisicaoSitef.Salvar("4", "0", 0);

                _requisicaoSitef.Salvar("718", _configuracaoCliSiTef.ConfiguracaoTef.Terminal, 0);

                _requisicaoSitef.Salvar("719", _configuracaoCliSiTef.ConfiguracaoTef.Empresa, 0);

                ret = _requisicaoSitef.ContinuarRequisicao();
            }

            if ((int)RetornosSitef.Success == ret) 
                Cnf();

            return null;
        }

        public void Cnf(string redeAdquirente, string nsu, string codigoControle, string nomeAutomacao, string versaoAutomacao, string registroCertificacao)
        {
            throw new NotImplementedException();
        }

        public RespostaCrt ConfirmarCrt(AutorizaDfeEventArgs autorizaDfeEventArgs)
        {
            throw new NotImplementedException();
        }

        public RespostaCrt Crt(decimal valor, string documentoVinculado, string operador, bool confirmarManual = false)
        {
            var paramsAdc = string.Empty;
            var ret = _requisicaoSitef.IniciarRequisicao(OperacoesSitef.OperacaoCRT, valor, documentoVinculado, paramsAdc, operador);

            if((int)RetornosSitef.Continue == ret)
            {
                _requisicaoSitef.SiTefTransacao = new SiTefTransacao()
                {
                    DocumentoVinculado = documentoVinculado,
                    ValorTransacao = valor
                };
                SalvarTransacao(_requisicaoSitef.SiTefTransacao);

                _requisicaoSitef.Salvar("0", "CRT", 0);

                _requisicaoSitef.Salvar("2", documentoVinculado, 0);

                _requisicaoSitef.Salvar("2", _requisicaoSitef.SiTefTransacao.Identificador.ToString(), 1);

                _requisicaoSitef.Salvar("3", valor.ToString("N2"), 0);

                _requisicaoSitef.Salvar("4", "0", 0);

                _requisicaoSitef.Salvar("718", $"IP{_configuracaoCliSiTef.ConfiguracaoTef.Terminal}", 0);

                _requisicaoSitef.Salvar("719", _configuracaoCliSiTef.ConfiguracaoTef.CnpjEmpresa, 0);

                ret = _requisicaoSitef.ContinuarRequisicao();
            }
            else if ((int)RetornosSitef.Success == ret)
            {
                _requisicaoSitef.Salvar("9", "0", 0);
                Cnf();
            }

            return null;
        }

        public void Ncn(string redeAdquirente, string codigoControle)
        {
            throw new NotImplementedException();
        }

        public void VerificaSeTefEstaAtivo(TefLinhaLista requisicao)
        {
            throw new NotImplementedException();
        }


        private void SalvarTransacao(SiTefTransacao siTefTransacao)
        {
            throw new NotImplementedException();
        }
    }
}
