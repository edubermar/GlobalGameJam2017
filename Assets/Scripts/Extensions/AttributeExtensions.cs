using System;
using System.Collections.Generic;

namespace Extensions.System
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public sealed class LinkedStringAttribute : Attribute
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Campos
        // ---- ---- ---- ---- ---- ---- ---- ----
        private string value;
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Propiedades
        // ---- ---- ---- ---- ---- ---- ---- ----
        public string Value
        {
            get { return this.value; }
        }
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Constructores
        // ---- ---- ---- ---- ---- ---- ---- ----
        public LinkedStringAttribute(string value)
        {
            this.value = value;
        }
    }
    
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public sealed class LinkedIntegerAttribute : Attribute
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Campos
        // ---- ---- ---- ---- ---- ---- ---- ----
        private int value;
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Propiedades
        // ---- ---- ---- ---- ---- ---- ---- ----
        public int Value
        {
            get { return this.value; }
        }
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Constructores
        // ---- ---- ---- ---- ---- ---- ---- ----
        public LinkedIntegerAttribute(int value)
        {
            this.value = value;
        }
    }
    
    /*
    public static class ReflectionUtil
    {
        public static void CallMethodsByStringAttribute<T>(T instance, string s)
        {
            var methodList = instance.GetType().GetMethods();
            foreach (var item in methodList)
            {
                var attributeList = item.GetCustomAttributes(typeof(LinkedStringAttribute), false);
                if (attributeList != null && attributeList.Length > 0)
                {
                    var attribute = (LinkedStringAttribute)attributeList[0];
                    if (attribute.Value == s)
                    {
                        List<object> parameters = new List<object>();
                        foreach (var parameter in item.GetParameters())
                        {
                            parameters.Add(parameter.ParameterType.de)
                        }
                        item.
                    }
                }
            }
        }
    }
    */
    
}