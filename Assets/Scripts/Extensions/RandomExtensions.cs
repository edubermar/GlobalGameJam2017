using System;
using System.Collections.Generic;

namespace Extensions.UnityEngine
{
    public static class RandomUtil
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Atributos
        // ---- ---- ---- ---- ---- ---- ---- ----
        #if (!UNITY_5 && !UNITY_4)
        private static Random r = new Random();
        #endif
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Propiedades
        // ---- ---- ---- ---- ---- ---- ---- ----
        /// <summary>
        /// Devuelve un valor booleano aleatorio con una distribución uniforme.
        /// </summary>
        public static bool NextBoolean
        {
            #if (UNITY_5 || UNITY_4)
                get { return global::UnityEngine.Random.value > 0.5f; }
            #else
                get { return RandomUtil.r.Next() % 2 > 0; }
            #endif
        }
        
        #if (!UNITY_5 && !UNITY_4)
        public static int Seed
        {
            set { RandomUtil.r = new Random(value); }
        }
        #endif
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos
        // ---- ---- ---- ---- ---- ---- ---- ----
        /// <summary>
        /// Devuelve un valor booleano aleatorio, con la probabilidad especificada de tener el valor 'true'.
        /// </summary>
        /// <param name = "probability">Probabilidad entre cero y uno de devolver 'true'.</param>
        public static bool Chance(float probability)
        {
            #if (UNITY_5 || UNITY_4)
                return global::UnityEngine.Random.value < probability;
            #else
                return ((float)RandomUtil.r.NextDouble()) < probability;
            #endif
        }
        
        /// <summary>
        /// Devuelve uno de los elementos de una enumeración elegido al azar, con una distribución uniforme.
        /// </summary>
        public static T NextEnum<T>() where T : struct
        {
            #if (UNITY_5 || UNITY_4)
                Array enumValues = Enum.GetValues(typeof(T));
                int index = global::UnityEngine.Random.Range(0, enumValues.Length);
                return (T)enumValues.GetValue(index);
            #else
                Array enumValues = Enum.GetValues(typeof(T));
                int index = RandomUtil.r.Next(enumValues.Length);
                return (T)enumValues.GetValue(index);
            #endif
        }
        
        /// <summary>
        /// Genera un número aleatorio siguiendo una distribución normal o gaussiana, con forma de campana.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="mu">Valor medio de la distribución normal.</param>
        /// <param name="sigma">Desviación típica de la distribución normal.</param>
        public static float NextGaussian(float mu = 0, float sigma = 1)
        {
            #if (UNITY_5 || UNITY_4)
                float u1 = global::UnityEngine.Random.value;
                float u2 = global::UnityEngine.Random.value;
                
                float rand_std_normal =
                    global::UnityEngine.Mathf.Sqrt(-2.0f * global::UnityEngine.Mathf.Log(u1)) *
                                                   global::UnityEngine.Mathf.Sin(6.2831853f * u2);
                return mu + sigma * rand_std_normal;
            #else
                double u1 = RandomUtil.r.NextDouble();
                double u2 = RandomUtil.r.NextDouble();
                
                double rand_std_normal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(6.2831853f * u2);
                
                return mu + sigma * ((float)rand_std_normal);
            #endif
        }
        
        /// <summary>
        /// Genera un número aleatorio siguiendo una distribución normal o gaussiana, con forma de campana. El valor
        /// generado está delimitado por dos números de punto flotante de simple precisión, de tal forma que definen el
        /// intervalo que contiene el 95% de los valores de la distribución (el equivalente a 2 veces la desviación
        /// típica a cada lado del valor medio).
        /// </summary>
        /// <param name="min">Límite inferior del intervalo.</param>
        /// <param name="max">Límite superior del intervalo.</param>
        public static float NextGaussianRanged(float min, float max)
        {
            return max > min ? RandomUtil.NextGaussian((min + max) * 0.5f, (max - min) * 0.25f)
                             : RandomUtil.NextGaussian((min + max) * 0.5f, (min - max) * 0.25f);
        }
        
        /// <summary>
        /// Devuelve uno de los valores especificados como parámetros con una distribución uniforme.
        /// </summary>
        public static T NextInRange<T>(params T[] valueList)
        {
            #if (UNITY_5 || UNITY_4)
                return valueList[global::UnityEngine.Random.Range(0, valueList.Length)];
            #else
                return valueList[RandomUtil.r.Next(valueList.Length)];
            #endif
        }
        
        /// <summary>
        /// Devuelve un número primo aleatorio, entre 2 y el valor especificado.
        /// </summary>
        public static int NextPrime(int max)
        {
            List<int> primes = new List<int>();
            primes.Add(2);
            
            for (int i = 3; i < max; i += 2)
            {
                if (RandomUtil.IsPrime(i))
                    primes.Add(i);
            }
            
            #if (UNITY_5 || UNITY_4)
                return primes[global::UnityEngine.Random.Range(0, primes.Count)];
            #else
                return primes[RandomUtil.r.Next(primes.Count)];
            #endif
        }
        
        /// <summary>
        /// Devuelve un número primo aleatorio, en el rango de valores especificado.
        /// </summary>
        public static int NextPrime(int min, int max)
        {
            List<int> primes = new List<int>();
            
            if (min <= 2)
                primes.Add(2);
            
            for (int i = ((min / 2) * 2) + 1; i < max; i += 2)
            {
                if (RandomUtil.IsPrime(i))
                    primes.Add(i);
            }
            
            #if (UNITY_5 || UNITY_4)
            return primes[global::UnityEngine.Random.Range(0, primes.Count)];
            #else
            return primes[RandomUtil.r.Next(primes.Count)];
            #endif
        }
        
        /// <summary>
        /// Genera un número aleatorio siguiendo una distribución triangular.
        /// </summary>
        /// <remarks>
        /// Ver enlace: http://en.wikipedia.org/wiki/Triangular_distribution para una descripción de la distribución de
        /// probabilidades triangular y del algoritmo para generarlo.
        /// </remarks>
        /// <param name = "min">Valor mínimo de la distribución.</param>
        /// <param name = "max">Valor máximo de la distribución.</param>
        /// <param name = "mode">Moda de la distribución (el valor más frecuente).</param>
        public static float NextTriangular(float min, float max, float mode)
        {
            #if (UNITY_5 || UNITY_4)
                float u = global::UnityEngine.Random.value;
                
                return u < (mode - min) / (max - min)
                           ? min + global::UnityEngine.Mathf.Sqrt(u * (max - min) * (mode - min))
                           : max - global::UnityEngine.Mathf.Sqrt((1.0f - u) * (max - min) * (max - mode));
            #else
                float u = (float)RandomUtil.r.NextDouble();
                
                return u < (mode - min) / (max - min)
                           ? min + (float)Math.Sqrt(u * (max - min) * (mode - min))
                           : max - (float)Math.Sqrt((1.0f - u) * (max - min) * (max - mode));
            #endif
        }
        
        // Métodos auxiliares
        private static bool IsPrime(int i)
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
    
    public sealed class RandomList<T> : IEnumerable<T>
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Atributos
        // ---- ---- ---- ---- ---- ---- ---- ----
        private Random r;
        
        private List<T> itemList;
        private List<float> weightList;
        
        private float weightTotal;
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Propiedades
        // ---- ---- ---- ---- ---- ---- ---- ----
        /// <summary>
        /// Obtiene el número total de elementos que contiene este RandomList<T>.
        /// </summary>
        public int Count
        {
            get { return this.itemList.Count; }
        }
        
        /// <summary>
        /// Devuelve uno de los elementos de este RandomList<T> elegido al azar, según los pesos con los que han sido
        /// introducidos.
        /// </summary>
        public T NextItem
        {
            get
            {
                if (this.itemList.Count > 0)
                {
                    T result = this.itemList[0];
                    float resultValue = 0.0f;
                    float value = ((float)r.NextDouble()) * weightTotal;
                    
                    for (int i = 0; i < this.itemList.Count; i++)
                    {
                        resultValue += this.weightList[i];
                        if (resultValue > value)
                        {
                            result = this.itemList[i];
                            break;
                        }
                    }
                    
                    return result;
                }
                else
                    return default(T);
            }
        }
        
        /// <summary>
        /// Obtiene la suma de los pesos de todos los elementos de este RandomList<T>.
        /// </summary>
        /// <value>The total weight.</value>
        public float TotalWeight
        {
            get { return this.weightTotal; }
        }
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Constructores
        // ---- ---- ---- ---- ---- ---- ---- ----
        public RandomList()
        {
            this.r = new Random();
            
            this.itemList = new List<T>();
            this.weightList = new List<float>();
            
            this.weightTotal = 0.0f;
        }
        
        public RandomList(int seed)
        {
            this.r = new Random(seed);
            
            this.itemList = new List<T>();
            this.weightList = new List<float>();
            
            this.weightTotal = 0.0f;
        }
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos de gestión
        /// <summary>
        /// Agrega un elemento con un peso asociado a este RandomList<T>. Si el peso especificado no es positivo, el
        /// elemento no será agregado.
        /// </summary>
        public void Add(T item, float weight)
        {
            if (weight > 0.0f)
            {
                this.weightTotal += weight;
                this.itemList.Add(item);
                this.weightList.Add(weight);
            }
        }
        
        /// <summary>
        /// Elimina todos los elementos de este RandomList<T>.
        /// </summary>
        public void Clear()
        {
            this.weightTotal = 0.0f;
            this.itemList.Clear();
            this.weightList.Clear();
        }
        
        /// <summary>
        /// Determina si el elemento especificado se encuentra en este RandomList<T>.
        /// </summary>
        public bool Contains(T item)
        {
            return this.itemList.Contains(item);
        }
        
        /// <summary>
        /// Elimina la primera aparición del objeto especificado de este RandomList<T>.
        /// </summary>
        public bool Remove(T item)
        {
            int indexOfItem = this.itemList.IndexOf(item);
            if (indexOfItem >= 0)
            {
                // Reajustar peso total
                float removedWeight = this.weightList[indexOfItem];
                this.weightTotal -= removedWeight;
                
                // Eliminar elemento y peso asociado
                this.itemList.RemoveAt(indexOfItem);
                this.weightList.RemoveAt(indexOfItem);
                
                return true;
            }
            return false;
        }
        
        // Métodos de IEnumerable
        /// <summary>
        /// Devuelve un enumerador que recorre en iteración los elementos de RandomList<T>.
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            return this.itemList.GetEnumerator();
        }
        
        global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
    
}