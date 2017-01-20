#if !(UNITY_5 || UNITY_4)
using System;

namespace Extensions.System.Forms
{
    public sealed class ListControlValue<T>
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Campos
        // ---- ---- ---- ---- ---- ---- ---- ----
        private T value;
        private string description;
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Propiedades
        // ---- ---- ---- ---- ---- ---- ---- ----
        public T Value { get { return this.value; } }
        public string Description { get { return this.description; } }
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Constructores
        // ---- ---- ---- ---- ---- ---- ---- ----
        public ListControlValue(T value, string description)
        {
            this.value = value;
            this.description = description;
        }
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos
        // ---- ---- ---- ---- ---- ---- ---- ----
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
        
        public override string ToString()
        {
            return this.description;
        }
    }
    
}
#endif