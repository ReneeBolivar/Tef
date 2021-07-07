using System;
using System.Linq;
using System.Text;
using Tef.Dominio.CliSiTef.Args;
using Tef.Dominio.Enums;
using Tef.Dominio.Exceptions;
using Tef.Dominio.Utils;

namespace Tef.Dominio.CliSiTef
{
    public class RequisicaoSitef : IRequisicaoSitef
    {
        public NotificarMensagemSitef AoNotificarMensagemSitef { get; set; }
        public SolicitarCamposSitef AoSolicitarCamposSitef { get; set; }
        public AdicionarRetorno AoAdicionarRetorno { get; set; }

        private readonly IConfigAcCliSiTef _configAcTefCliSiTef;
        public SiTefTransacao SiTefTransacao { get; set; }

        public RequisicaoSitef(IConfigAcCliSiTef configAcTefCliSiTef, SiTefTransacao siTefTransacao)
        {
            _configAcTefCliSiTef = configAcTefCliSiTef;
            SiTefTransacao = siTefTransacao;
            ChecarInscricaoEventos();
        }

        private void ChecarInscricaoEventos()
        {
            if (!AoNotificarMensagemSitef.GetInvocationList().Any())
                throw new SitefEventNotImplementedException($"Necessário existir ao menos um inscrito no evento {nameof(AoNotificarMensagemSitef)}");

            if (!AoSolicitarCamposSitef.GetInvocationList().Any())
                throw new SitefEventNotImplementedException($"Necessário existir ao menos um inscrito no evento {nameof(AoSolicitarCamposSitef)}");

            if (!AoAdicionarRetorno.GetInvocationList().Any())
                throw new SitefEventNotImplementedException($"Necessário existir ao menos um inscrito no evento {nameof(AoAdicionarRetorno)}");
        }

        public int IniciarRequisicao(OperacoesSitef funcao, decimal valor = 0, string documento = "", string paramAdicionais = "", string operador = "")
        {
            if (string.IsNullOrEmpty(documento.Trim()))
                DefineDocumento(out documento);

            if (!paramAdicionais.Contains("{TipoTratamento=4}") && (paramAdicionais.Contains(OperacoesSitef.OperacaoADM.ObterDescricao()) ||
                                                                      paramAdicionais.Contains(OperacoesSitef.OperacaoCRT.ObterDescricao()) ||
                                                                      paramAdicionais.Contains(OperacoesSitef.OperacaoCHQ.ObterDescricao())))
                paramAdicionais += "{TipoTratamento=4}";

            if (funcao == OperacoesSitef.OperacaoCRT && _configAcTefCliSiTef.RetornaQRCode)
                paramAdicionais += "{DevolveStringQRCode=1}";

            var agora = DateTime.Now;

            return SitefDllMapper.IniciaFuncaoSiTefInterativo(funcao: (int)funcao,
                                                              valor: valor.ToString("N2"),
                                                              cupomFiscal: documento,
                                                              dataFiscal: agora.ToString("yyyyMMdd"),
                                                              horaFiscal: agora.ToString("HHmmss"),
                                                              operador: operador,
                                                              parametrosAdicionais: paramAdicionais);
        }

        private void DefineDocumento(out string documento)
            => documento = DateTime.Now.ToString("HHmmss");

        public int ContinuarRequisicao()
        {
            var buffer = new byte[20000];
            RetornosSitef retorno;
            var continua = 0;
            var taxas = 1;
            var tituloMenu = string.Empty;
            var tituloCarteiraDig = string.Empty;
            var interromper = false;
            CliSitefCamposEventArgs reqCliSitefCampos;
            CliSitefCamposEventArgs respCliSitefCampos;

            do
            {
                retorno = (RetornosSitef)SitefDllMapper.ContinuaFuncaoSiTefInterativo(comando: out int proxComando,
                                                                                      tipoCampo: out long tipoCampo,
                                                                                      tamMinimo: out short minBuffer,
                                                                                      tamMaximo: out short maxBuffer,
                                                                                      buffer: buffer,
                                                                                      tamBuffer: buffer.Length,
                                                                                      continua: continua);

                continua = 0;
                string mensagem = Encoding.UTF8.GetString(buffer).Replace("\0", string.Empty).Trim();
                string respostaSitef = string.Empty;
                bool voltar = false;
                bool digitado = true;

                if (retorno == RetornosSitef.Continue)
                {
                    switch ((ComandosSitef)proxComando)
                    {
                        case ComandosSitef.StoreValue:
                            ArmazenarInformacoes(mensagem, tipoCampo);

                            if (!string.IsNullOrEmpty(mensagem.Trim()) && tipoCampo == 107)
                                tituloCarteiraDig = mensagem;

                            if (tipoCampo == 111)
                                AoNotificarMensagemSitef(mensagem, 100);

                            break;
                        case ComandosSitef.DisplayOperatorMessage:
                        case ComandosSitef.DisplayCustomerMessage:
                        case ComandosSitef.DisplayBothMessage:
                            AoNotificarMensagemSitef(mensagem, 100);
                            break;
                        case ComandosSitef.DisplayMenuHeader:
                            tituloMenu = mensagem;
                            break;
                        case ComandosSitef.ClearOperatorMessage:
                        case ComandosSitef.ClearCustomerMessage:
                        case ComandosSitef.ClearBothMessage:
                            mensagem = string.Empty;
                            AoNotificarMensagemSitef(mensagem, 0);
                            break;
                        case ComandosSitef.ClearMenuHeader:
                        case ComandosSitef.ClearHeader:
                            tituloMenu = string.Empty;
                            break;
                        case ComandosSitef.DisplayConfirm:
                            #region Solicitação de confirmação do usuário

                            if (string.IsNullOrEmpty(mensagem.Trim()))
                                mensagem = "Confirma?";

                            reqCliSitefCampos = new CliSitefCamposEventArgs(mensagem: mensagem,
                                                                            tipoCampo: tipoCampo,
                                                                            operacao: CampoCliSitefTipos.Confirmation,
                                                                            resposta: "1");

                            respCliSitefCampos = AoSolicitarCamposSitef(reqCliSitefCampos);
                            respostaSitef = respCliSitefCampos.Resposta;
                            interromper = respCliSitefCampos.VoltarMenu; 
                            
                            #endregion
                            break;
                        case ComandosSitef.DisplayMenuOptions:
                            #region Mostrar menu de opções

                            reqCliSitefCampos = new CliSitefCamposEventArgs(titulo: tituloMenu,
                                                                            itensMenu: mensagem.Split(';'),
                                                                            operacao: CampoCliSitefTipos.Menu);

                            respCliSitefCampos = AoSolicitarCamposSitef(reqCliSitefCampos);
                            respostaSitef = respCliSitefCampos.Resposta;
                            interromper = respCliSitefCampos.VoltarMenu;

                            #endregion
                            break;
                        case ComandosSitef.WaitAnyKey:
                            #region Aguardar processamento (notifica mensagem para operador se necessário)

                            if (string.IsNullOrEmpty(mensagem.Trim()))
                                mensagem = "Aguarde...";

                            reqCliSitefCampos = new CliSitefCamposEventArgs(mensagem: mensagem,
                                                                            operacao: CampoCliSitefTipos.Wait);

                            AoSolicitarCamposSitef(reqCliSitefCampos);

                            #endregion
                            break;
                        case ComandosSitef.CancelPinPadOperation:

                            break;
                        case ComandosSitef.ParameterInformedWithoutIntervention:
                            break;
                        case ComandosSitef.TextInputNeeded:
                            #region Solicitação de valor numérico

                            reqCliSitefCampos = new CliSitefCamposEventArgs(titulo: mensagem,
                                                                            tamanhoMin: minBuffer,
                                                                            tamanhoMax: maxBuffer,
                                                                            tipoCampo: tipoCampo,
                                                                            operacao: CampoCliSitefTipos.Numeric);

                            respCliSitefCampos = AoSolicitarCamposSitef(reqCliSitefCampos);
                            respostaSitef = respCliSitefCampos.Resposta;
                            interromper = respCliSitefCampos.VoltarMenu;

                            if (!interromper && tipoCampo == 505)
                            {
                                var tefRetorno = new TefLinha(chave: "505",
                                                              valor: respCliSitefCampos.Resposta,
                                                              posicao: 0);
                                AoAdicionarRetorno(tefRetorno);
                            }
                            
                            #endregion
                            break;
                        case ComandosSitef.BankCheckInputNeeded:
                            break;
                        case ComandosSitef.MoneyInputNeeded:
                            #region Solicitação do valor

                            reqCliSitefCampos = new CliSitefCamposEventArgs(titulo: mensagem,
                                                                            tamanhoMin: minBuffer,
                                                                            tamanhoMax: maxBuffer,
                                                                            tipoCampo: tipoCampo,
                                                                            operacao: CampoCliSitefTipos.Numeric);

                            respCliSitefCampos = AoSolicitarCamposSitef(reqCliSitefCampos);
                            respostaSitef = respCliSitefCampos.Resposta;
                            interromper = respCliSitefCampos.VoltarMenu;

                            if(tipoCampo == 504 || tipoCampo == 130)
                                if(!string.IsNullOrEmpty(respCliSitefCampos.Resposta.Trim()) && Decimal.Parse(respCliSitefCampos.Resposta) > 0)
                                {
                                    var sb = new StringBuilder();
                                    sb.Append(Decimal.Parse(respCliSitefCampos.Resposta).ToString("N2"));
                                    sb.Append("|");
                                    sb.Append(mensagem.RemoverQuebraLinha());

                                    var tefRetorno = new TefLinha(chave: "3",
                                                                  valor: sb.ToString(),
                                                                  posicao: taxas);

                                    AoAdicionarRetorno(tefRetorno);
                                    taxas++;
                                }

                            #endregion
                            break;
                        case ComandosSitef.BarcodeInputNeeded:
                            break;
                        case ComandosSitef.PasswordTextInputNeeded:
                            break;
                        case ComandosSitef.DisplayIdentifiedMenuOptions:
                            break;
                        case ComandosSitef.DisplayQRCode when _configAcTefCliSiTef.RetornaQRCode:
                            #region Dispor QRCode

                            reqCliSitefCampos = new CliSitefCamposEventArgs(titulo: mensagem,
                                                                            mensagem: mensagem,
                                                                            tipoCampo: tipoCampo,
                                                                            operacao: CampoCliSitefTipos.QrCode);

                            respCliSitefCampos = AoSolicitarCamposSitef(reqCliSitefCampos);
                            respostaSitef = respCliSitefCampos.Resposta;
                            interromper = respCliSitefCampos.VoltarMenu;
                            tituloCarteiraDig = string.Empty;

                            #endregion
                            break;
                        case ComandosSitef.ClearQRCode:
                            AoNotificarMensagemSitef(mensagem, 100);
                            break;
                        default:
                            break;
                    }
                }

                if (voltar)
                    continua = 1;
                else
                    if (!digitado || interromper)
                        continua = -1;

                buffer = Encoding.ASCII.GetBytes(respostaSitef + new string('\0', 20000 - respostaSitef.Length));

            } while (retorno == RetornosSitef.Continue);

            return 0;
        }

        private TefLinha tefRetorno;
        private void ArmazenarInformacoes(string mensagem, long tipoCampo)
        {
            switch (tipoCampo)
            {
                case 0:
                    Salvar("1", mensagem, 0);
                    break;
                case 100:
                    Salvar("11", mensagem, 0);

                    var aut = mensagem.PadRight(4, '0');
                    Salvar("731", aut.Substring(0, 2), 0);

                    var modPagtoGrp = ModalidadePagamentoGrupo.ObterModalidadePagamento(aut.Substring(0, 2));
                    if (!string.IsNullOrEmpty(modPagtoGrp))
                        Salvar("731", modPagtoGrp, 1);

                    Salvar("732", aut.Substring(2, 2), 0);

                    var modPagtoSubGrp = ModalidadePagamentoGrupo.ObterModalidadePagamento(aut.Substring(2, 2));
                    if (!string.IsNullOrEmpty(modPagtoSubGrp))
                        Salvar("732", modPagtoSubGrp, 1);
                    break;
                case 105:
                    var data = mensagem.Substring(6, 2) + mensagem.Substring(4, 2) + mensagem.Substring(0, 4);
                    Salvar("22", data, 0);

                    var hora = mensagem.Substring(8);
                    Salvar("23", hora, 0);
                    break;
                case 106:
                    if (!string.IsNullOrEmpty(mensagem.Trim()))
                    {
                        Salvar("748", mensagem, 1);

                        var bandeira = BandeiraPadrao.ObterCartao(mensagem);
                        if(!string.IsNullOrEmpty(bandeira))
                            Salvar("748", bandeira, 2);
                    }
                    break;
                case 107:
                    if (!string.IsNullOrEmpty(mensagem.Trim()))
                        Salvar("748", mensagem, 0);
                    
                    break;
                case 121:
                    var viaCliente = mensagem.Split('\n', '\r');
                    var tamArray = viaCliente.Length;
                    Salvar("712", tamArray.ToString(), 0);

                    for (var i = 0; i < tamArray; i++)
                        Salvar("713", $"\"{viaCliente[i]}\"", i);

                    break;
                case 122:
                    var viaEstabelecimento = mensagem.Split('\n', '\r');
                    var tamArrayE = viaEstabelecimento.Length;
                    Salvar("714", tamArrayE.ToString(), 0);

                    for (int i = 0; i < tamArrayE; i++)
                        Salvar("715", viaEstabelecimento[i], i);

                    break;
                case 123:
                    if(!string.IsNullOrEmpty(mensagem.Trim()))
                    {
                        var comp = ComprovanteTipos.ObterComprovante(mensagem);
                        if(!string.IsNullOrEmpty(comp))
                        {
                            Salvar("712", comp, 1);
                            Salvar("714", comp, 1);
                        }
                    }
                    break;
                case 131:
                    Salvar("10", mensagem, 0);

                    var redeAut = RedeAutorizadora.ObterAutorizadora(mensagem);
                    if(!string.IsNullOrEmpty(redeAut))
                        Salvar("10", redeAut, 1);
                    break;
                case 132:
                    Salvar("784", mensagem, 1);

                    var bandeiraDefault = BandeiraPadrao.ObterCartao(mensagem);
                    if (!string.IsNullOrEmpty(bandeiraDefault))
                        Salvar("748", bandeiraDefault, 2);
                    break;
                case 133:
                    Salvar("13", mensagem, 0);
                    break;
                case 134:
                    Salvar("12", mensagem, 0);
                    break;
                case 156:
                    Salvar("748", mensagem, 0);
                    break;
                case 158:
                    Salvar("739", mensagem, 0);
                    break;
                case 590:
                    Salvar("742", mensagem, 0);
                    break;
                case 591:
                    if (!string.IsNullOrEmpty(mensagem.Trim()))
                    {
                        Decimal.TryParse(mensagem, out decimal valorRec);
                        valorRec = valorRec / 100m;
                        Salvar("742", valorRec.ToString("N2"), 1);
                    }
                    break;
                case 800:
                    Salvar("27", mensagem, 0);
                    break;
                case 2021:
                    Salvar("740", mensagem, 0);
                    break;
                case 2022:
                    var msgAut = mensagem.PadRight(4, '0');
                    Salvar("747", $"{msgAut.Substring(2, 2)}{msgAut.Substring(0, 2)}", 0);
                    break;
                case 2023:
                    Salvar("741", mensagem, 0);
                    break;
                default:
                    break;
            }
        }

        public void Salvar(string chave, string valor, int indice)
        {
            tefRetorno = new TefLinha(chave, valor, indice);
            SalvarRetorno(tefRetorno);
            tefRetorno = null;
        }

        private void SalvarRetorno(TefLinha tefRetorno)
        {
            var retorno = SiTefTransacao.Retornos.SingleOrDefault(x => x.Chave == tefRetorno.Chave);
            if (retorno != null)
                SiTefTransacao.Retornos.Remove(retorno);

            SiTefTransacao.Retornos.Add(tefRetorno);
        }
        
    }
}
