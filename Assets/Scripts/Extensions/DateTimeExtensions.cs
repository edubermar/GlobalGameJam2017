using System;

namespace Extensions.System
{
    public static class DateTimeExtensions
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos
        // ---- ---- ---- ---- ---- ---- ---- ----
        /// <summary>
        /// Determina si el objeto System.DateTime actual se encuentra dentro del rango de fechas especificado.
        /// </summary>
        public static bool InRange(this DateTime dt, DateTime startDate, DateTime endDate)
        {
            return (dt.Ticks >= startDate.Ticks) && (dt.Ticks <= endDate.Ticks);
        }
        
        /// <summary>
        /// Determina si el objeto System.DateTime actual representa un día de la semana laborable, de lunes a viernes.
        /// </summary>
        public static bool IsWorkingDay(this DateTime dt)
        {
            return (dt.DayOfWeek != DayOfWeek.Saturday) && (dt.DayOfWeek != DayOfWeek.Sunday);
        }
        
        /// <summary>
        /// Determina si el objeto System.DateTime actual representa un día del fin de semana, sábado o domingo.
        /// </summary>
        public static bool IsWeekend(this DateTime dt)
        {
            return (dt.DayOfWeek == DayOfWeek.Saturday) || (dt.DayOfWeek == DayOfWeek.Sunday);
        }
        
        /// <summary>
        /// Devuelve el próximo día laborable que hay a partir de la fecha representada por el objeto System.DateTime
        /// actual.
        /// </summary>
        public static DateTime NextWorkday(this DateTime dt)
        {
            DateTime nextDay = dt;
            while (!DateTimeExtensions.IsWorkingDay(nextDay))
            {
                nextDay = nextDay.AddDays(1);
            }
            return nextDay;
        }
    }
    
}