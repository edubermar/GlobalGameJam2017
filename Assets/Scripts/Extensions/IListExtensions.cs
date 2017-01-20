using System;
using System.Collections;
using System.Collections.Generic;

namespace Extensions.System.Colections
{
    public static class IListExtensions
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Atributos
        // ---- ---- ---- ---- ---- ---- ---- ----
        private static Random r = new Random();
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos
        // ---- ---- ---- ---- ---- ---- ---- ----
        /// <summary>
        /// Obtiene el elemento que se encuentra situado en la mitad de la lista, aproximadamente a la misma distancia
        /// del principio y del final.
        /// </summary>
        public static T MiddleItem<T>(this IList<T> list)
        {
            if (list.Count > 1)
                return list[list.Count / 2];
            return list.Count == 1 ? list[0] : default(T);
        }
        
        /// <summary>
        /// Saca un elemento de la lista y lo reinserta en otra posición, preservando el orden de cualquier elemento
        /// intermedio.
        /// </summary>
        /// <param name="i">Índice del elemento a mover.</param>
        /// <param name="j">Índice de destino.</param>
        public static void Move(this IList list, int fromIndex, int toIndex)
        {
            if (list.IndexInRange(fromIndex) && list.IndexInRange(toIndex))
            {
                object item = list[fromIndex];
                list.RemoveAt(fromIndex);
                list.Insert(toIndex, item);
            }
        }
        
        /// <summary>
        /// Comprueba si el índice especificado es un índice válido para esta instancia.
        /// </summary>
        /// <param name="index">Índice a comprobar.</param>
        public static bool IndexInRange(this IList list, int index)
        {
            return (index >= 0) && (index < list.Count);
        }
        
        /// <summary>
        /// Devuelve el primer elemento de esta lista, sacándolo además de la colección.
        /// </summary>
        public static T PullFirst<T>(this IList<T> list)
        {
            if (list.Count > 0)
            {
                T item = list[0];
                list.RemoveAt(0);
                return item;
            }
            else
                return default(T);
        }
        
        /// <summary>
        /// Devuelve un elemento de esta lista elegido al azar, sacándolo además de la colección.
        /// </summary>
        public static object PullRandomItem(this IList list)
        {
            int index = IListExtensions.r.Next(list.Count);
            object item = list[index];
            list.RemoveAt(index);
            return item;
        }
        
        /// <summary>
        /// Devuelve un elemento de esta lista elegido al azar, sacándolo además de la colección.
        /// </summary>
        public static T PullRandomItem<T>(this IList<T> list)
        {
            int index = IListExtensions.r.Next(list.Count);
            T item = list[index];
            list.RemoveAt(index);
            return item;
        }
        
        /// <summary>
        /// Devuelve un índice válido para esta instancia elegido al azar.
        /// </summary>
        public static int RandomIndex(this IList list)
        {
            return IListExtensions.r.Next(list.Count);
        }
        
        /// <summary>
        /// Devuelve un elemento de esta lista elegido al azar, sin sacarlo de la colección.
        /// </summary>
        public static object RandomItem(this IList list)
        {
            if (list.Count > 0)
                return list[IListExtensions.r.Next(list.Count)];
            
            return default(object);
        }
        
        /// <summary>
        /// Devuelve un elemento de esta lista elegido al azar, sin sacarlo de la colección.
        /// </summary>
        public static T RandomItem<T>(this IList<T> list)
        {
            if (list.Count > 0)
                return list[IListExtensions.r.Next(list.Count)];
            
            return default(T);
        }
        
        /// <summary>
        /// Mueve todos los elementos de esta instancia una posición hacia atrás, y recoloca el primer elemento en
        /// último lugar.
        /// </summary>
        public static void RotateToLeft(this IList list)
        {
            object item = list[0];
            list.RemoveAt(0);
            list.Insert(list.Count, item);
        }
        
        /// <summary>
        /// Mueve todos los elementos de esta instancia una posición hacia delante, y recoloca el último elemento en
        /// primer lugar.
        /// </summary>
        public static void RotateToRight(this IList list)
        {
            object item = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
            list.Insert(0, item);
        }
        
        /// <summary>
        /// Reordena los elementos de esta lista al azar.
        /// </summary>
        public static void Shuffle(this IList list)
        {
            for (int i = list.Count; i > 0; i--)
            {
                int destination = IListExtensions.r.Next(i);
                object value = list[destination];
                list[destination] = list[i - 1];
                list[i - 1] = value;
            }
        }
        
        /// <summary>
        /// Intercambia la posición de dos elementos de esta lista según sus índices.
        /// </summary>
        /// <param name="i">Índice del primer elemento.</param>
        /// <param name="j">Índice del segundo elemento.</param>
        public static void Swap(this IList list, int i, int j)
        {
            object value = list[i];
            list[i] = list[j];
            list[j] = value;
        }
    }
    
}