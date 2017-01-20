using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Extensions.System
{
    public static class ObjectExtensions
    {
        // Métodos de comparación
        /// <summary>
        /// Determina si el objeto actual es igual a al menos uno de los objetos suministrados como parámetros.
        /// </summary>
        public static bool EqualsAny<T>(this T obj, params T[] values)
        {
            return Array.IndexOf(values, obj) != -1;
        }
        
        /// <summary>
        /// Determina si el objeto actual es diferente a cualquiera de los objetos suministrados como parámetros.
        /// </summary>
        public static bool EqualsNone<T>(this T obj, params T[] values)
        {
            return ObjectExtensions.EqualsAny(obj, values) == false;
        }
        
        // Métodos de reflexión
        /// <summary>
        /// Invoca un método a partir de su nombre mediante reflexión.
        /// </summary>
        public static object InvokeMethod(this object obj, string methodName, params object[] parameters)
        {
            return InvokeMethod<object>(obj, methodName, parameters);
        }
        
        /// <summary>
        /// Invoca un método a partir de su nombre mediante reflexión, y obtiene el valor de retorno.
        /// </summary>
        public static T InvokeMethod<T>(this object obj, string methodName, params object[] parameters)
        {
            var type = obj.GetType();
            var parameterTypes = new Type[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
                parameterTypes[i] = parameters[i].GetType();
            
            var method = type.GetMethod(methodName, parameterTypes);
            
            if (method == null)
                throw new ArgumentException(string.Format("Method '{0}' not found.", methodName), methodName);
            
            var value = method.Invoke(obj, parameters);
            return (value is T) ? (T)value : default(T);
        }
        
        /// <summary>
        /// Obtiene el valor de una propiedad a partir de su nombre mediante reflexión.
        /// </summary>
        public static object GetPropertyValue(this object obj, string propertyName)
        {
            return GetPropertyValue<object>(obj, propertyName, null);
        }
        
        /// <summary>
        /// Obtiene el valor de una propiedad a partir de su nombre mediante reflexión.
        /// </summary>
        public static T GetPropertyValue<T>(this object obj, string propertyName)
        {
            return GetPropertyValue(obj, propertyName, default(T));
        }
        
        /// <summary>
        /// Obtiene el valor de una propiedad a partir de su nombre mediante reflexión.
        /// </summary>
        public static T GetPropertyValue<T>(this object obj, string propertyName, T defaultValue)
        {
            var type = obj.GetType();
            var property = type.GetProperty(propertyName);
            
            if (property == null)
                throw new ArgumentException(string.Format("Property '{0}' not found.", propertyName), propertyName);
            
            var value = property.GetValue(obj, null);
            return (value is T ? (T)value : defaultValue);
        }
        
        // Otros métodos
        /// <summary>
        /// Realiza una copia profunda del objeto actual.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static T Clone<T>(this T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }
            
            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }
            
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }
    }
    
}