namespace Tef.Dominio.Interfaces
{
    public interface IOperacaoCRT
    {
        int Crt(decimal valor, string documentoVinculado, string operador, bool confirmarManual = true);
    }
}
