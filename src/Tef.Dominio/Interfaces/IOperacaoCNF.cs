namespace Tef.Dominio.Interfaces
{
    public interface IOperacaoCNF
    {
        int Cnf(string documentoVinculado = "", bool gerarArquivo = true);
    }
}
