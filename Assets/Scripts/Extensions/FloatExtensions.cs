using System;

namespace Extensions.System
{
    public static class FloatExtensions
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos
        // ---- ---- ---- ---- ---- ---- ---- ----
        /// <summary>
        /// Restringe el valor de esta instancia a un intervalo de valores, devolviendo el valor de uno de los límites
        /// cuando el valor está fuera del rango.
        /// </summary>
        /// <param name="min">Límite inferior del intervalo.</param>
        /// <param name="max">Límite superior del intervalo.</param>
        public static float ClampTo(this float f, float min, float max)
        {
            if (min < max)
                return (f < min) ? min : (f > max ? max : f);
            return (f < max) ? max : (f > min ? min : f);
        }
        
        /// <summary>
        /// Devuelve el valor de esta instancia si es positivo, o cero en otro caso.
        /// </summary>
        public static float ClampToPositive(this float f)
        {
            return (f > 0.0f) ? f : 0.0f;
        }
        
        /// <summary>
        /// Restringe el valor de esta instancia al intervalo [0.0, 1.0].
        /// </summary>
        public static float ClampToUnit(this float f)
        {
            return (f < 0.0f) ? 0.0f : (f > 1.0f ? 1.0f : f);
        }
        
        /// <summary>
        /// Comprueba si el valor de esta instancia no es infinito o indefinido (NaN).
        /// </summary>
        public static bool IsDefinite(this float f)
        {
            return !float.IsInfinity(f) && !float.IsNaN(f);
        }
        
        /// <summary>
        /// Comprueba si el valor de esta instancia se encuentra en el intervalo de valores especificado.
        /// </summary>
        /// <param name="min">Límite inferior del intervalo.</param>
        /// <param name="max">Límite superior del intervalo.</param>
        public static bool InRange(this float f, float min, float max)
        {
            return (min < max) ? (f >= min) && (f <= max) : (f >= max) && (f <= min);
        }
        
        /// <summary>
        /// Transforma linealmente el valor de esta instancia de un intervalo de valores de origen a uno de destino.
        /// </summary>
        public static float RemapTo(this float f, float sourceMin, float sourceMax, float targetMin, float targetMax)
        {
            return targetMin + (f - sourceMin) * (targetMax - targetMin) / (sourceMax - sourceMin);
        }
        
        /// <summary>
        /// Transforma linealmente el valor de esta instancia del intervalo de valores especificado al intervalo
        /// [0.0, 1.0].
        /// </summary>
        public static float RemapToUnit(this float f, float sourceMin, float sourceMax)
        {
            return (f - sourceMin) * 1.0f / (sourceMax - sourceMin);
        }
        
        /// <summary>
        /// Redondea el valor de esta instancia al múltiplo más cercano del valor especificado como parámetro.
        /// indefinido (NaN).
        /// </summary>
        /// <param name="multiple">Valor a cuyo múltiplo más cercano se va a redondear.</param>
        public static float RoundToNearest(this float f, float multiple)
        {
            if (multiple < 1.0f)
            {
                float i = (float)Math.Floor(f);
                float x2 = i;
                while ((x2 += multiple) < f) ;
                float x1 = x2 - multiple;
                return (Math.Abs(f - x1) < Math.Abs(f - x2)) ? x1 : x2;
            }
            else
            {
                return (float)Math.Round(f / multiple, MidpointRounding.AwayFromZero) * multiple;
            }
        }
        
        /// <summary>
        /// Convierte el valor de esta instancia en un valor booleano, según si está más cerca de 0.0 o de 1.0.
        /// </summary>
        public static bool ToBool(this float f)
        {
            return f >= 0.5f;
        }
        
        /// <summary>
        /// Redondea el valor de esta instancia para que tenga el número de cifras significativas especificado.
        /// </summary>
        /// <param name="digits">Número de cifras significativas.</param>
        public static float ToPrecision(float f, int digits)
        {
            if (f == 0.0f) return 0.0f;
            
            double d = Math.Ceiling(Math.Log10(f < 0.0f ? -f : f));
            int power = digits - (int)Math.Truncate(d);
            
            double magnitude = Math.Pow(10, power);
            long shifted = (long)Math.Round(f * magnitude);
            return (float)((double)shifted / magnitude);
        }
        
        /// <summary>
        /// Restringe el valor de esta instancia a un intervalo de valores, devolviendo un valor que se desplaza y se
        /// repite de forma continua a lo largo del rango.
        /// </summary>
        /// <param name="min">Límite inferior del intervalo.</param>
        /// <param name="max">Límite superior del intervalo.</param>
        public static float WrapTo(this float f, float min, float max)
        {
            return (((f - min) % (max - min)) + (max - min)) % (max - min) + min;
        }
        
        /// <summary>
        /// Restringe el valor de esta instancia al un intervalo [0.0, 1.0], devolviendo un valor que se desplaza y se
        /// repite de forma continua.
        /// </summary>
        public static float WrapToUnit(this float f)
        {
            return ((f % 1.0f) + 1.0f) % (1.0f);
        }
    }
    
}