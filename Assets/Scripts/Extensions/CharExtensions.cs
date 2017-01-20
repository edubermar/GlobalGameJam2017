using System;
using System.Globalization;

namespace Extensions.System
{
    public static class CharExtensions
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos de conversión
        /// <summary>
        /// Convierte el carácter actual a su equivalente en minúsculas.
        /// </summary>
        public static char ToLower(this char c)
        {
            return char.ToLowerInvariant(c);
        }
        
        /// <summary>
        /// Convierte el carácter actual a su equivalente en minúsculas, utilizando las reglas de formato culturales
        /// especificadas.
        /// </summary>
        public static char ToLower(this char c, CultureInfo culture)
        {
            return char.ToLower(c, culture);
        }
        
        /// <summary>
        /// Convierte el carácter actual a su equivalente en mayúsculas.
        /// </summary>
        public static char ToUpper(this char c)
        {
            return char.ToUpperInvariant(c);
        }
        
        /// <summary>
        /// Convierte el carácter actual a su equivalente en mayúsculas, utilizando las reglas de formato culturales
        /// especificadas.
        /// </summary>
        public static char ToUpper(this char c, CultureInfo culture)
        {
            return Char.ToUpper(c, culture);
        }
        
        // Métodos de información
        /// <summary>
        /// Determina si el carácter actual es una consonante.
        /// </summary>
        public static bool IsConsonant(this char c)
        {
            return char.IsLetter(c) && !CharExtensions.IsVowel(c);
        }
        
        /// <summary>
        /// Determina si el carácter actual es un dígito.
        /// </summary>
        public static bool IsDigit(this char c)
        {
            return char.IsDigit(c);
        }
        
        /// <summary>
        /// Determina si el carácter actual es alfabético.
        /// </summary>
        public static bool IsLetter(this char c)
        {
            return char.IsLetter(c);
        }
        
        /// <summary>
        /// Determina si el carácter actual es alfanumérico.
        /// </summary>
        public static bool IsLetterOrDigit(this char c)
        {
            return char.IsLetterOrDigit(c);
        }
        
        /// <summary>
        /// Determina si el carácter actual está en minúsculas.
        /// </summary>
        public static bool IsLower(this char c)
        {
            return char.IsLower(c);
        }
        
        /// <summary>
        /// Determina si el carácter actual está en mayúsculas.
        /// </summary>
        public static bool IsUpper(this char c)
        {
            return char.IsUpper(c);
        }
        
        /// <summary>
        /// Determina si el carácter actual es una vocal.
        /// </summary>
        public static bool IsVowel(this char c)
        {
            return "aeiouáéíóúàèìòùäëïöüâêîôû".Contains(string.Empty + c);
        }
        
        /// <summary>
        /// Determina si el carácter actual es considerado un espacio en blanco.
        /// </summary>
        public static bool IsWhitespace(this char c)
        {
            return char.IsWhiteSpace(c);
        }
        
        // Otros métodos
        /// <summary>
        /// Devuelve un objeto System.String que contiene el carácter actual repetido el número de veces especificado.
        /// </summary>
        /// <param name="repeatCount">Número de repeticiones.</param>
        public static string Repeat(this char c, int count)
        {
            return new string(c, count);
        }
    }
    
}