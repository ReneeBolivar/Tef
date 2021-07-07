using System;

namespace Tef.Dominio.Exceptions
{
    public class SitefEventNotImplementedException : Exception
    {
        public SitefEventNotImplementedException(string errorMensage = "") : base(errorMensage)
        {
        }
    }
}
