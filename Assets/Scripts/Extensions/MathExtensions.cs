using System;
using System.Collections.Generic;

namespace Extensions.System
{
    public static class MathUtil
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Atributos
        // ---- ---- ---- ---- ---- ---- ---- ----
        private static int[] primeListEvery200000 =
            {            2,    2750161,    5800139,    8960467,   12195263,
                  15485867,   18815249,   22182379,   25582163,   29005549,
                  32452867,   35926309,   39410869,   42920209,   46441223,
                  49979693,   53533523,   57099349,   60678109,   64268783,
                  67867979,   71480063,   75103543,   78736487,   82376243,
                  86028157,   89687693,   93354689,   97026263,  100711463,
                 104395303,  108092983,  111794899,  115507919,  119227013,
                 122949829,  126673979,  130408843,  134151007,  137896303,
                 141650963,  145403471,  149163299,  152935963,  156704131,
                 160481219,  164263007,  168048739,  171834301,  175628473,
                 179424691,  183236569,  187038041,  190850581,  194667089,
                 198491329,  202313627,  206141401,  209968651,  213808403,
                 217645199,  221489561,  225330881,  229179877,  233031229,
                 236887699,  240737963,  244605377,  248467649,  252334751,
                 256203221,  260079361,  263956243,  267836671,  271723979,
                 275604547,  279495497,  283384393,  287282227,  291178001,
                 295075153,  298981139,  302883151,  306789653,  310695797,
                 314606891,  318525731,  322445923,  326363251,  330287569,
                 334214467,  338141569,  342066091,  345996263,  349931947,
                 353868019,  357807473,  361749979,  365691323,  369638219,
                 373587911,  377528093,  381471617,  385426549,  389381621,
                 393342743,  397303559,  401265737,  405225649,  409192999,
                 413158523,  417125279,  421101649,  425069573,  429042797,
                 433024253,  437003069,  440981153,  444960067,  448943711,
                 452930477,  456911509,  460908053,  464895103,  468884879,
                 472882049,  476880797,  480871423,  484870097,  488874803,
                 492876863,  496876907,  500883577,  504900127,  508910533,
                 512927377,  516939919,  520951843,  524967347,  528981773,
                 533000401,  537018373,  541037269,  545066747,  549088471,
                 553105253,  557136683,  561166163,  565194727,  569225611,
                 573259433,  577292777,  581330611,  585367063,  589405753,
                 593441861,  597473749,  601518833,  605558641,  609606539,
                 613651369,  617707261,  621753667,  625804937,  629853437,
                 633910111,  637961257,  642016891,  646075673,  650132141,
                 654188429,  658251619,  662308589,  666372947,  670437871,
                 674506111,  678576179,  682647811,  686712391,  690775747,
                 694847539,  698922179,  702993937,  707069533,  711145559,
                 715225741,  719304541,  723379843,  727459583,  731547571,
                 735632797,  739716349,  743798113,  747889573,  751980769,
                 756065179,  760158937,  764252267,  768347903,  772432847,
                 776531419,  780625631,  784716623,  788811659,  792904111,
                 797003437,  801102823,  805200211,  809299703,  813404717,
                 817504253,  821611859,  825719371,  829827433,  833934359,
                 838041647,  842149313,  846260237,  850372561,  854486027,
                 858599509,  862721441,  866836543,  870947863,  875065823,
                 879190841,  883311217,  887435303,  891560609,  895682083,
                 899809363,  903932807,  908054801,  912171059,  916292957,
                 920419823,  924551941,  928683689,  932819479,  936948223,
                 941083987,  945217501,  949349537,  953485517,  957618023,
                 961748941,  965886697,  970024753,  974165923,  978308869,
                 982451707,  986587207,  990729269,  994872917,  999009049,
                1003162837, 1007304029, 1011451901, 1015595227, 1019744191,
                1023893887, 1028046001, 1032193363, 1036348757, 1040493253,
                1044645419, 1048806709, 1052959793, 1057120819, 1061278429,
                1065433427, 1069583819, 1073741237, 1077906079, 1082063023,
                1086218501, 1090378951, 1094539643, 1098701629, 1102864309,
                1107029839, 1111192087, 1115359823, 1119529007, 1123692473,
                1127870683, 1132040579, 1136217703, 1140386777, 1144570529,
                1148739817, 1152910469, 1157079827, 1161258401, 1165422547,
                1169604841, 1173775303, 1177951703, 1182135197, 1186320001,
                1190494771, 1194679561, 1198855069, 1203042521, 1207224973,
                1211405387, 1215591287, 1219769791, 1223955221, 1228150969,
                1232332813, 1236521513, 1240713121, 1244904761, 1249088273,
                1253270833, 1257465739, 1261655519, 1265848777, 1270027261,
                1274224999, 1278422053, 1282612601, 1286807377, 1291002247,
                1295202523, 1299403709, 1303601473, 1307795903, 1311987569,
                1316196209, 1320395761, 1324597811, 1328798579, 1332998417,
                1337195527, 1341399359, 1345602031, 1349804867, 1354008899,
                1358208613, 1362413383, 1366619797, 1370828741, 1375040479,
                1379256029, 1383461393, 1387679113, 1391880653, 1396094023,
                1400305369, 1404522397, 1408737047, 1412953543, 1417168889,
                1421376533, 1425593963, 1429808747, 1434028837, 1438247879,
                1442469313, 1446680891, 1450899347, 1455122611, 1459338679,
                1463555011, 1467781039, 1472008807, 1476223313, 1480443061,
                1484670179, 1488884053, 1493105833, 1497325307, 1501552333,
                1505776963, 1510006049, 1514242211, 1518466949, 1522690633,
                1526922017, 1531145257, 1535377609, 1539613021, 1543847689,
                1548074371, 1552302701, 1556536493, 1560772021, 1565016179,
                1569250363, 1573488181, 1577719123, 1581950137, 1586187761,
                1590425983, 1594668377, 1598903321, 1603141829, 1607375933,
                1611623887, 1615863233, 1620110831, 1624347299, 1628590751,
                1632828059, 1637064479, 1641305923, 1645557623, 1649802461,
                1654054511, 1658302651, 1662547979, 1666793189, 1671043009,
                1675293223, 1679535239, 1683786827, 1688031619, 1692281111,
                1696528907, 1700784643, 1705036303, 1709282387, 1713531997,
                1717783153, 1722031709, 1726290703, 1730548843, 1734807163,
                1739062387, 1743317047, 1747573771, 1751828929, 1756083473,
                1760341447, 1764602687, 1768858579, 1773118147, 1777379839,
                1781636627, 1785896681, 1790159317, 1794417769, 1798670243,
                1802933621, 1807198873, 1811461153, 1815727999, 1819993891,
                1824261419, 1828525481, 1832796751, 1837056673, 1841324857,
                1845587717, 1849864297, 1854134629, 1858397413, 1862670913,
                1866941123, 1871212333, 1875484267, 1879754147, 1884030431,
                1888303063, 1892570851, 1896848363, 1901109701, 1905386173,
                1909662913, 1913940361, 1918218437, 1922496853, 1926775157,
                1931045239, 1935319703, 1939601639, 1943880457, 1948154267,
                1952429177, 1956706319, 1960990007, 1965269363, 1969551757,
                1973828669, 1978106729, 1982386079, 1986669829, 1990951133,
                1995230821, 1999520693, 2003793959, 2008075067, 2012361313,
                2016634099, 2020923649, 2025211421, 2029501879, 2033788411,
                2038074751, 2042368613, 2046648391, 2050930523, 2055223741,
                2059519673, 2063812651, 2068101319, 2072396201, 2076681877,
                2080975187, 2085262129, 2089556039, 2093846743, 2098139233,
                2102429887, 2106722209, 2111010919, 2115298249, 2119595167,
                2123895979, 2128193651, 2132500423, 2136801209, 2141097799,
                2145390539
            };
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos
        // ---- ---- ---- ---- ---- ---- ---- ----
        /// <summary>
        /// Calcula el máximo común divisor de dos números.
        /// </summary>
        /// <param name="a">Número a.</param>
        /// <param name="b">Número b.</param>
        /// <returns></returns>
        public static int GCD(int a, int b)
        {
            // Hacer positivos
            a = a < 0 ? -a : a;
            b = b < 0 ? -b : b;
            
            // Casos base
            if (a == 0) return b;
            if (b == 0) return a;
            
            // Asegurarse de que a es mayor que b
            int remainder;
            if (a < b)
            {
                remainder = a;
                a = b;
                b = remainder;
            }
            
            int maxIterations = 1000;
            for (int i = 0; i < maxIterations; i++)
            {
                remainder = a % b;
                if (remainder == 0)
                    return b;
                
                a = b;
                b = remainder;
            }
            return 1;
        }
        
        /// <summary>
        /// Calcula el mínimo común múltiplo de dos números.
        /// </summary>
        /// <param name="a">Número a.</param>
        /// <param name="b">Número b.</param>
        /// <returns></returns>
        public static int LCM(int a, int b)
        {
            return (a / MathUtil.GCD(a, b)) * b;
        }
        
        #if (!UNITY_5 && !UNITY_4)
        public static float Lerp(float start, float end, float value)
        {
            return ((1.0f - value) * start) + (value * end);
        }
        
        public static float Hermite(float start, float end, float value)
        {
            return MathUtil.Lerp(start, end, value * value * (3.0f - 2.0f * value));
        }
        #endif
        
        public static float Remap(float sourceMin, float sourceMax, float targetMin, float targetMax, float value)
        {
            return targetMin + (value - sourceMin) * (targetMax - targetMin) / (sourceMax - sourceMin);
        }
        
        /// <summary>
        /// Dadas las probabilidades de dos sucesos independientes, calcula la probabilidad de que ocurra al menos uno
        /// de los dos.
        /// </summary>
        /// <param name="a">Probabilidad a.</param>
        /// <param name="b">Probabilidad b.</param>
        public static float ProbabilityOr(float a, float b)
        {
            a = a > 1.0f ? 1.0f : a; a = a < 0.0f ? 0.0f : a;
            b = b > 1.0f ? 1.0f : b; b = b < 0.0f ? 0.0f : b;
            
            return a + ((1.0f - a) * b);
        }
        
        public static int ToBase10(string number, int radix)
        {
            const string digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            
            if (radix < 2 || radix > digits.Length)
                throw new ArgumentException("The radix must be >= 2 and <= " +
                    digits.Length.ToString());
            
            if (String.IsNullOrEmpty(number))
                return 0;
            
            // Make sure the arbitrary numeral system number is in upper case
            number = number.ToUpperInvariant();
            
            int result = 0;
            int multiplier = 1;
            for (int i = number.Length - 1; i >= 0; i--)
            {
                char c = number[i];
                if (i == 0 && c == '-')
                {
                    // This is the negative sign symbol
                    result = -result;
                    break;
                }
                
                int digit = digits.IndexOf(c);
                if (digit == -1)
                    throw new ArgumentException(
                        "Invalid character in the arbitrary numeral system number",
                        "number");
                
                result += digit * multiplier;
                multiplier *= radix;
            }
            
            return result;
        }
        
        /// <summary>
        /// Devuelve el n-ésimo número de la secuencia de Fibonacci.
        /// </summary>
        /// <param name="n">Ordinal del número de la secuencia de Fibonacci a determinar.</param>
        public static int NthFibonacci(int n) 
        {
            double n1 = Math.Pow(1.6180339887498948, n + 1);
            double n2 = Math.Pow(-0.6180339887498948, n + 1);
            return (int)((n1 - n2) * 0.4472135954999579);
        }
        
        /// <summary>
        /// Devuelve el n-ésimo número primo.
        /// </summary>
        /// <param name="n">Ordinal del número primo a determinar.</param>
        public static int NthPrime(int n)
        {
            if (n <= 1) return 2;
            
            int steps = (n - 1) / 200000;
            int currentIndex = steps > 0 ? (steps * 200000) + 1 : 2;
            int currentNumber = steps > 0 ? MathUtil.primeListEvery200000[steps] : 3;
            
            if (currentIndex == n)
                return currentNumber;
            
            for (currentNumber += 2; currentNumber < int.MaxValue - 1; currentNumber += 2)
            {
                if (MathUtil.IsPrime(currentNumber))
                {
                    currentIndex++;
                    if (currentIndex >= n)
                        return currentNumber;
                }
            }
            
            return -1;
        }
        
        private static bool IsPrime(this int i)
        {
            if (i <= 1)
                return false;
            else if (i <= 3)
                return true;
            else if ((i % 2 == 0) || (i % 3 == 0))
                return false;
            
            int limit = (int)Math.Sqrt(i);
            for (int divisor = 5; divisor <= limit; divisor = divisor + 6)
            {
                if ((i % divisor == 0) || (i % (divisor + 2) == 0))
                    return false;
            }
            
            return true;
        }
    }
    
    public static class StatisticsExtensions
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos
        // ---- ---- ---- ---- ---- ---- ---- ----
        public static T Max<T>(this IEnumerable<T> values) where T : struct, IComparable<T>, IEquatable<T>
        {
            T max = default(T);
            
            int index = 0;
            foreach (T item in values)
            {
                if (index == 0)
                {
                    max = item;
                    index++;
                }
                else if (max.CompareTo(item) < 0)
                    max = item;
            }
            
            return max;
        }
        
        public static T Min<T>(this IEnumerable<T> values) where T : struct, IComparable<T>, IEquatable<T>
        {
            T min = default(T);
            
            int index = 0;
            foreach (T item in values)
            {
                if (index == 0)
                {
                    min = item;
                    index++;
                }
                else if (min.CompareTo(item) > 0)
                    min = item;
            }
            
            return min;
        }
        
        public static float Mean(this IEnumerable<float> values)
        {
            int count = 0;
            float total = 0.0f;
            foreach (var item in values)
            {
                count++;
                total += item;
            }
            
            return total / (float)count;
        }
        
        public static float Mean(this IEnumerable<int> values)
        {
            int count = 0, total = 0;
            foreach (var item in values)
            {
                count++;
                total += item;
            }
            
            return (float)total / (float)count;
        }
        
        public static float StandardDeviation(this IEnumerable<float> values)
        {
            float mean = values.Mean();
            
            int count = 0;
            float sumOfSquaresTotal = 0;
            foreach (var item in values)
            {
                count++;
                sumOfSquaresTotal += (item - mean) * (item - mean);
            }
            
            return (float)Math.Sqrt(sumOfSquaresTotal / (float)count);
        }
        
        public static float StandardDeviation(this IEnumerable<int> values)
        {
            float mean = values.Mean();
            
            int count = 0;
            float sumOfSquaresTotal = 0;
            foreach (var item in values)
            {
                count++;
                sumOfSquaresTotal += ((float)item - mean) * ((float)item - mean);
            }
            
            return (float)Math.Sqrt(sumOfSquaresTotal / (float)count);
        }
    }
    
}