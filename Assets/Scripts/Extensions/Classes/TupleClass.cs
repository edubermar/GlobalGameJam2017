using System;

namespace Extensions.Tuple
{
    /// <summary>
    /// Interfaz que representa una tupla de cualquier multiplicidad.
    /// </summary>
    public interface ITuple
    {
        int Size { get; }
    } 
    
    /// <summary>
    /// Clase auxiliar que simplifica la creación de tuplas a partir de métodos estáticos en lugar de constructores.
    /// </summary>
    public static class Tuple
    {
        /// <summary>
        /// Crea una nueva tupla con los elementos especificados. El método puede usarse sin especificar los parámetros
        /// de tipo genéricos, ya que el compilador de C# generalmente puede inferir los tipos automáticamente.
        /// </summary>
        /// <param name="item1">Primer elemento de la tupla.</param>
        /// <param name="item2">Segundo elemento de la tupla.</param>
        /// <returns>Una nueva tupla con los elementos especificados.</returns>
        public static Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
        {
            return new Tuple<T1, T2>(item1, item2);
        }
        
        /// <summary>
        /// Crea una nueva tupla con los elementos especificados. El método puede usarse sin especificar los parámetros
        /// de tipo genéricos, ya que el compilador de C# generalmente puede inferir los tipos automáticamente.
        /// </summary>
        /// <param name="item1">Primer elemento de la tupla.</param>
        /// <param name="item2">Segundo elemento de la tupla.</param>
        /// <param name="item3">Tercer elemento de la tupla.</param>
        /// <returns>Una nueva tupla con los elementos especificados.</returns>
        public static Tuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
        {
            return new Tuple<T1, T2, T3>(item1, item2, item3);
        }
        
        /// <summary>
        /// Crea una nueva tupla con los elementos especificados. El método puede usarse sin especificar los parámetros
        /// de tipo genéricos, ya que el compilador de C# generalmente puede inferir los tipos automáticamente.
        /// </summary>
        /// <param name="item1">Primer elemento de la tupla.</param>
        /// <param name="item2">Segundo elemento de la tupla.</param>
        /// <param name="item3">Tercer elemento de la tupla.</param>
        /// <param name="item4">Cuarto elemento de la tupla.</param>
        /// <returns>Una nueva tupla con los elementos especificados.</returns>
        public static Tuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4)
        {
            return new Tuple<T1, T2, T3, T4>(item1, item2, item3, item4);
        }
        
        /// <summary>
        /// Método de extensión que permite guardar los elementos de la tupla en parámetros pasados por referencia.
        /// </summary>
        /// <param name="tuple">La tupla que proporciona los elementos.</param>
        /// <param name="ref1">El parámetro por referencia al que se le asignará el primer elemento.</param>
        /// <param name="ref2">El parámetro por referencia al que se le asignará el segundo elemento.</param>
        public static void Unpack<T1, T2>(this Tuple<T1, T2> tuple, out T1 ref1, out T2 ref2)
        {
            ref1 = tuple.Item1;
            ref2 = tuple.Item2;
        }
        
        /// <summary>
        /// Método de extensión que permite guardar los elementos de la tupla en parámetros pasados por referencia.
        /// </summary>
        /// <param name="tuple">La tupla que proporciona los elementos.</param>
        /// <param name="ref1">El parámetro por referencia al que se le asignará el primer elemento.</param>
        /// <param name="ref2">El parámetro por referencia al que se le asignará el segundo elemento.</param>
        /// <param name="ref3">El parámetro por referencia al que se le asignará el tercer elemento.</param>
        public static void Unpack<T1, T2, T3>(this Tuple<T1, T2, T3> tuple, out T1 ref1, out T2 ref2, out T3 ref3)
        {
            ref1 = tuple.Item1;
            ref2 = tuple.Item2;
            ref3 = tuple.Item3;
        }
        
        /// <summary>
        /// Método de extensión que permite guardar los elementos de la tupla en parámetros pasados por referencia.
        /// </summary>
        /// <param name="tuple">La tupla que proporciona los elementos.</param>
        /// <param name="ref1">El parámetro por referencia al que se le asignará el primer elemento.</param>
        /// <param name="ref2">El parámetro por referencia al que se le asignará el segundo elemento.</param>
        /// <param name="ref3">El parámetro por referencia al que se le asignará el tercer elemento.</param>
        /// <param name="ref4">El parámetro por referencia al que se le asignará el cuarto elemento.</param>
        public static void Unpack<T1, T2, T3, T4>(this Tuple<T1, T2, T3, T4> tuple,
            out T1 ref1, out T2 ref2, out T3 ref3, out T4 ref4)
        {
            ref1 = tuple.Item1;
            ref2 = tuple.Item2;
            ref3 = tuple.Item3;
            ref4 = tuple.Item4;
        }
    }
    
    /// <summary>
    /// Representa una tupla de dos valores, que puede utilizarse para guardar dos valores de los tipos especificados
    /// en un solo objeto.
    /// </summary>
    /// <typeparam name="T1">El tipo del primer elemento.</typeparam>
    /// <typeparam name="T2">El tipo del segundo elemento.</typeparam>
    [Serializable]
    public sealed class Tuple<T1, T2> : ITuple
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Atributos
        // ---- ---- ---- ---- ---- ---- ---- ----
        private readonly T1 item1;
        private readonly T2 item2;
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Propiedades
        // ---- ---- ---- ---- ---- ---- ---- ----
        /// <summary>
        /// Devuelve el primer elemento de la tupla.
        /// </summary>
        public T1 Item1
        {
            get { return this.item1; }
        }
        
        /// <summary>
        /// Devuelve el segundo elemento de la tupla.
        /// </summary>
        public T2 Item2
        {
            get { return this.item2; }
        }
        
        /// <summary>
        /// Devuelve la multiplicidad de la tupla.
        /// </summary>
        public int Size
        {
            get { return 2; }
        }
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Constructores
        // ---- ---- ---- ---- ---- ---- ---- ----
        /// <summary>
        /// Crea una nueva tupla.
        /// </summary>
        /// <param name="item1">Primer elemento de la tupla.</param>
        /// <param name="item2">Segundo elemento de la tupla.</param>
        public Tuple(T1 item1, T2 item2)
        {
            this.item1 = item1;
            this.item2 = item2;
        }
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos
        // ---- ---- ---- ---- ---- ---- ---- ----
        public override bool Equals(object other)
        {
            if (other == null)
                return false;
            
            if (other is Tuple<T1, T2>)
            {
                Tuple<T1, T2> otherTuple = (Tuple<T1, T2>)other;
                return this == otherTuple;
            }
            else
                return false;
        }
        
        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + (this.item1 == null ? 0 : this.item1.GetHashCode());
            hash = hash * 23 + (this.item2 == null ? 0 : this.item2.GetHashCode());
            return hash;
        }
        
        public override string ToString()
        {
            return string.Format("({0}, {1})", this.item1.ToString(), this.item2.ToString());
        }
        
        public bool Equals(Tuple<T1, T2> other)
        {
            return this == other;
        }
        
        public static bool operator ==(Tuple<T1, T2> a, Tuple<T1, T2> b)
        {
            if (object.ReferenceEquals(a, null))
                return object.ReferenceEquals(b, null);
            
            if (a.item1 == null && b.item1 != null) return false;
            if (a.item2 == null && b.item2 != null) return false;
            
            return a.item1.Equals(b.item1) && a.item2.Equals(b.item2);
        }
        
        public static bool operator !=(Tuple<T1, T2> a, Tuple<T1, T2> b)
        {
            return !(a == b);
        }
        
        public void Unpack(Action<T1, T2> unpackerDelegate)
        {
            unpackerDelegate(this.item1, this.item2);
        }
    }
    
    /// <summary>
    /// Representa una tupla de tres valores, que puede utilizarse para guardar tres valores de los tipos especificados
    /// en un solo objeto.
    /// </summary>
    /// <typeparam name="T1">El tipo del primer elemento.</typeparam>
    /// <typeparam name="T2">El tipo del segundo elemento.</typeparam>
    /// <typeparam name="T3">El tipo del tercer elemento.</typeparam>
    [Serializable]
    public sealed class Tuple<T1, T2, T3> : ITuple
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Atributos
        // ---- ---- ---- ---- ---- ---- ---- ----
        private readonly T1 item1;
        private readonly T2 item2;
        private readonly T3 item3;
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Propiedades
        // ---- ---- ---- ---- ---- ---- ---- ----
        /// <summary>
        /// Devuelve el primer elemento de la tupla.
        /// </summary>
        public T1 Item1
        {
            get { return this.item1; }
        }
        
        /// <summary>
        /// Devuelve el segundo elemento de la tupla.
        /// </summary>
        public T2 Item2
        {
            get { return this.item2; }
        }
        
        /// <summary>
        /// Devuelve el tercer elemento de la tupla.
        /// </summary>
        public T3 Item3
        {
            get { return this.item3; }
        }
        
        /// <summary>
        /// Devuelve la multiplicidad de la tupla.
        /// </summary>
        public int Size
        {
            get { return 3; }
        }
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Constructores
        // ---- ---- ---- ---- ---- ---- ---- ----
        /// <summary>
        /// Crea una nueva tupla.
        /// </summary>
        /// <param name="item1">Primer elemento de la tupla.</param>
        /// <param name="item2">Segundo elemento de la tupla.</param>
        /// <param name="item3">Tercer elemento de la tupla.</param>
        public Tuple(T1 item1, T2 item2, T3 item3)
        {
            this.item1 = item1;
            this.item2 = item2;
            this.item3 = item3;
        }
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos
        // ---- ---- ---- ---- ---- ---- ---- ----
        public override bool Equals(object other)
        {
            if (other == null)
                return false;
            
            if (other is Tuple<T1, T2, T3>)
            {
                Tuple<T1, T2, T3> otherTuple = (Tuple<T1, T2, T3>)other;
                return this == otherTuple;
            }
            else
                return false;
        }
        
        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + (this.item1 == null ? 0 : this.item1.GetHashCode());
            hash = hash * 23 + (this.item2 == null ? 0 : this.item2.GetHashCode());
            hash = hash * 23 + (this.item3 == null ? 0 : this.item3.GetHashCode());
            return hash;
        }
        
        public override string ToString()
        {
            return string.Format("({0}, {1}, {2})",
                this.item1.ToString(), this.item2.ToString(), this.item3.ToString());
        }
        
        public bool Equals(Tuple<T1, T2, T3> other)
        {
            return this == other;
        }
        
        public static bool operator ==(Tuple<T1, T2, T3> a, Tuple<T1, T2, T3> b)
        {
            if (object.ReferenceEquals(a, null))
                return object.ReferenceEquals(b, null);
            
            if (a.item1 == null && b.item1 != null) return false;
            if (a.item2 == null && b.item2 != null) return false;
            if (a.item3 == null && b.item3 != null) return false;
            
            return a.item1.Equals(b.item1) && a.item2.Equals(b.item2) && a.item3.Equals(b.item3);
        }
        
        public static bool operator !=(Tuple<T1, T2, T3> a, Tuple<T1, T2, T3> b)
        {
            return !(a == b);
        }
        
        public void Unpack(Action<T1, T2, T3> unpackerDelegate)
        {
            unpackerDelegate(this.item1, this.item2, this.item3);
        }
    }
    
    /// <summary>
    /// Representa una tupla de cuatro valores, que puede utilizarse para guardar cuatro valores de los tipos
    /// especificados en un solo objeto.
    /// </summary>
    /// <typeparam name="T1">El tipo del primer elemento.</typeparam>
    /// <typeparam name="T2">El tipo del segundo elemento.</typeparam>
    /// <typeparam name="T3">El tipo del tercer elemento.</typeparam>
    /// <typeparam name="T4">El tipo del cuarto elemento.</typeparam>
    [Serializable]
    public sealed class Tuple<T1, T2, T3, T4> : ITuple
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Atributos
        // ---- ---- ---- ---- ---- ---- ---- ----
        private readonly T1 item1;
        private readonly T2 item2;
        private readonly T3 item3;
        private readonly T4 item4;
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Propiedades
        // ---- ---- ---- ---- ---- ---- ---- ----
        /// <summary>
        /// Devuelve el primer elemento de la tupla.
        /// </summary>
        public T1 Item1
        {
            get { return this.item1; }
        }
        
        /// <summary>
        /// Devuelve el segundo elemento de la tupla.
        /// </summary>
        public T2 Item2
        {
            get { return this.item2; }
        }
        
        /// <summary>
        /// Devuelve el tercer elemento de la tupla.
        /// </summary>
        public T3 Item3
        {
            get { return this.item3; }
        }
        
        /// <summary>
        /// Devuelve el cuarto elemento de la tupla.
        /// </summary>
        public T4 Item4
        {
            get { return this.item4; }
        }
        
        /// <summary>
        /// Devuelve la multiplicidad de la tupla.
        /// </summary>
        public int Size
        {
            get { return 4; }
        }
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Constructores
        // ---- ---- ---- ---- ---- ---- ---- ----
        /// <summary>
        /// Crea una nueva tupla.
        /// </summary>
        /// <param name="item1">Primer elemento de la tupla.</param>
        /// <param name="item2">Segundo elemento de la tupla.</param>
        /// <param name="item3">Tercer elemento de la tupla.</param>
        /// <param name="item4">Cuarto elemento de la tupla.</param>
        public Tuple(T1 item1, T2 item2, T3 item3, T4 item4)
        {
            this.item1 = item1;
            this.item2 = item2;
            this.item3 = item3;
            this.item4 = item4;
        }
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos
        // ---- ---- ---- ---- ---- ---- ---- ----
        public override bool Equals(object other)
        {
            if (other == null)
                return false;
            
            if (other is Tuple<T1, T2, T3, T4>)
            {
                Tuple<T1, T2, T3, T4> otherTuple = (Tuple<T1, T2, T3, T4>)other;
                return this == otherTuple;
            }
            else
                return false;
        }
        
        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + (this.item1 == null ? 0 : this.item1.GetHashCode());
            hash = hash * 23 + (this.item2 == null ? 0 : this.item2.GetHashCode());
            hash = hash * 23 + (this.item3 == null ? 0 : this.item3.GetHashCode());
            hash = hash * 23 + (this.item4 == null ? 0 : this.item4.GetHashCode());
            return hash;
        }
        
        public override string ToString()
        {
            return string.Format("({0}, {1}, {2}, {3})",
                this.item1.ToString(), this.item2.ToString(), this.item3.ToString(), this.item4.ToString());
        }
        
        public bool Equals(Tuple<T1, T2, T3, T4> other)
        {
            return this == other;
        }
        
        public static bool operator ==(Tuple<T1, T2, T3, T4> a, Tuple<T1, T2, T3, T4> b)
        {
            if (object.ReferenceEquals(a, null))
                return object.ReferenceEquals(b, null);
            
            if (a.item1 == null && b.item1 != null) return false;
            if (a.item2 == null && b.item2 != null) return false;
            if (a.item3 == null && b.item3 != null) return false;
            if (a.item4 == null && b.item4 != null) return false;
            
            return a.item1.Equals(b.item1) && a.item2.Equals(b.item2) &&
                   a.item3.Equals(b.item3) && a.item4.Equals(b.item4);
        }
        
        public static bool operator !=(Tuple<T1, T2, T3, T4> a, Tuple<T1, T2, T3, T4> b)
        {
            return !(a == b);
        }
        
        public void Unpack(Action<T1, T2, T3, T4> unpackerDelegate)
        {
            unpackerDelegate(this.item1, this.item2, this.item3, this.item4);
        }
    }
    
}