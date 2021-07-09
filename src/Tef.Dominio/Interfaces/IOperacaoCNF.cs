namespace Tef.Dominio.Interfaces
{
    public interface IOperacaoCNF
    {
        void Cnf(string documentoVinculado = "", bool gerarArquivo = true);
    }
}
