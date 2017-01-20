using System;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    /// <summary>
    /// Cuando Unity cierra la aplicación, destruye los objetos en un orden
    /// aleatorio. En teoría un Singleton solo se destruye cuando se cierra la
    /// aplicación, pero si cualquier clase intenta acceder a la propiedad
    /// Instance después de que el Singleton se destruye, se crearía un objeto
    /// fantasma que permanecería en la escena del editor tras detener por
    /// completo la aplicación.
    /// Esta variable evitará este comportamiento no deseado, asegurando que
    /// no se cree este objeto fantasma.
    /// </summary>
    private static bool applicationIsQuitting = false;
    
    private static T instance;
    private static object lockObject = new object();
    
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Propiedades
    // ---- ---- ---- ---- ---- ---- ---- ----
    public static T Instance
    {
        get
        {
            if (Singleton<T>.applicationIsQuitting)
            {
                Debug.LogWarningFormat("El cierre de la aplicación ha eliminado el Singleton de tipo {0}. " +
                                       "No se creará de nuevo, devolviendo null.", typeof(T).ToString());
                return null;
            }
            
            lock (Singleton<T>.lockObject)
            {
                // Vemos si la instancia "global" no está inicializada.
                if (Singleton<T>.instance == null)
                {
                    T[] singletonObjects = GameObject.FindObjectsOfType<T>();
                    
                    // Hay al menos una instancia del objeto Singleton en la escena actual.
                    if (singletonObjects.Length > 0)
                    {
                        Singleton<T>.instance = singletonObjects[0];
                        if (singletonObjects.Length > 1)
                        {
                            Debug.LogWarningFormat("Se ha encontrado más de una instancia del Singleton de tipo {0} " +
                                                   "a la vez. Manteniendo la primera y destruyendo las demás.",
                                                   typeof(T).ToString());
                        }
                        for (int i = singletonObjects.Length - 1; i > 0; i--)
                            GameObject.DestroyImmediate(singletonObjects[i].gameObject);
                        
                        return Singleton<T>.instance;
                    }
                    
                    // No hay ninguna instancia del objeto singleton, la creamos en este mismo momento.
                    PrefabAttribute attribute =
                        Attribute.GetCustomAttribute(typeof(T), typeof(PrefabAttribute)) as PrefabAttribute;
                    if (attribute == null)
                    {
                        Debug.LogErrorFormat("No se ha definido el atributo de Prefab para el Singleton de tipo {0}.",
                                             typeof(T).ToString());
                        return null;
                    }
                    
                    string prefabName = attribute.Name;
                    if (string.IsNullOrEmpty(prefabName))
                    {
                        Debug.LogErrorFormat("El nombre del Prefab está vacío para el Singleton de tipo {0}.",
                                             typeof(T).ToString());
                        return null;
                    }
                    
                    GameObject resourceObject = Resources.Load<GameObject>(prefabName);
                    GameObject gameObject = GameObject.Instantiate<GameObject>(resourceObject);
                    if (gameObject == null)
                    {
                        Debug.LogErrorFormat("No se ha podido encontrar el Prefab {0} en la carpeta de recursos, " +
                                             "para el singleton de tipo {1}", prefabName, typeof(T).ToString());
                        return null;
                    }
                    
                    gameObject.name = prefabName;
                    Singleton<T>.instance = gameObject.GetComponent<T>();
                    if (Singleton<T>.instance == null)
                    {
                        Debug.LogWarningFormat("No había un componente de tipo {0} en el Prefab {1}. Creando uno.",
                                               typeof(T).ToString(), prefabName);
                        Singleton<T>.instance = gameObject.AddComponent<T>();
                    }
                    
                    if (attribute.Persistent)
                        GameObject.DontDestroyOnLoad(Singleton<T>.instance.gameObject);
                }
                
                // La instancia "global" está inicializada, la devolvemos.
                return Singleton<T>.instance;
            }
        }
    }
    
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Constructores
    // ---- ---- ---- ---- ---- ---- ---- ----
    protected Singleton() { }
    
    protected virtual void OnDestroy()
    {
        applicationIsQuitting = true;
    }
    
}