using System;

namespace Extensions.System
{
    public static class BoolExtensions
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos
        // ---- ---- ---- ---- ---- ---- ---- ----
        /// <summary>
        /// Convierte el valor de esta instancia en una representación de cadena de texto personalizada.
        /// </summary>
        /// <param name="trueText">Cadena de texto a utilizar para el valor verdadero.</param>
        /// <param name="falseText">Cadena de texto a utilizar para el valor falso.</param>
        public static string ToCustomString(this bool b, string trueText, string falseText)
        {
            return b ? trueText : falseText;
        }
        
        /// <summary>
        /// Elige y devuelve uno de los valores especificados como parámetros, según si el valor de esta instancia es
        /// verdadero o falso.
        /// </summary>
        /// <param name="trueValue">Valor a devolver si esta instancia tiene el valor verdadero.</param>
        /// <param name="falseValue">Valor a devolver si esta instancia tiene el valor falso.</param>
        public static T SelectValue<T>(this bool b, T trueValue, T falseValue)
        {
            return b ? trueValue : falseValue;
        }
        
        /// <summary>
        /// Convierte el valor de esta instancia en los valores numéricos 0.0 y 1.0, como números de punto flotanta de
        /// simple precisión.
        /// </summary>
        public static float ToFloat(this bool b)
        {
            return b ? 1.0f : 0.0f;
        }
        
        /// <summary>
        /// Convierte el valor de esta instancia en los valores numéricos 0 y 1, como números enteros de 32 bits.
        /// </summary>
        public static int ToInt(this bool b)
        {
            return b ? 1 : 0;
        }
    }
    
}