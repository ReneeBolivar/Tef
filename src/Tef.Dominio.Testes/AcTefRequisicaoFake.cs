﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Tef.Dominio.Testes
{
    public class AcTefRequisicaoFake : IAcTefRequisicao
    {
        private string _arquivoCancelamentoCnc;
        private string _arquivoSaqueAdm;
        private TefLinhaLista _requisicao;

        public AcTefRequisicaoFake(ConfigRequisicao configRequisicao)
        {
            var path =
                $"{Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path))}";

            _arquivoCancelamentoCnc = $"{path}\\Assets\\ArquivoCancelamentoCnc.001";
            _arquivoSaqueAdm = $"{path}\\Assets\\ArquivoCancelamentoCnc.001";
        }

        public event EventHandler<AguardaRespostaEventArgs> AguardandoResposta;
        public event EventHandler<ImprimeViaEventArgs> ImprimirVia;
        public event EventHandler<ExibeMensagemEventArgs> ExibeMensagem;
        public event EventHandler<AutorizaDfeEventArgs> AutorizaDfe;
        public string ArquivoTemporario { get; }
        public string ArquivoRequisicao { get; }
        public string ArquivoResposta { get; }
        public string PastaBackup { get; }
        public string ArquivoSts { get; }
        public int EsperaSleep { get; }
        public int EsperaSts { get; }

        public TefLinhaLista Enviar(TefLinhaLista requisicao)
        {
            _requisicao = requisicao;
            var resposta = new List<TefLinha>
            {
                new TefLinha("000-000", requisicao.BuscaLinha(AcTefIdentificadorCampos.Comando).Valor),
                new TefLinha("001-000", requisicao.BuscaLinha(AcTefIdentificadorCampos.Identificacao).Valor),
                new TefLinha("999-999", "0")
            };

            return new TefLinhaLista(resposta);
        }

        public void OnAguardandoResposta(AguardaRespostaEventArgs e)
        {
            throw new NotImplementedException();
        }

        public TefLinhaLista AguardaRespostaRequisicao()
        {
            var resposta = new List<TefLinha>();

            if (_requisicao.BuscaLinha(AcTefIdentificadorCampos.Comando).Valor == "CNC")
            {
                AdicionaLinhasDeACordoComRequisicao(resposta, _arquivoCancelamentoCnc);
            }

            if (_requisicao.BuscaLinha(AcTefIdentificadorCampos.Comando).Valor == "ADM")
            {
                AdicionaLinhasDeACordoComRequisicao(resposta, _arquivoSaqueAdm);
            }

            return new TefLinhaLista(resposta);
        }

        private void AdicionaLinhasDeACordoComRequisicao(List<TefLinha> resposta, string arquivo)
        {
            using (var sr = new StreamReader(arquivo))
            {
                var linha = string.Empty;

                while ((linha = sr.ReadLine()) != null)
                {
                    resposta.Add(new TefLinha(linha));
                }
            }
        }

        public void OnImprimirVia(ImprimeViaEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnExibeMensagem(ExibeMensagemEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnAutorizaDfe(AutorizaDfeEventArgs e)
        {
            throw new NotImplementedException();
        }

        public List<string> BuscarCaminhosBackup()
        {
            throw new NotImplementedException();
        }
    }
}