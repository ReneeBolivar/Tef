﻿using System;

namespace Tef.Dominio
{
    public interface ITef : IRequisicaoAtv
    {
        RespostaAtv Atv();
        RespostaAdm Adm(string documentoVinculado = "");
        RespostaCnc Cnc(string rede, string nsu, DateTime transacaoEm, decimal valor);
        RespostaCrt Crt(decimal valor, string documentoVinculado, string operador = "", bool confirmarManual = false);
        RespostaCrt ConfirmarCrt(AutorizaDfeEventArgs autorizaDfeEventArgs);

        void Cnf(
            string redeAdquirente,
            string nsu,
            string codigoControle,
            string nomeAutomacao,
            string versaoAutomacao,
            string registroCertificacao
        );

        void Ncn(string redeAdquirente, string codigoControle);
        void Inicializa();
    }
}