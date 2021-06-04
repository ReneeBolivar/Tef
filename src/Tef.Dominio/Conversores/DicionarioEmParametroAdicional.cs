using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tef.Dominio.Conversores
{
    public static class DicionarioEmParametroAdicional
    {
        public static string Converter(this Dictionary<string, string> dic)
        {
            var sb = new StringBuilder();

            if (dic.Any())
            {
                sb.Append("[");

                foreach (var pair in dic)
                    sb.Append($"{pair.Key}={pair.Value};");
                
                sb.Append("]");
            }

            return sb.ToString();
        }
    }
}
