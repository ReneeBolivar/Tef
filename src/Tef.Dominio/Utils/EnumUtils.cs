using System.ComponentModel;
using System.Reflection;

namespace Tef.Dominio.Utils
{
    public static class EnumUtils
    {
        public static string ObterDescricao<T>(this T @enum)
        {
            FieldInfo fi = @enum.GetType().GetField(@enum.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0) 
                return attributes[0].Description;
            else 
                return @enum.ToString();
        }
    }
}
