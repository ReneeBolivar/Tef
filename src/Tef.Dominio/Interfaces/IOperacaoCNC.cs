using Tef.Dominio.CliSiTef;

namespace Tef.Dominio.Interfaces
{
    public interface IOperacaoCNC
    {
        int Cnc(OperacoesTEF operacao, string documentoVinculado = "");
    }
}
