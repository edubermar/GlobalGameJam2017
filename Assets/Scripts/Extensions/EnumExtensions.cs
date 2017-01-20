using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Extensions.System
{
    public static class EnumExtensions
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos de atributos
        public static string GetDescription(this Enum e)
        {
            Type type = e.GetType();
            MemberInfo[] memInfo = type.GetMember(e.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return e.ToString();
        }
        
        // Métodos de gestión de banderas
        public static T ClearFlag<T>(this Enum e, T flag)
        {
            return ClearFlags(e, flag);
        }
        
        public static T ClearFlags<T>(this Enum e, params T[] flags)
        {
            var result = Convert.ToUInt64(e);
            foreach (T flag in flags)
                result &= ~Convert.ToUInt64(flag);
            return (T)Enum.Parse(e.GetType(), result.ToString());
        }
        
        public static T SetFlag<T>(this Enum e, T flag)
        {
            return SetFlags(e, flag);
        }
        
        public static T SetFlags<T>(this Enum e, params T[] flags)
        {
            var result = Convert.ToUInt64(e);
            foreach (T flag in flags)
                result |= Convert.ToUInt64(flag);
            return (T)Enum.Parse(e.GetType(), result.ToString());
        }
        
        public static bool HasFlag<E>(this E e, E flag)
            where E : struct, IComparable, IFormattable, IConvertible
        {
            if (!typeof(E).IsEnum)
                throw new ArgumentException("e must be an Enum", "e");
            
            if (!Enum.IsDefined(typeof(E), flag))
                return false;
            
            ulong numFlag = Convert.ToUInt64(flag);
            if ((Convert.ToUInt64(e) & numFlag) != numFlag)
                return false;
            
            return true;
        }
        
        public static bool HasFlags<E>(this E e, params E[] flags)
            where E : struct, IComparable, IFormattable, IConvertible
        {
            if (!typeof(E).IsEnum)
                throw new ArgumentException("e must be an Enum", "e");
            
            foreach (var flag in flags)
            {
                if (!Enum.IsDefined(typeof(E), flag))
                    return false;
            
                ulong numFlag = Convert.ToUInt64(flag);
                if ((Convert.ToUInt64(e) & numFlag) != numFlag)
                    return false;
            }
            
            return true;
        }
    }
    
    public static class EnumUtil
    {
        public static int ItemCount<T>() where T : struct, IComparable, IFormattable, IConvertible
        {
            // Check if base type is System.Enum
            if (typeof(T).BaseType != typeof(Enum))
                throw new ArgumentException("T must be of type System.Enum");
            
            return Enum.GetNames(typeof(T)).Length;
        }
        
        public static IEnumerable<T> ItemList<T>() where T : struct, IComparable, IFormattable, IConvertible
        {
            Type enumType = typeof(T);
            
            // Check if base type is System.Enum
            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T must be of type System.Enum");
            
            Array enumValArray = Enum.GetValues(enumType);
            List<T> enumValList = new List<T>(enumValArray.Length);
            
            foreach (int val in enumValArray)
            {
                enumValList.Add((T)Enum.Parse(enumType, val.ToString()));
            }
            
            return enumValList;
        }
    }
    
}