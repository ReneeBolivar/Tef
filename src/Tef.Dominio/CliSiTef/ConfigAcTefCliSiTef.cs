﻿using System;
using System.Collections.Generic;

namespace Tef.Dominio.CliSiTef
{
    public class ConfigAcTefCliSiTef : IConfigAcCliSiTef
    {
        public string Host { get; }
        public string CodigoLoja { get; }
        public string NumeroTerminal { get; }
        public bool Reservado { get; }
        public Dictionary<string, string> ParametrosAdicionais { get; }
        public string Operador { get; }
        public string Restricoes { get; }
        public string DocumentoFiscal { get; }
        public DateTime DataHoraFiscal { get; }
        public bool ExibirErroRetorno { get; }
        public string PathDll { get; }
        public bool RetornaQRCode { get; set; }
        public IConfigTef ConfiguracaoTef { get; }

        public ConfigAcTefCliSiTef(string host,
                                   string codigoLoja,
                                   string numeroTerminal,
                                   bool reservado,
                                   Dictionary<string, string> parametrosAdicionais,
                                   string operador,
                                   string restricoes,
                                   bool exibirErroRetorno,
                                   string pathDll,
                                   bool retornaQRCode,
                                   IConfigTef configuracaoTef,
                                   DateTime? dataHoraFiscal = null,
                                   string documentoFiscal = null)
        {
            Host = host;
            CodigoLoja = codigoLoja;
            NumeroTerminal = numeroTerminal;
            Reservado = reservado;
            ParametrosAdicionais = parametrosAdicionais;
            Operador = operador;
            Restricoes = restricoes;
            DocumentoFiscal = documentoFiscal ?? DateTime.Now.ToString("hhmmss");
            DataHoraFiscal = dataHoraFiscal ?? DateTime.Now;
            ExibirErroRetorno = exibirErroRetorno;
            PathDll = pathDll;
            RetornaQRCode = retornaQRCode;
            ConfiguracaoTef = configuracaoTef;
        }
    }
}
