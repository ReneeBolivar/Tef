namespace Tef.Dominio.CliSiTef
{
    public class TefRetorno
    {
        public string Chave { get; set; }
        public int Indice { get; set; }
        public string Valor { get; set; }

        public TefRetorno(string chave, string valor, int indice)
        {
            Chave = chave;
            Indice = indice;
            Valor = valor;
        }
    }
}
