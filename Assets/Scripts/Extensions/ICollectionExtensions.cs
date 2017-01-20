using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Extensions.System.Colections
{
    public static class ICollectionExtensions
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos
        // ---- ---- ---- ---- ---- ---- ---- ----
        /// <summary>
        /// Determina si System.Collections.ICollection no contiene ningún elemento.
        /// </summary>
        public static bool IsEmpty(this ICollection collection)
        {
            return collection.Count <= 0;
        }
        
        /// <summary>
        /// Determina si System.Collections.Generic.ICollection&lt;T&gt; no contiene ningún elemento.
        /// </summary>
        public static bool IsEmpty<T>(this ICollection<T> collection)
        {
            return collection.Count <= 0;
        }

        /// <summary>
        /// Devuelve un objeto System.String que representa en forma de texto cada uno de los elementos que contiene el
        /// array actual.
        /// </summary>
        public static string ItemListToString(this ICollection c, string separator)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in c)
                sb.Append(item.ToString() + separator);

            return sb.ToString(0, sb.Length - separator.Length);
        }

        /// <summary>
        /// Devuelve un objeto System.String que representa en forma de texto cada uno de los elementos que contiene el
        /// array actual.
        /// </summary>
        public static string ItemListToString(this ICollection c, string separator, string start, string end)
        {
            StringBuilder sb = new StringBuilder(start);
            foreach (var item in c)
                sb.Append(item.ToString() + separator);

            return sb.ToString(0, sb.Length - separator.Length) + end;
        }
    }
    
}