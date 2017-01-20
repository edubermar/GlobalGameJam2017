using System;
using System.Globalization;

#if (UNITY_5 || UNITY_4)
    using Color = UnityEngine.Color;
#else
    using Color = System.Drawing.Color;
#endif

namespace Extensions.UnityEngine
{
    public static class ColorExtensions
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos
        // ---- ---- ---- ---- ---- ---- ---- ----
        #if (UNITY_5 || UNITY_4)
        /// <summary>
        /// Devuelve el tono del color.
        /// </summary>
        public static float GetHue(this Color color)
        {
                float h, s, v;
                Color.RGBToHSV(color, out h, out s, out v);
                return h;
        }
        
        /// <summary>
        /// Devuelve la saturación del color.
        /// </summary>
        public static float GetSaturation(this Color color)
        {
            float h, s, v;
            Color.RGBToHSV(color, out h, out s, out v);
            return s;
        }
        
        /// <summary>
        /// Devuelve la luminosidad del color.
        /// </summary>
        public static float GetValue(this Color color)
        {
            float h, s, v;
            Color.RGBToHSV(color, out h, out s, out v);
            return v;
        }
        #endif
        
        /// <summary>
        /// Devuelve un color del mismo tono aplicando un incremento de brillo del 25%.
        /// </summary>
        public static Color Lighter(this Color color)
        {
            #if (UNITY_5 || UNITY_4)
                return new Color(color.r + (1.0f - color.r) * 0.25f,
                                 color.g + (1.0f - color.g) * 0.25f,
                                 color.b + (1.0f - color.b) * 0.25f,
                                 color.a);
            #else
                return Color.FromArgb((int)color.A,
                                      (int)(color.R + (1.0f - color.R) * 0.25f),
                                      (int)(color.G + (1.0f - color.G) * 0.25f),
                                      (int)(color.B + (1.0f - color.B) * 0.25f));
            #endif
        }
        
        /// <summary>
        /// Devuelve un color del mismo tono aplicando un incremento de brillo con el factor especificado. Un factor
        /// igual o menor que cero no alterará en absoluto el color, mientras que un factor igual o superior a uno lo
        /// convertirá en blanco puro. Los valores intermedios interpolan el efecto descrito.
        /// </summary>
        public static Color Lighter(this Color color, float factor)
        {
            factor = factor < 0.0f ? 0.0f : (factor > 1.0f ? 1.0f : factor);
            #if (UNITY_5 || UNITY_4)
                return new Color(color.r + (1.0f - color.r) * factor,
                                 color.g + (1.0f - color.g) * factor,
                                 color.b + (1.0f - color.b) * factor,
                                 color.a);
            #else
                return Color.FromArgb((int)color.A,
                                      (int)(color.R + (1.0f - color.R) * factor),
                                      (int)(color.G + (1.0f - color.G) * factor),
                                      (int)(color.B + (1.0f - color.B) * factor));
            #endif
        }
        
        /// <summary>
        /// Devuelve un color del mismo tono aplicando una reducción de brillo del 25%.
        /// </summary>
        public static Color Darker(this Color color)
        {
            #if (UNITY_5 || UNITY_4)
                return new Color(color.r * 0.75f, color.g * 0.75f, color.b * 0.75f, color.a);
            #else
                return Color.FromArgb((int)color.A,
                                      (((int)color.R) * 3) / 4,
                                      (((int)color.G) * 3) / 4,
                                      (((int)color.B) * 3) / 4);
            #endif
        }
        
        /// <summary>
        /// Devuelve un color del mismo tono aplicando una reducción de brillo con el factor especificado. Un factor
        /// igual o menor que cero no alterará en absoluto el color, mientras que un factor igual o superior a uno lo
        /// convertirá en negro puro. Los valores intermedios interpolan el efecto descrito.
        /// </summary>
        public static Color Darker(this Color color, float factor)
        {
            factor = factor < 0.0f ? 0.0f : (factor > 1.0f ? 1.0f : factor);
            #if (UNITY_5 || UNITY_4)
                return new Color(color.r * factor, color.g * factor, color.b * factor, color.a);
            #else
                return Color.FromArgb((int)color.A,
                                      (int)(((float)color.R) * factor),
                                      (int)(((float)color.G) * factor),
                                      (int)(((float)color.B) * factor));
            #endif
        }
        
        #if (UNITY_5 || UNITY_4)
        /// <summary>
        /// Devuelve el valor de brillo general de esta instancia, según el valor medio de los tres componentes.
        /// </summary>
        public static float Brightness(this Color color)
        {
            return (color.r + color.g + color.b) * 0.33333333f;
        }
        
        /// <summary>
        /// Devuelve el valor de brillo general de esta instancia, según un esquema perceptual que tiene en cuenta la
        /// diferente sensibilidad que tiene el ojo humano a los diferentes tonos de color.
        /// </summary>
        public static float BrightnessPerceptual(this Color color)
        {
            return (color.r * 0.2126f) + (color.g * 0.7152f) + (color.b * 0.0722f);
        }
        #endif
        
        /// <summary>
        /// Devuelve una versión del mismo tono de color pero con el alfa especificado.
        /// </summary>
        public static Color WithAlpha(this Color color, float alpha)
        {
            #if (UNITY_5 || UNITY_4)
                return new Color(color.r, color.g, color.b, alpha);
            #else
                return Color.FromArgb((int)(alpha * 255.0f), color);
            #endif
        }
        
        #if (UNITY_5 || UNITY_4)
        /// <summary>
        /// Devuelve una versión del mismo tono de color pero con el nivel de brillo especificado.
        /// </summary>
        public static Color WithBrightness(this Color color, float brightness)
        {
            if (color.Brightness() < 0.001f)
                return new Color(brightness, brightness, brightness, color.a);
            
            float factor = brightness / color.Brightness();
            
            float r = color.r * factor;
            float g = color.g * factor;
            float b = color.b * factor;
            
            float a = color.a;
            
            return new Color(r, g, b, a);
        }
        
        /// <summary>
        /// Devuelve una versión del mismo tono de color pero con el nivel de brillo perceprual especificado.
        /// </summary>
        public static Color WithBrightnessPerceptual(this Color color, float brightnessPerceptual)
        {
            if (color.BrightnessPerceptual() < 0.001f)
                return new Color(brightnessPerceptual, brightnessPerceptual, brightnessPerceptual, color.a);
            
            float factor = brightnessPerceptual / color.BrightnessPerceptual();
            
            float r = color.r * factor;
            float g = color.g * factor;
            float b = color.b * factor;
            
            float a = color.a;
            
            return new Color(r, g, b, a);
        }
        #endif
        
        /// <summary>
        /// Devuelve una versión del mismo tono de color sin ningún tipo de transparencia.
        /// </summary>
        public static Color Opaque(this Color color)
        {
            #if (UNITY_5 || UNITY_4)
                return new Color(color.r, color.g, color.b);
            #else
                return Color.FromArgb(255, color);
            #endif
        }
        
        // Metodos de conversión
        /// <summary>
        /// Convierte el objeto System.String actual en un objeto UnityEngine.Color, atendiendo a su representación en
        /// hexadecimal. La respresentación hexadecimal deberá tener al menos una longitud de 6, en cuyo caso se
        /// interpretarán los valores de los componentes R, G, y B del color. Si tiene dos caracteres más se intentará
        /// además obtener el componente alfa del color. La cadena de texto podrá empezar opcionalmente por "0x" o por
        /// "#" (estos prefijos serán simplemente ignorados). Si no se puede realizar la conversión, se devuelve el
        /// color blanco por defecto.
        /// </summary>
        public static Color ToColorFromHex(this string s)
        {
            #if (UNITY_5 || UNITY_4)
                return ColorExtensions.ToColorFromHex(s, Color.white);
            #else
                return ColorExtensions.ToColorFromHex(s, Color.White);
            #endif
        }
        
        /// <summary>
        /// Convierte el objeto System.String actual en un objeto UnityEngine.Color, atendiendo a su representación en
        /// hexadecimal. La respresentación hexadecimal deberá tener al menos una longitud de 6, en cuyo caso se
        /// interpretarán los valores de los componentes R, G, y B del color. Si tiene dos caracteres más se intentará
        /// además obtener el componente alfa del color. La cadena de texto podrá empezar opcionalmente por "0x" o por
        /// "#" (estos prefijos serán simplemente ignorados). Si no se puede realizar la conversión, se devuelve el
        /// color especificado como alternativa.
        /// </summary>
        public static Color ToColorFromHex(this string s, Color fallback)
        {
            // Eliminación de prefijos comunes de definición de colores en hexadecimal
            s = s.Replace("0x", string.Empty);
            s = s.Replace("#", string.Empty);
            
            #if (UNITY_5 || UNITY_4)
                // Convertir color de respaldo a bytes
                global::UnityEngine.Color32 fallback32 = (global::UnityEngine.Color32)fallback;
                
                // Intentar conversión
                if (s.Length >= 6)
                {
                    byte a = byte.MaxValue;
                    byte r = ColorExtensions.TryParseByte(s.Substring(0, 2), fallback32.r);
                    byte g = ColorExtensions.TryParseByte(s.Substring(2, 2), fallback32.g);
                    byte b = ColorExtensions.TryParseByte(s.Substring(4, 2), fallback32.b);
                
                    if (s.Length > 7)
                        a = ColorExtensions.TryParseByte(s.Substring(6, 2), byte.MaxValue);
                
                    return new global::UnityEngine.Color32(r, g, b, a);
                }
                else
                    return fallback;
            #else   
            // Intentar conversión
                if (s.Length >= 6)
                {
                    byte a = byte.MaxValue;
                    byte r = ColorExtensions.TryParseByte(s.Substring(0, 2), fallback.R);
                    byte g = ColorExtensions.TryParseByte(s.Substring(2, 2), fallback.G);
                    byte b = ColorExtensions.TryParseByte(s.Substring(4, 2), fallback.B);
                    
                    if (s.Length > 7)
                        a = ColorExtensions.TryParseByte(s.Substring(6, 2), byte.MaxValue);
                    
                    return Color.FromArgb(a, r, g, b);
                }
                else
                    return fallback;
            #endif
        }
        
        // Métodos auxiliares
        private static byte TryParseByte(string s, byte defaultValue)
        {
            byte result = defaultValue;
            if (byte.TryParse(s, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out result))
                return result;
            return defaultValue;
        }
    }
    
    #if !(UNITY_5 || UNITY_4)
    public static class ColorUtil
    {
        public static Color Lerp(Color start, Color end, float parameter)
        {
            int a = (int)(((1.0f - parameter) * start.A) + (parameter * end.A));
            int r = (int)(((1.0f - parameter) * start.R) + (parameter * end.R));
            int g = (int)(((1.0f - parameter) * start.G) + (parameter * end.G));
            int b = (int)(((1.0f - parameter) * start.B) + (parameter * end.B));
            
            return Color.FromArgb(a, r, g, b);
        }
    }
    #endif
    
}