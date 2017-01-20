using System;
using System.Collections.Generic;
using System.Globalization;

namespace Extensions.System
{
    public static class StringExtensions
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Atributos
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Delegados
        private delegate bool ParsingFunc<T>(string s, out T result);
        private delegate bool ParsingFuncDec<T>(string s, NumberStyles ns, IFormatProvider ci, out T result);
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos de manipulación
        /// <summary>
        /// Devuelve una copia de este objeto System.String con su primer carácter convertido a mayúsculas, aplicando
        /// las reglas de mayúsculas y minúsculas de la referencia cultural invariante.
        /// </summary>
        public static string Capitalize(this string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                if (s.Length == 1) return s.ToUpperInvariant();
                return s.Substring(0, 1).ToUpperInvariant() + s.Substring(1);
            }
            return s;
        }
        
        /// <summary>
        /// Devuelve una copia de este objeto System.String con su primer carácter convertido a mayúsculas, aplicando
        /// las reglas de mayúsculas y minúsculas de la referencia cultural especificada.
        /// </summary>
        public static string Capitalize(this string s, CultureInfo cultureInfo)
        {
            if (!string.IsNullOrEmpty(s))
            {
                if (s.Length == 1) return s.ToUpper(cultureInfo);
                return s.Substring(0, 1).ToUpper(cultureInfo) + s.Substring(1);
            }
            return s;
        }
        
        /// <summary>
        /// Devuelve una copia de este objeto System.String, con el primero de sus caracteres descartado. Si no
        /// contenía ningún carácter antes de realizar la operación, se devuelve la cadena vacía.
        /// </summary>
        public static string DropFirst(this string s)
        {
            if (!string.IsNullOrEmpty(s))
                return s.Substring(1);
            
            return s;
        }
        
        /// <summary>
        /// Devuelve una copia de este objeto System.String, con uno o más de los primeros caracteres que contiene
        /// descartados. Si se especifica un número de caracteres a descartar mayor que la longitud del objeto
        /// System.String actual, se devuelve la cadena vacía.
        /// </summary>
        /// <param name="charCount">Número de carácteres a descartar.</param>
        public static string DropFirst(this string s, int charCount)
        {
            if (!string.IsNullOrEmpty(s))
            {
                // A partir de aquí, la cadena no es nula y tiene al menos longitud 1.
                int resultLength = s.Length - charCount;
                if (resultLength > 0)
                    return s.Substring(charCount, s.Length - charCount);
                return string.Empty;
            }
            return s;
        }
        
        /// <summary>
        /// Devuelve una copia de este objeto System.String, con el último de sus caracteres descartado. Si no contenía
        /// ningún carácter antes de realizar la operación, se devuelve la cadena vacía.
        /// </summary>
        public static string DropLast(this string s)
        {
            if (!string.IsNullOrEmpty(s))
                return s.Substring(0, s.Length - 1);
            
            return s;
        }
        
        /// <summary>
        /// Devuelve una copia de este objeto System.String, con uno o más de los últimos caracteres que contiene
        /// descartados. Si se especifica un número de caracteres a descartar mayor que la longitud del objeto
        /// System.String actual, se devuelve la cadena vacía.
        /// </summary>
        /// <param name="charCount">Número de carácteres a descartar.</param>
        public static string DropLast(this string s, int charCount)
        {
            if (!string.IsNullOrEmpty(s))
            {
                // A partir de aquí, la cadena no es nula y tiene al menos longitud 1.
                int resultLength = s.Length - charCount;
                if (resultLength > 0)
                    return s.Substring(0, s.Length - charCount);
                return string.Empty;
            }
            return s;
        }
        
        /// <summary>
        /// Busca la primera ocurrencia de la cadena de caracteres especificados, y devuelve el fragmento de este
        /// objeto System.String que aparece antes de esta ocurrencia. Si no se puede encontrar la cadena especificada,
        /// se devuelve la cadena vacía.
        /// </summary>
        /// <param name="charCount">Número de carácteres a descartar.</param>
        public static string GetBefore(this string s, string substring)
        {
            int xPos = s.IndexOf(substring);
            return xPos == -1 ? String.Empty : s.Substring(0, xPos);
        }
        
        /// <summary>
        /// Busca dos cadenas de texto en este objeto System.String, y devuelve el fragmento que está delimitado por
        /// ambos. Si no se puede realizar esta delimitación, se devuelve la cadena vacía.
        /// </summary>
        /// <param name="charCount">Número de carácteres a descartar.</param>
        public static string GetBetween(this string s, string first, string second)
        {
            var xPos = s.IndexOf(first);
            var yPos = s.LastIndexOf(second);
            
            if (xPos == -1 || yPos == -1)
                return string.Empty;
            
            var startIndex = xPos + first.Length;
            return startIndex >= yPos ? string.Empty : s.Substring(startIndex, yPos - startIndex);
        }
        
        /// <summary>
        /// Obtiene la parte de la cadena de texto actual que viene a continuación de la cadena especificada.
        /// </summary>
        public static string GetAfter(this string s, string substring)
        {
            int xPos = s.LastIndexOf(substring);
            
            if (xPos == -1)
                return String.Empty;
            
            int startIndex = xPos + substring.Length;
            return startIndex >= s.Length ? string.Empty : s.Substring(startIndex);
        }
        
        /// <summary>
        /// Elimina cualquiera de los caractéres especificados de esta cadena de texto, y devuelve el resultado en un
        /// nuevo objeto System.String.
        /// </summary>
        public static string Remove(this string s, params char[] charList)
        {
            int resultIndex = 0;
            char[] result = new char[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                foreach (char c in charList) { if (c == s[i]) continue; }
                
                result[resultIndex] = s[i];
                resultIndex++;
            }
            
            return resultIndex <= 0 ? string.Empty : new string(result, 0, resultIndex);
        }
        
        /// <summary>
        /// Elimina cualquiera de las ocurrencias de las cadenas de texto especificadas como parámetros de esta cadena
        /// de texto, y devuelve el resultado en un nuevo objeto System.String.
        /// </summary>
        public static string Remove(this string value, params string[] stringList)
        {
            string result = value;
            foreach (var item in stringList)
                result = result.Replace(item, string.Empty);
            
            return result;
        }
        
        /// <summary>
        /// Devuelve una copia de este objeto System.String con todos los caracteres en orden inverso.
        /// </summary>
        public static string Reverse(this string s)
        {
            if (s.Length <= 1) return s;
            
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        
        /// <summary>
        /// Devuelve una copia de este objeto System.String con todos los caracteres reordenados en orden ascendente.
        /// </summary>
        public static string SortCharacters(this string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Sort<char>(charArray);
            return new string(charArray);
        }
        
        /// <summary>
        /// Devuelve una copia de este objeto System.String con todos los caracteres reordenados según el criterio del
        /// comparador especificado.
        /// </summary>
        public static string SortCharacters(this string s, IComparer<char> comparer)
        {
            char[] charArray = s.ToCharArray();
            Array.Sort<char>(charArray, comparer);
            return new string(charArray);
        }
        
        /// <summary>
        /// Devuelve una copia de este objeto System.String truncada a una determinada longitud máxima.
        /// </summary>
        /// <param name="charCount">Número máximo de caracteres.</param>
        public static string Truncate(this string s, int charCount)
        {
            return (s == null || s.Length < charCount) ? s : s.Substring(0, charCount);
        }
        
        /// <summary>
        /// Devuelve una copia de este objeto System.String truncada a una determinada longitud máxima.
        /// </summary>
        /// <param name="charCount">Número máximo de caracteres.</param>
        /// <param name="suffix">String to add to the end to mean an ellipsis had ocurred.</param>
        public static string Truncate(this string s, int charCount, string suffix)
        {
            return (s == null || s.Length < charCount) ? s : s.Substring(0, charCount - suffix.Length) + suffix;
        }
        
        /// <summary>
        /// Devuelve una copia de este objeto System.String convertida al formato Camel Case, en el que cada palabra
        /// aparece en mayúsculas y sin espacios de separación.
        /// </summary>
        public static string ToCamelCase(this string s)
        {
            string[] words = StringExtensions.SplitWords(
                CultureInfo.InvariantCulture.TextInfo.ToTitleCase(s.ToLowerInvariant()));
            return string.Join(string.Empty, words);
        }
        
        // Métodos de información
        /// <summary>
        /// Devuelve la distancia de edición existente entre esta instancia y el objeto System.String especificado.
        /// </summary>
        /// <param name="t">Cadena de texto a comparar.</param>
        public static int EditDistanceTo(this string s, string t)
        {
            return StringUtil.EditDistance(s, t);
        }
        
        /// <summary>
        /// Determina si el valor de este objeto System.String coincide con el de otro objeto del mismo tipo, ignorando
        /// mayúsculas y minúsculas.
        /// </summary>
        public static bool EqualsIgnoreCase(this string s, string other)
        {
            return s.ToUpperInvariant() == other.ToUpperInvariant();
        }
        
        /// <summary>
        /// Determina si el valor de este objeto System.String coincide con de alguno de los otros objetos
        /// System.String suministrados como parámetros.
        /// </summary>
        /// <param name="stringList">Objetos System.String para comparar con esta instancia.</param>
        public static bool EqualsRange(this string s, params string[] stringList)
        {
            foreach (string item in stringList)
            {
                if (item == s)
                    return true;
            }
            return false;
        }
        
        /// <summary>
        /// Determina si el valor de este objeto System.String es un palíndromo. Ignora mayúsculas y minúsculas.
        /// </summary>
        public static bool IsPalindrome(this string s)
        {
            if (s.Length < 2)
                return true;
            
            int min = 0;
            int max = s.Length - 1;
            while (true)
            {
                if (min > max)
                    return true;
                
                if (char.ToUpperInvariant(s[min]) != char.ToUpperInvariant(s[max]))
                    return false;
                
                min++;
                max--;
            }
        }
        
        // Métodos de generación
        /// <summary>
        /// Devuelve una matriz de cadenas que contiene las subcadenas de esta instancia que están delimitadas por
        /// elementos de la matriz de caracteres Unicode especificada, y además quita todos los caracteres de espacio
        /// en blanco del principio y del final de cada objeto System.String de la matriz resultante.
        /// </summary>
        /// <param name="separators"></param>
        public static string[] SplitAndTrim(this string s, params char[] separators)
        {
            string[] result = s.Split(separators);
            for (int i = 0; i < result.Length; i++)
                result[i] = result[i].Trim();
            
            return result;
        }
        
        public static string[] SplitAndTrim(this string s, char[] separators, int count)
        {
            string[] result = s.Split(separators, count);
            for (int i = 0; i < result.Length; i++)
                result[i] = result[i].Trim();
            
            return result;
        }
        
        public static string[] SplitAndTrim(this string s, char[] separators, StringSplitOptions options)
        {
            string[] result = s.Split(separators, options);
            for (int i = 0; i < result.Length; i++)
                result[i] = result[i].Trim();
            
            return result;
        }
        
        public static string[] SplitAndTrim(this string s, string[] separators, StringSplitOptions options)
        {
            string[] result = s.Split(separators, options);
            for (int i = 0; i < result.Length; i++)
                result[i] = result[i].Trim();
            
            return result;
        }
        
        public static string[] SplitAndTrim(this string s, char[] separators, int count, StringSplitOptions options)
        {
            string[] result = s.Split(separators, count, options);
            for (int i = 0; i < result.Length; i++)
                result[i] = result[i].Trim();
            
            return result;
        }
        
        public static string[] SplitAndTrim(this string s, string[] separators, int count, StringSplitOptions options)
        {
            string[] result = s.Split(separators, count, options);
            for (int i = 0; i < result.Length; i++)
                result[i] = result[i].Trim();
            
            return result;
        }
        
        /// <summary>
        /// Devuelve una matriz de cadenas que contiene las subcadenas de esta instancia que están delimitadas por los
        /// caracteres que cumplen con el predicado especificado.
        /// </summary>
        public static string[] SplitByPredicate(this string s, Predicate<char> predicate)
        {
            return StringExtensions.SplitByPredicate(s, predicate, StringSplitOptions.RemoveEmptyEntries);
        }
        
        /// <summary>
        /// Devuelve una matriz de cadenas que contiene las subcadenas de esta instancia que están delimitadas por los
        /// caracteres que cumplen con el predicado especificado.
        /// </summary>
        public static string[] SplitByPredicate(this string s, Predicate<char> predicate, StringSplitOptions options)
        {
            List<string> result = new List<string>();
            
            int startIndex = 0, length = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (predicate(s[i]))
                {
                    if (!((options == StringSplitOptions.RemoveEmptyEntries) && (length == 0)))
                        result.Add(s.Substring(startIndex, length));
                    
                    startIndex = i + 1;
                    length = 0;
                }
                else
                {
                    length++;
                }
            }
            
            if (!((options == StringSplitOptions.RemoveEmptyEntries) && (length == 0)))
                result.Add(s.Substring(startIndex, length));
            
            return result.ToArray();
        }
        
        /// <summary>
        /// Devuelve una matriz de cadenas que contiene las subcadenas de esta instancia que están delimitadas por
        /// saltos de línea.
        /// </summary>
        public static string[] SplitLines(this string s)
        {
            return StringExtensions.SplitLines(s, StringSplitOptions.RemoveEmptyEntries);
        }
        
        /// <summary>
        /// Devuelve una matriz de cadenas que contiene las subcadenas de esta instancia que están delimitadas por
        /// saltos de línea.
        /// </summary>
        public static string[] SplitLines(this string s, StringSplitOptions stringSplitOptions)
        {
            return s.Split(new string[] { Environment.NewLine }, stringSplitOptions);
        }
        
        /// <summary>
        /// Devuelve una matriz de cadenas que contiene todas las palabras de esta instancia, es decir, las subcadenas
        /// de texto que están delimitadas por uno o más espacios en blanco o signos de puntuación.
        /// </summary>
        public static string[] SplitWords(this string s)
        {
            return StringExtensions.SplitByPredicate(s, c => char.IsWhiteSpace(c) || char.IsPunctuation(c),
                StringSplitOptions.RemoveEmptyEntries);
        }
        
        // Métodos de conversión
        /// <summary>
        /// Convierte el objeto System.String actual en un número entero sin signo de 8 bits, atendiendo a su
        /// representación como cadena de texto. Si no se puede convertir, devuelve el valor cero.
        /// </summary>
        public static byte ToByte(this string s)
        {
            return StringExtensions.TryParse<byte>(s, byte.TryParse, (byte)0);
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en un número entero sin signo de 8 bits, atendiendo a su
        /// representación como cadena de texto. Si no se puede convertir, devuelve el valor especificado como
        /// alternativa.
        /// </summary>
        /// <param name="fallbackValue">Valor a devolver como alternativa, en caso de no poder convertir el objeto
        /// System.String actual.</param>
        public static byte ToByte(this string s, byte fallbackValue)
        {
            return StringExtensions.TryParse<byte>(s, byte.TryParse, fallbackValue);
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en una estructura System.DateTime que representa una fecha y hora,
        /// atendiendo a su representación como cadena de texto. Si no se puede convertir, devuelve una fecha estándar
        /// (1 de Enero del 2000, a las 0:00:00)
        /// </summary>
        public static DateTime ToDateTime(this string s)
        {
            DateTime result = new DateTime(2000, 1, 1, 0, 0, 0, 0);
            if (DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                return result;
            return new DateTime(2000, 1, 1, 0, 0, 0, 0);
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en una estructura System.DateTime que representa una fecha y hora,
        /// atendiendo a su representación como cadena de texto. Si no se puede convertir, devuelve el valor
        /// especificado como alternativa.
        /// </summary>
        /// <param name="fallbackValue">Valor a devolver como alternativa, en caso de no poder convertir el objeto
        /// System.String actual.</param>
        public static DateTime ToDateTime(this string s, DateTime fallbackValue)
        {
            DateTime result = fallbackValue;
            if (DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                return result;
            return fallbackValue;
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en un número de punto flotante de doble precisión, atendiendo a
        /// su representación como cadena de texto. Si no se puede convertir, devuelve el valor cero.
        /// </summary>
        public static double ToDouble(this string s)
        {
            return StringExtensions.TryParseDec<double>(s, double.TryParse, 0.0);
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en un número de punto flotante de doble precisión, atendiendo a
        /// su representación como cadena de texto. Si no se puede convertir, devuelve el valor especificado como
        /// alternativa.
        /// </summary>
        /// <param name="fallbackValue">Valor a devolver como alternativa, en caso de no poder convertir el objeto
        /// System.String actual.</param>
        public static double ToDouble(this string s, double fallbackValue)
        {
            return StringExtensions.TryParseDec<double>(s, double.TryParse, fallbackValue);
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en una enumeración del tipo especificado. Si no se puede
        /// convertir, devuelve el valor por defecto según el obtenido por la palabra clave default para la
        /// enumeración expecificada.
        /// </summary>
        /// <typeparam name="T">Tipo de enumeración a la que se desea convertir.</typeparam>
        public static T ToEnum<T>(this string s) where T : struct
        {
            return StringExtensions.ToEnum(s, default(T));//(T)Enum.Parse(typeof(T), s, true);
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en una enumeración del tipo especificado. Si no se puede
        /// convertir, devuelve el valor especificado como alternativa.
        /// </summary>
        /// <typeparam name="T">Tipo de enumeración a la que se desea convertir.</typeparam>
        /// <param name="fallbackValue">Valor a devolver como alternativa, en caso de no poder convertir el objeto
        /// System.String actual.</param>
        public static T ToEnum<T>(this string s, T fallbackValue) where T : struct
        {
            try
            {
                return (T)Enum.Parse(typeof(T), s, true);
            }
            catch
            {
                return fallbackValue;
            }
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en un número de punto flotante de simple precisión, atendiendo a
        /// su representación como cadena de texto. Si no se puede convertir, devuelve el valor cero.
        /// </summary>
        public static float ToFloat(this string s)
        {
            return StringExtensions.TryParseDec<float>(s, float.TryParse, 0.0f);
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en un número de punto flotante de simple precisión, atendiendo a
        /// su representación como cadena de texto. Si no se puede convertir, devuelve el valor especificado como
        /// alternativa.
        /// </summary>
        /// <param name="fallbackValue">Valor a devolver como alternativa, en caso de no poder convertir el objeto
        /// System.String actual.</param>
        public static float ToFloat(this string s, float fallbackValue)
        {
            return StringExtensions.TryParseDec<float>(s, float.TryParse, fallbackValue);
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en un número entero de 32 bits, atendiendo a su representación
        /// como cadena de texto. Si no se puede convertir, devuelve el valor cero.
        /// </summary>
        public static int ToInt(this string s)
        {
            return StringExtensions.TryParse<int>(s, int.TryParse, 0);
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en un número entero de 32 bits, atendiendo a su representación
        /// como cadena de texto. Si no se puede convertir, devuelve el valor especificado como alternativa.
        /// </summary>
        /// <param name="fallbackValue">Valor a devolver como alternativa, en caso de no poder convertir el objeto
        /// System.String actual.</param>
        public static int ToInt(this string s, int fallbackValue)
        {
            return StringExtensions.TryParse<int>(s, int.TryParse, fallbackValue);
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en un número entero de 64 bits, atendiendo a su representación
        /// como cadena de texto. Si no se puede convertir, devuelve el valor cero.
        /// </summary>
        public static long ToLong(this string s)
        {
            return StringExtensions.TryParse<long>(s, long.TryParse, 0);
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en un número entero de 64 bits, atendiendo a su representación
        /// como cadena de texto. Si no se puede convertir, devuelve el valor especificado como alternativa.
        /// </summary>
        /// <param name="fallbackValue">Valor a devolver como alternativa, en caso de no poder convertir el objeto
        /// System.String actual.</param>
        public static long ToLong(this string s, long fallbackValue)
        {
            return StringExtensions.TryParse<long>(s, long.TryParse, fallbackValue);
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en una estructura System.TimeSpan que representa una cantidad de
        /// tiempo, atendiendo a su representación como cadena de texto. Si no se puede convertir, devuelve una
        /// cantidad de tiempo igual a cero.
        /// </summary>
        public static TimeSpan ToTimeSpan(this string s)
        {
            return StringExtensions.TryParse<TimeSpan>(s, TimeSpan.TryParse, TimeSpan.Zero);
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en una estructura System.TimeSpan que representa una cantidad de
        /// tiempo, atendiendo a su representación como cadena de texto. Si no se puede convertir, devuelve el valor
        /// especificado como alternativa.
        /// </summary>
        /// <param name="fallbackValue">Valor a devolver como alternativa, en caso de no poder convertir el objeto
        /// System.String actual.</param>
        public static TimeSpan ToTimeSpan(this string s, TimeSpan fallbackValue)
        {
            return StringExtensions.TryParse<TimeSpan>(s, TimeSpan.TryParse, fallbackValue);
        }
        
        // Métodos de información sobre conversión
        /// <summary>
        /// Determina si el objeto System.String actual se podría convertir en un número entero sin signo de 8 bits.
        /// </summary>
        public static bool IsByte(this string s)
        {
            return StringExtensions.IsParseable<byte>(s, byte.TryParse);
        }
        
        /// <summary>
        /// Determina si el objeto System.String actual se podría convertir en un objeto System.DateTime.
        /// </summary>
        public static bool IsDateTime(this string s)
        {
            DateTime result = new DateTime(2000, 1, 1, 0, 0, 0, 0);
            if (DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                return true;
            return false;
        }
        
        /// <summary>
        /// Determina si el objeto System.String actual se podría convertir en un número de punto flotante de doble
        /// precisión.
        /// </summary>
        public static bool IsDouble(this string s)
        {
            return StringExtensions.IsParseableDec<double>(s, double.TryParse);
        }
        
        /// <summary>
        /// Determina si el objeto System.String actual se podría convertir en una enumeración del tipo especificado.
        /// </summary>
        public static bool IsEnum<T>(this string s) where T : struct
        {
            try
            {
                Enum.Parse(typeof(T), s, true);
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        /// <summary>
        /// Determina si el objeto System.String actual se podría convertir en un número de punto flotante de simple
        /// precisión.
        /// </summary>
        public static bool IsFloat(this string s)
        {
            return StringExtensions.IsParseableDec<float>(s, float.TryParse);
        }
        
        /// <summary>
        /// Determina si el objeto System.String actual se podría convertir en un número entero de 32 bits.
        /// </summary>
        public static bool IsInt(this string s)
        {
            return StringExtensions.IsParseable<int>(s, int.TryParse);
        }
        
        /// <summary>
        /// Determina si el objeto System.String actual se podría convertir en un número entero de 64 bits.
        /// </summary>
        public static bool IsLong(this string s)
        {
            return StringExtensions.IsParseable<long>(s, long.TryParse);
        }
        
        /// <summary>
        /// Determina si el objeto System.String actual se podría convertir en un objeto System.TimeSpan.
        /// </summary>
        public static bool IsTimeSpan(this string s)
        {
            return StringExtensions.IsParseable<TimeSpan>(s, TimeSpan.TryParse);
        }
        
        // Métodos auxiliares
        private static T TryParse<T>(string s, ParsingFunc<T> parsingFunc, T defaultValue) where T : struct
        {
            T result = defaultValue;
            if (parsingFunc(s, out result))
                return result;
            return defaultValue;
        }
        
        private static T TryParseDec<T>(string s, ParsingFuncDec<T> parsingFunc, T defaultValue) where T : struct
        {
            T result = defaultValue;
            if (parsingFunc(s, NumberStyles.Float, CultureInfo.InvariantCulture, out result))
                return result;
            return defaultValue;
        }
        
        private static bool IsParseable<T>(string s, ParsingFunc<T> parsingFunc) where T : struct
        {
            T result = default(T);
            if (parsingFunc(s, out result))
                return true;
            return false;
        }
        
        private static bool IsParseableDec<T>(string s, ParsingFuncDec<T> parsingFunc) where T : struct
        {
            T result = default(T);
            if (parsingFunc(s, NumberStyles.Float, CultureInfo.InvariantCulture, out result))
                return true;
            return false;
        }
    }
    
    public static class StringUtil
    {
        /// <summary>
        /// Dtermina si dos cadenas de texto son anagramas una de la otra.
        /// </summary>
        public static bool Anagram(string s1, string s2)
        {
            char[] c1 = s1.ToUpperInvariant().ToCharArray();
            char[] c2 = s2.ToUpperInvariant().ToCharArray();
            
            Array.Sort(c1);
            Array.Sort(c2);
            
            return new string(c1) == new string(c2);
        }
        
        /// <summary>
        /// Devuelve la distancia de edición existente entre las dos cadenas de texto especificadas, que se define como
        /// el número mínimo de de inserciones, eliminaciones o reemplazos de caracteres que hay que realizar para
        /// convertir una de las cadenas de texto en la otra.
        /// </summary>
        public static int EditDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];
            
            // Si una de las dos cadenas es vacía, devolver la longitud de la otra
            if (n == 0) return m;
            if (m == 0) return n;
            
            for (int i = 0; i <= n; d[i, 0] = i++);
            for (int j = 0; j <= m; d[0, j] = j++);
            
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
                }
            }
            
            return d[n, m];
        }
        
        /// <summary>
        /// Genera una cadena de texto aleatoria con la longitud especificada y el número de palabras especificado.
        /// </summary>
        /// <param name="length">Longitud de la cadena de texto generada.</param>
        /// <param name="wordCount">Número de palabras.</param>
        public static string Placeholder(int length, int wordCount)
        {
            Random r = new Random();
            char[] result = new char[length];
            
            for (int i = 0; i < length; i++)
                result[i] = (char)r.Next(48, 125);
            
            for (int i = 0; i < wordCount - 1; i++)
                result[r.Next(length)] = ' ';
            
            return new string(result);
        }
    }
    
}