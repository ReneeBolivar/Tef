using Bogus;
using Bogus.DataSets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Tef.Dominio.CliSiTef;
using Tef.Dominio.Enums;
using Tef.Dominio.Interfaces;

namespace Tef.Dominio.Testes
{
    [TestClass]
    public class AcSiTefTestes
    {
        public ISiTefRequisicao SiTefRequisicao { get; }
        public IRequisicaoSitef RequisicaoSitef { get; }
        public IConfigTef ConfigTef { get;  }
        public IConfigAcCliSiTef ConfigCliSitef { get; }

        public AcSiTefTestes()
        {
            ConfigTef = new ConfigTef(pathArquivos: "C:\\Users\\Renee\\source\\repos\\Tef\\src\\Tef.Dominio\\CliSiTef\\Dlls",
                                      ip: "127.0.0.1",
                                      empresa: "FIOLUX COMERCIAL LTDA",
                                      cnpjEmpresa: "32876302000114",
                                      terminal: "000001",
                                      cnpjSoftwareHouse: "74227577000177",
                                      pinPadPorta: "AUTO_USB",
                                      pinPadMensagem: "PINPAD MSG",
                                      pinPadVerificar: false);

            var param = new Dictionary<string, string>();

            ConfigCliSitef = new ConfigAcTefCliSiTef(host: "localhost",
                                                     codigoLoja: "000000000000005",
                                                     numeroTerminal: "000001",
                                                     reservado: false,
                                                     parametrosAdicionais: param,
                                                     operador: "renee",
                                                     restricoes: "",
                                                     exibirErroRetorno: true,
                                                     pathDll: "C:\\Users\\Renee\\source\\repos\\Tef\\src\\Tef.Dominio\\CliSiTef\\Dlls",
                                                     retornaQRCode: true,
                                                     configuracaoTef: ConfigTef);

            var sitefTrans = new SiTefTransacao();

            RequisicaoSitef = new RequisicaoSitef(ConfigCliSitef, sitefTrans);

            SiTefRequisicao = new AcCliSiTef(ConfigCliSitef, RequisicaoSitef);

            _faker = new Faker();
        }

        ~AcSiTefTestes()
        {
            RequisicaoSitef.AoSolicitarCamposSitef = null;
            RequisicaoSitef.AoNotificarMensagemSitef = null;
            RequisicaoSitef.AoGerarComprovante = null;
        }

        private Faker _faker;

        [TestMethod]
        public void Requisicao_ATV()
        {
            RequisicaoSitef.AoSolicitarCamposSitef += (e) =>
            {
                e.Resposta = "1"; // (1:Confirma)
                return e;
            };

            var retorno = (RetornosSitef)SiTefRequisicao.Atv();

            Assert.AreEqual(RetornosSitef.Success, retorno);
        }

        [TestMethod]
        public void Requisicao_ADM()
        {
            var itensMenuEsperado = new List<string>
            {
                "1:Teste de comunicacao",
                "2:Reimpressao de comprovante",
                "3:Cancelamento de transacao",
                "4:Pre-autorizacao",
                "5:Consulta parcelas CDC",
                "6:Consulta Private Label",
                "7:Consulta saque e saque Fininvest",
                "8:Consulta Saldo Debito",
                "9:Consulta Saldo Credito",
                "10:Outros Cielo",
                "11:Carga forcada de tabelas no pinpad (SiTef)",
                ""
            };
            var itensMenuRetornado = new List<string>();

            RequisicaoSitef.AoSolicitarCamposSitef += (e) =>
            {
                if(e.ItensMenu?.Any() ?? false)
                    itensMenuRetornado = e.ItensMenu.ToList();

                e.Resposta = "1";
                return e;
            };

            var retorno = (RetornosSitef)SiTefRequisicao.Adm();

            var listasIguais = itensMenuEsperado.SequenceEqual(itensMenuRetornado);

            Assert.AreEqual(RetornosSitef.Success, retorno);
            Assert.AreEqual(true, listasIguais);
        }

        [TestMethod]
        public void Requisicao_CRT_Credito()
        {
            var numeroDoCartao = _faker.Finance.CreditCardNumber(CardType.Mastercard).Replace("-", string.Empty);
            var expiracao_MMyy = DateTime.Now.AddYears(2).ToString("MMyy");
            var cvv = _faker.Finance.CreditCardCvv();
            var respostas = new List<string>
            {
                "3", // (3:Cartão de crédito)
                "2", // (2:Digitado)
                numeroDoCartao,
                expiracao_MMyy,
                cvv,
                "1" // (1:Á vista)
            };
            var indiceResp = 0;

            RequisicaoSitef.AoSolicitarCamposSitef += (e) =>
            {
                e.Resposta = respostas.ElementAt(indiceResp);

                indiceResp++;
                return e;
            };

            RequisicaoSitef.AoNotificarMensagemSitef += (m, t) =>
            {
            };

            var valor = Math.Round(_faker.Random.Decimal(1, 100), 2);
            var documentoVinculado = _faker.Random.String2(6, "0123456789");
            var operador = _faker.Person.FirstName;

            var retorno = (RetornosSitef)SiTefRequisicao.Crt(valor, documentoVinculado, operador);

            Assert.AreEqual(RetornosSitef.Success, retorno);
        }

        [TestMethod]
        public void Requisicao_CRT_PIX()
        {
            var respostas = new List<string>
            {
                "6", // (6:Carteira Digital)
                "3", // (3:Pix) 
            };
            var indiceResp = 0;

            var qrCode = string.Empty;
            RequisicaoSitef.AoSolicitarCamposSitef += (e) =>
            {
                if (e.Operacao == CampoCliSitefTipos.QrCode && !string.IsNullOrEmpty(e.Mensagem))
                    qrCode = e.Mensagem;
                else
                    e.Resposta = respostas.ElementAt(indiceResp);

                indiceResp++;
                return e;
            };

            RequisicaoSitef.AoNotificarMensagemSitef += (m, t) =>
            {
            };

            var valor = Math.Round(_faker.Random.Decimal(1, 100), 2);
            var documentoVinculado = _faker.Random.String2(6, "0123456789");
            var operador = _faker.Person.FirstName;

            var retorno = (RetornosSitef)SiTefRequisicao.Crt(valor, documentoVinculado, operador);

            Assert.AreEqual(RetornosSitef.Success, retorno);
            Assert.AreEqual(true, !string.IsNullOrEmpty(qrCode));
        }

        [TestMethod]
        public void Requisicao_CRT_ImpriveVias()
        {
            var viasComprovante = new List<string>();

            var numeroDoCartao = _faker.Finance.CreditCardNumber(CardType.Mastercard).Replace("-", string.Empty);
            var expiracao_MMyy = DateTime.Now.AddYears(2).ToString("MMyy");
            var cvv = _faker.Finance.CreditCardCvv();
            var respostas = new List<string>
            {
                "3", // (3:Cartão de crédito)
                "2", // (2:Digitado)
                numeroDoCartao,
                expiracao_MMyy,
                cvv,
                "1" // (1:Á vista)
            };
            var indiceResp = 0;

            RequisicaoSitef.AoSolicitarCamposSitef += (e) =>
            {
                e.Resposta = respostas.ElementAt(indiceResp);

                indiceResp++;
                return e;
            };

            RequisicaoSitef.AoNotificarMensagemSitef += (m, t) =>
            {
            };

            RequisicaoSitef.AoGerarComprovante += (m) =>
            {
                viasComprovante.Add(m);
            };

            var valor = Math.Round(_faker.Random.Decimal(1, 100), 2);
            var documentoVinculado = _faker.Random.String2(6, "0123456789");
            var operador = _faker.Person.FirstName;

            var retorno = (RetornosSitef)SiTefRequisicao.Crt(valor, documentoVinculado, operador);

            Assert.AreEqual(RetornosSitef.Success, retorno);
            Assert.AreEqual(2, viasComprovante.Count);
        }
    }
}
