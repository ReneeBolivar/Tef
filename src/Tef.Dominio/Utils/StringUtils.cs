using System.Text.RegularExpressions;

namespace Tef.Dominio.Utils
{
    public static class StringUtils
    {
        public static string RemoverQuebraLinha(this string valor)
        {
            if (string.IsNullOrEmpty(valor.Trim()))
                return string.Empty;

            var str = " ";

            valor = valor.Replace("\n", str)
                         .Replace("\t", str)
                         .Replace("\r\n", str);

            return Regex.Replace(valor, @"\s{2,}", str);
        }
    }
}
