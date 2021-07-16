using System;
using System.IO;
using System.Linq;
using System.Threading;
using Tef.Dominio.Conversores;
using Tef.Dominio.Enums;
using Tef.Dominio.Interfaces;
using Tef.Dominio.Utils;

namespace Tef.Dominio.CliSiTef
{
    public class AcCliSiTef : ISiTefRequisicao
    {
        private readonly IConfigAcCliSiTef _configuracaoCliSiTef;
        private readonly IRequisicaoSitef _requisicaoSitef;

        private Cupom CupomTef;

        public AcCliSiTef(IConfigAcCliSiTef configAcTefCliSiTef,
                          IRequisicaoSitef requisicaoSitef)
        {
            _configuracaoCliSiTef = configAcTefCliSiTef;
            _requisicaoSitef = requisicaoSitef;
        }

        #region Inicialização de tef

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

        public void Inicializa()
        {
            if (Inicializado) return;
            
            var @params = _configuracaoCliSiTef.ParametrosAdicionais.Converter();

            var retorno = SitefDllMapper.ConfiguraIntSiTefInterativoEx(_configuracaoCliSiTef.ConfiguracaoTef.IP,
                                                                       "00000000",
                                                                       $"IP{_configuracaoCliSiTef.ConfiguracaoTef.Terminal}",
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

        private void ChecaTefInicializado()
        {
            if (!Inicializado)
                Inicializa();
        }

        #endregion

        #region Operações

        public int Adm(string documentoVinculado = "")
        {
            ChecaTefInicializado();

            CupomTef = new Cupom
            {
                TipoOperacao = OperacoesTEF.OperacaoADM.ObterDescricao(),
                DocumentoVinculado = documentoVinculado,
                ValorTotal = 0m
            };

            var ret = _requisicaoSitef.IniciarRequisicao(OperacoesTEF.OperacaoADM, 0, documentoVinculado);
            if (RetornosSitef.Continue == (RetornosSitef)ret)
            {
                _requisicaoSitef.SiTefTransacao = new SiTefTransacao()
                {
                    DocumentoVinculado = documentoVinculado,
                    ValorTransacao = 0m
                };
                SalvarTransacao(_requisicaoSitef.SiTefTransacao);

                _requisicaoSitef.Salvar("0", OperacoesTEF.OperacaoADM.ObterDescricao(), 0);

                _requisicaoSitef.Salvar("2", documentoVinculado, 0);

                _requisicaoSitef.Salvar("2", _requisicaoSitef.SiTefTransacao.Identificador.ToString(), 1);

                _requisicaoSitef.Salvar("4", "0", 0);

                _requisicaoSitef.Salvar("718", $"IP{_configuracaoCliSiTef.ConfiguracaoTef.Terminal}", 0);

                _requisicaoSitef.Salvar("719", _configuracaoCliSiTef.ConfiguracaoTef.Empresa, 0);

                ret = _requisicaoSitef.ContinuarRequisicao();
            }

            if (RetornosSitef.Success == (RetornosSitef)ret)
                Cnf(documentoVinculado);

            return ret;
        }

        public int Atv()
        {
            ChecaTefInicializado();

            var ret = _requisicaoSitef.IniciarRequisicao(OperacoesTEF.OperacaoATV);
            if (RetornosSitef.Continue == (RetornosSitef)ret)
                ret = _requisicaoSitef.ContinuarRequisicao();

            if (_configuracaoCliSiTef.ConfiguracaoTef.PinPadVerificar)
                SitefDllMapper.EscreveMensagemPermanentePinPad(_configuracaoCliSiTef.ConfiguracaoTef.PinPadMensagem);

            return ret;
        }

        public int Cnc(OperacoesTEF operacao, string documentoVinculado = "")
        {
            ChecaTefInicializado();

            CupomTef = new Cupom
            {
                TipoOperacao = operacao.ObterDescricao(),
                DocumentoVinculado = documentoVinculado,
                ValorTotal = 0m
            };

            var parametrosAdc = "";
            var ret = _requisicaoSitef.IniciarRequisicao(operacao, 0, "", parametrosAdc, _configuracaoCliSiTef.Operador);
            if(RetornosSitef.Continue == (RetornosSitef)ret)
            {
                _requisicaoSitef.SiTefTransacao = new SiTefTransacao()
                {
                    DocumentoVinculado = documentoVinculado,
                    ValorTransacao = 0
                };
                SalvarTransacao(_requisicaoSitef.SiTefTransacao);

                _requisicaoSitef.Salvar("0", operacao.ObterDescricao(), 0);

                _requisicaoSitef.Salvar("2", documentoVinculado, 0);

                _requisicaoSitef.Salvar("2", _requisicaoSitef.SiTefTransacao.Identificador.ToString(), 1);

                _requisicaoSitef.Salvar("4", "0", 0);

                _requisicaoSitef.Salvar("718", _configuracaoCliSiTef.ConfiguracaoTef.Terminal, 0);

                _requisicaoSitef.Salvar("719", _configuracaoCliSiTef.ConfiguracaoTef.Empresa, 0);

                ret = _requisicaoSitef.ContinuarRequisicao();
            }

            if (RetornosSitef.Success == (RetornosSitef)ret) 
                Cnf(documentoVinculado);

            return ret;
        }

        public int Cnf(string documentoVinculado = "", bool gerarArquivo = true)
        {
            ChecaTefInicializado();

            var ret = FinalizarOperacao(1, documentoVinculado);

            if (RetornosSitef.Success != (RetornosSitef)ret)
                throw new Exception("Pagamento não confirmado");

            _requisicaoSitef.Salvar("729", "1", 0);
            _requisicaoSitef.Salvar("999", "0", 0);

            if (gerarArquivo)
                ExportarTransacaoParaArquivo();

            if (_configuracaoCliSiTef.ConfiguracaoTef.PinPadVerificar)
                SitefDllMapper.EscreveMensagemPermanentePinPad(_configuracaoCliSiTef.ConfiguracaoTef.PinPadMensagem);

            return ret;
        }

        public int Crt(decimal valor, string documentoVinculado, string operador, bool confirmarAuto = true)
        {
            ChecaTefInicializado();

            CupomTef = new Cupom
            {
                TipoOperacao = OperacoesTEF.OperacaoCRT.ObterDescricao(),
                DocumentoVinculado = documentoVinculado,
                ValorTotal = valor
            };

            var paramsAdc = string.Empty;
            var ret = _requisicaoSitef.IniciarRequisicao(OperacoesTEF.OperacaoCRT, valor, documentoVinculado, paramsAdc, operador);

            if(RetornosSitef.Continue == (RetornosSitef)ret)
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
            
            if (RetornosSitef.Success == (RetornosSitef)ret)
            {
                _requisicaoSitef.Salvar("9", "0", 0);

                if (confirmarAuto)
                {
                    #if DEBUG
                    //Sem esse atraso a transação mesmo autorizada não fica registrada no relatório
                    Thread.Sleep(1000);
                    #endif

                    ret = Cnf(documentoVinculado);
                }
            }

            return ret;
        }

        public int Cel(string documentoVinculado = "")
        {
            ChecaTefInicializado();

            CupomTef = new Cupom
            {
                TipoOperacao = OperacoesTEF.RecargaCelular.ObterDescricao(),
                DocumentoVinculado = documentoVinculado,
                ValorTotal = 0m
            };

            var ret = _requisicaoSitef.IniciarRequisicao(OperacoesTEF.RecargaCelular, 0m, documentoVinculado);

            if(RetornosSitef.Continue == (RetornosSitef)ret)
            {
                _requisicaoSitef.SiTefTransacao = new SiTefTransacao()
                {
                    DocumentoVinculado = documentoVinculado,
                    ValorTransacao = 0m
                };
                SalvarTransacao(_requisicaoSitef.SiTefTransacao);

                _requisicaoSitef.Salvar("0", OperacoesTEF.RecargaCelular.ObterDescricao(), 0);

                _requisicaoSitef.Salvar("2", documentoVinculado, 0);
                
                _requisicaoSitef.Salvar("2", _requisicaoSitef.SiTefTransacao.Identificador.ToString(), 1);

                _requisicaoSitef.Salvar("4", "0", 0);
                
                _requisicaoSitef.Salvar("718", $"IP{_configuracaoCliSiTef.ConfiguracaoTef.Terminal}", 0);

                _requisicaoSitef.Salvar("719", $"IP{_configuracaoCliSiTef.ConfiguracaoTef.Empresa}", 0);

                ret = _requisicaoSitef.ContinuarRequisicao();
            }

            if (RetornosSitef.Success == (RetornosSitef)ret)
                Cnf(documentoVinculado);

            return ret;
        }

        #endregion

        private void SalvarTransacao(SiTefTransacao siTefTransacao)
            => CupomTef.TransacoesTEF.Add(siTefTransacao);

        private int FinalizarOperacao(short confirma, string documentoVinculado = "")
        {
            var dataHoraConfirmacao = DateTime.Now;

            if(string.IsNullOrEmpty(documentoVinculado.Trim()))
                documentoVinculado = new Random().Next(999999).ToString("000000");

            return SitefDllMapper.FinalizaFuncaoSiTefInterativo(confirma: confirma, 
                                                                cupomFiscal: documentoVinculado, 
                                                                dataFiscal: dataHoraConfirmacao.ToString("yyyyMMdd"),
                                                                horaFiscal: dataHoraConfirmacao.ToString("HHmmss"),
                                                                parametrosAdicionais: null);
        }

        private void ExportarTransacaoParaArquivo()
        {
            if (CupomTef == null && CupomTef.TransacoesTEF.Count == 0)
                return;

            var transacaoIdx = 1;
            foreach (var transacao in CupomTef.TransacoesTEF)
            {
                if (transacao.Retornos.Count == 0)
                    break;

                var path = $"{_configuracaoCliSiTef.ConfiguracaoTef.PathArquivos}\\TefRetorno";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                var retornos = transacao.Retornos.OrderBy(x => x.Chave)
                                                 .ThenBy(x => x.Indice)
                                                 .ToList();

                using (StreamWriter sw = File.AppendText($"{path}\\Tef{CupomTef.TipoOperacao}_{CupomTef.DocumentoVinculado}_T{transacaoIdx.ToString("000")}_{DateTime.Now.ToString("yyyyMMddHHmmss")}.tef"))
                    foreach (var retorno in retornos)
                    {
                        sw.WriteLine($"{retorno.Chave.PadLeft(3, '0')}-{retorno.Indice.ToString("000")}={retorno.Valor}");
                        sw.Flush();
                    }

                transacaoIdx++;
            }
        }
    }
}
