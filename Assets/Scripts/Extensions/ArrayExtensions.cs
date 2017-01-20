using System;
using System.Collections.Generic;
using System.Text;

namespace Extensions.System
{
    public static class ArrayExtensions
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos
        // ---- ---- ---- ---- ---- ---- ---- ----
        public static void Fill(this Array array, object o)
        {
            for (int i = 0; i < array.Length; i++)
                array.SetValue(o, i);
        }
        
        /// <summary>
        /// Devuelve un objeto System.String que representa en forma de texto cada uno de los elementos que contiene el
        /// array actual.
        /// </summary>
        public static string ItemListToString(this Array array, string separator)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in array)
                sb.Append(item.ToString() + separator);
            
            return sb.ToString(0, sb.Length - separator.Length);
        }
        
        /// <summary>
        /// Devuelve un objeto System.String que representa en forma de texto cada uno de los elementos que contiene el
        /// array actual.
        /// </summary>
        public static string ItemListToString(this Array array, string separator, string start, string end)
        {
            StringBuilder sb = new StringBuilder(start);
            foreach (var item in array)
                sb.Append(item.ToString() + separator);
            
            return sb.ToString(0, sb.Length - separator.Length) + end;
        }
        
        /// <summary>
        /// Devuelve una versión de una sola dimensión de un array multidimensional.
        /// </summary>
        public static T[] Flatten<T>(this Array array)
        {
            List<T> result = new List<T>(array.Length);
            
            int[] indices = new int[array.Rank];
            for (int i = 0; i < indices.Length; i++)
                indices[i] = array.GetLowerBound(i);
            
            bool end = false;
            while (!end)
            {
                result.Add((T)array.GetValue(indices));
                
                indices[array.Rank - 1]++;
                for (int i = indices.Length - 1; i >= 0; i--)
                {
                    if ((i > 0) && (indices[i] > array.GetUpperBound(i)))
                    {
                        indices[i] = 0;
                        indices[i - 1]++;
                    }
                }
                
                if (indices[0] > array.GetUpperBound(0))
                    end = true;
            }
            
            return result.ToArray();
        }
        
        /// <summary>
        /// Determina si al menos uno de los índices especificados se enuentra en el extremo inferior o superior del
        /// rango de índices válidos de este array.
        /// </summary>
        public static bool IndexInBorder(this Array array, params int[] indexes)
        {
            for (int i = 0; i < array.Rank; i++)
            {
                if ((indexes[i] == array.GetLowerBound(i)) || (indexes[i] == array.GetUpperBound(i)))
                    return true;
            }
            return false;
        }
        
        /// <summary>
        /// Determina si todos los índices especificados se enuentran en el extremo inferior o superior del rango de
        /// índices válidos de este array.
        /// </summary>
        public static bool IndexInCorner(this Array array, params int[] indexes)
        {
            for (int i = 0; i < array.Rank; i++)
            {
                if ((indexes[i] != array.GetLowerBound(i)) && (indexes[i] != array.GetUpperBound(i)))
                    return false;
            }
            return true;
        }
        
        /// <summary>
        /// Determina si todos los índices especificados se enuentran dentro del rango de índices válidos de este
        /// array.
        /// </summary>
        public static bool IndexInRange(this Array array, params int[] indexes)
        {
            for (int i = 0; i < array.Rank; i++)
            {
                if ((indexes[i] < array.GetLowerBound(i)) || (indexes[i] > array.GetUpperBound(i)))
                    return false;
            }
            return true;
        }
        
        /// <summary>
        /// Devuelve un índice válido para esta instancia elegido al azar.
        /// </summary>
        public static int RandomIndex(this Array array)
        {
            Random r = new Random();
            return r.Next(array.Length);
        }
    }
    
}