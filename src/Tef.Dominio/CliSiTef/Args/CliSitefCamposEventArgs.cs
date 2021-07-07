using System;
using System.Collections.Generic;
using Tef.Dominio.Enums;

namespace Tef.Dominio.CliSiTef.Args
{
    public class CliSitefCamposEventArgs : EventArgs
    {
        public CliSitefCamposEventArgs(string mensagem, long tipoCampo, CampoCliSitefTipos operacao, string resposta)
        {
            Mensagem = mensagem;
            TipoCampo = tipoCampo;
            Operacao = operacao;
            Resposta = resposta;
        }

        public CliSitefCamposEventArgs(string titulo, ICollection<string> itensMenu, CampoCliSitefTipos operacao)
        {
            Titulo = titulo;
            ItensMenu = itensMenu;
            Operacao = operacao;
        }

        public CliSitefCamposEventArgs(string mensagem, CampoCliSitefTipos operacao)
        {
            Mensagem = mensagem;
            Operacao = operacao;
        }

        public CliSitefCamposEventArgs(string titulo, int tamanhoMin, int tamanhoMax, long tipoCampo, CampoCliSitefTipos operacao)
        {
            Titulo = titulo;
            TamanhoMin = tamanhoMin;
            TamanhoMax = tamanhoMax;
            TipoCampo = tipoCampo;
            Operacao = operacao;
        }

        public CliSitefCamposEventArgs(string titulo, string mensagem, long tipoCampo, CampoCliSitefTipos operacao)
        {
            Titulo = titulo;
            Mensagem = mensagem;
            TipoCampo = tipoCampo;
            Operacao = operacao;
        }

        public string Titulo { get; set; }
        public string Mensagem { get; set; }
        public ICollection<string> ItensMenu { get; set; }
        public int TamanhoMin { get; set; }
        public int TamanhoMax { get; set; }
        public long TipoCampo { get; set; }
        public CampoCliSitefTipos Operacao { get; set; }
        public string Resposta { get; set; }
        public bool Digitado { get; set; }
        public bool VoltarMenu { get; set; }
    }
}
