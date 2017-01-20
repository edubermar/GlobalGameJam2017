#if (UNITY_5 || UNITY_4)
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions.UnityEngine
{
    public enum TransformAxis
    {
        X,
        Y,
        Z
    }
    
    public static class TransformExtensions
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos de gestión de hijos
        /// <summary>
        /// Destruye todos los hijos de esta instancia, llamando al método GameObject.Destroy de cada uno.
        /// </summary>
        public static void DestroyChildren(this Transform transform)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
                GameObject.Destroy(transform.GetChild(i).gameObject);
        }
        
        /// <summary>
        /// Destruye todos los hijos de esta instancia, llamando al método GameObject.DestroyImmediate de cada uno.
        /// </summary>
        public static void DestroyChildrenImmediate(this Transform transform)
        {
            List<Transform> children = new List<Transform>();
            foreach (Transform child in transform)
                children.Add(child);
            
            children.ForEach(t => GameObject.DestroyImmediate(t.gameObject));
        }
        
        /// <summary>
        /// Devuelve el primer hijo de esta instancia, o null en el caso de que no exista.
        /// </summary>
        public static Transform FirstChild(this Transform transform)
        {
            return transform.childCount > 0 ? transform.GetChild(0) : null;
        }
        
        /// <summary>
        /// Busca en el primer hijo de esta instancia un componente que coincida con el parámetro de tipo especificado,
        /// y lo devuelve si es posible encontrarlo. En caso de que el hijo no disponga de un componente del tipo
        /// adecuado, o de que el propio hijo no exista, devolverá null.
        /// </summary>
        public static T FirstChild<T>(this Transform transform) where T : Component
        {
            return transform.childCount > 0 ? transform.GetChild(0).GetComponent<T>() : null;
        }
        
        /// <summary>
        /// Devuelve el hijo de esta instancia cuyo índice coincide con el especificado como parámetro, o null en el
        /// caso de que no exista.
        /// </summary>
        public static Transform ChildAt(this Transform transform, int index)
        {
            return (index >= 0 && index < transform.childCount) ? transform.GetChild(index) : null;
        }
        
        /// <summary>
        /// Busca en el hijo de esta instancia cuyo íncide coincide con el especificado, un componente que coincida con
        /// el parámetro de tipo especificado, y lo devuelve si es posible encontrarlo. En caso de que el hijo no
        /// disponga de un componente del tipo adecuado, o de que el propio hijo no exista, devolverá null.
        /// </summary>
        public static T ChildAt<T>(this Transform transform, int index) where T : Component
        {
            return (index >= 0 && index < transform.childCount) ? transform.GetChild(index).GetComponent<T>() : null;
        }
        
        /// <summary>
        /// Devuelve el último hijo de esta instancia, o null en el caso de que no exista.
        /// </summary>
        public static Transform LastChild(this Transform transform)
        {
            return transform.childCount > 0 ? transform.GetChild(transform.childCount - 1) : null;
        }
        
        /// <summary>
        /// Busca en el último hijo de esta instancia un componente que coincida con el parámetro de tipo especificado,
        /// y lo devuelve si es posible encontrarlo. En caso de que el hijo no disponga de un componente del tipo
        /// adecuado, o de que el propio hijo no exista, devolverá null.
        /// </summary>
        public static T LastChild<T>(this Transform transform) where T : Component
        {
            return transform.childCount > 0 ? transform.GetChild(transform.childCount - 1).GetComponent<T>() : null;
        }
        
        // Métodos de transformación
        /// <summary>
        /// Reinicia la transformación de esta instancia, moviéndolo al origen del sistema de coordenadas local,
        /// eliminando la rotación, y asignando la escala unitaria.
        /// </summary>
        /// <param name="transform"></param>
        public static void Reset(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
        
        /// <summary>
        /// Rota uno solo de los componentes de rotación de esta instancia según el espacio de coordenadas global.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="axis">Eje a lo largo del cual efectuar la rotación.</param>
        /// <param name="angle">Ángulo a añadir en torno al eje especificado.</param>
        public static void RotateByAxis(this Transform transform, TransformAxis axis, float angle)
        {
            switch (axis)
            {
                case TransformAxis.X:
                    transform.rotation *= Quaternion.AngleAxis(angle, Vector3.right);
                    break;
                
                case TransformAxis.Y:
                    transform.rotation *= Quaternion.AngleAxis(angle, Vector3.up);
                    break;
                
                case TransformAxis.Z:
                    transform.rotation *= Quaternion.AngleAxis(angle, Vector3.forward);
                    break;
            }
        }
        
        /// <summary>
        /// Rota uno solo de los componentes de rotación de esta instancia según el espacio de coordenadas
        /// especificado.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="axis">Eje a lo largo del cual efectuar la rotación.</param>
        /// <param name="angle">Ángulo a añadir en torno al eje especificado.</param>
        /// <param name="transformSpace">Espacio de coordenadas a utilizar.</param>
        public static void RotateByAxis(this Transform transform, TransformAxis axis, float angle,
            Space transformSpace)
        {
            switch (axis)
            {
                case TransformAxis.X:
                    if (transformSpace == Space.World)
                        transform.rotation *= Quaternion.AngleAxis(angle, Vector3.right);
                    else
                        transform.Rotate(angle, 0.0f, 0.0f, Space.Self);
                    break;
                
                case TransformAxis.Y:
                    if (transformSpace == Space.World)
                        transform.rotation *= Quaternion.AngleAxis(angle, Vector3.up);
                    else
                        transform.Rotate(0.0f, angle, 0.0f, Space.Self);
                    break;
                
                case TransformAxis.Z:
                    if (transformSpace == Space.World)
                        transform.rotation *= Quaternion.AngleAxis(angle, Vector3.forward);
                    else
                        transform.Rotate(0.0f, 0.0f, angle, Space.Self);
                    break;
            }
        }
        
        /// <summary>
        /// Asigna el componente X de la posición global de esta instancia.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="value">Valor a asignar al componente X.</param>
        public static void SetPositionX(this Transform transform, float value)
        {
            transform.position = new Vector3(value, transform.position.y, transform.position.z);
        }
        
        /// <summary>
        /// Asigna el componente X de la posición de esta instancia en el espacio de coordenadas especificado.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="value">Valor a asignar al componente X.</param>
        /// <param name="transformSpace">Espacio de coordenadas a utilizar.</param>
        public static void SetPositionX(this Transform transform, float value, Space transformSpace)
        {
            if (transformSpace == Space.World)
                transform.position = new Vector3(value, transform.position.y, transform.position.z);
            else
                transform.localPosition = new Vector3(value, transform.localPosition.y, transform.localPosition.z);
        }
        
        /// <summary>
        /// Asigna el componente Y de la posición global de esta instancia.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="value">Valor a asignar al componente Y.</param>
        public static void SetPositionY(this Transform transform, float value)
        {
            transform.position = new Vector3(transform.position.x, value, transform.position.z);
        }
        
        /// <summary>
        /// Asigna el componente Y de la posición de esta instancia en el espacio de coordenadas especificado.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="value">Valor a asignar al componente Y.</param>
        /// <param name="transformSpace">Espacio de coordenadas a utilizar.</param>
        public static void SetPositionY(this Transform transform, float value, Space transformSpace)
        {
            if (transformSpace == Space.World)
                transform.position = new Vector3(transform.position.x, value, transform.position.z);
            else
                transform.localPosition = new Vector3(transform.localPosition.x, value, transform.localPosition.z);
        }
        
        /// <summary>
        /// Asigna el componente Z de la posición global de esta instancia.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="value">Valor a asignar al componente Z.</param>
        public static void SetPositionZ(this Transform transform, float value)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, value);
        }
        
        /// <summary>
        /// Asigna el componente Z de la posición de esta instancia en el espacio de coordenadas especificado.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="value">Valor a asignar al componente Z.</param>
        /// <param name="transformSpace">Espacio de coordenadas a utilizar.</param>
        public static void SetPositionZ(this Transform transform, float value, Space transformSpace)
        {
            if (transformSpace == Space.World)
                transform.position = new Vector3(transform.position.x, transform.position.y, value);
            else
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, value);
        }
        
        /// <summary>
        /// Asigna el componente Z de la rotación global de esta instancia (en su representación de Euler).
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="value">Valor a asignar al componente Z.</param>
        /// <param name="transformSpace">Espacio de coordenadas a utilizar.</param>
        public static void SetRotationEulerZ(this Transform transform, float value)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
                                                  transform.rotation.eulerAngles.y,
                                                  value);
        }
        
        /// <summary>
        /// Asigna el componente Z de la rotación de esta instancia (en su representación de Euler) en el espacio de
        /// coordenadas especificado.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="value">Valor a asignar al componente Z.</param>
        /// <param name="transformSpace">Espacio de coordenadas a utilizar.</param>
        public static void SetRotationEulerZ(this Transform transform, float value, Space transformSpace)
        {
            if (transformSpace == Space.World)
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
                                                      transform.rotation.eulerAngles.y,
                                                      value);
            else
                transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x,
                                                           transform.localRotation.eulerAngles.y,
                                                           value);
        }
        
        /// <summary>
        /// Desplaza uno solo de los componentes de la posición global de esta instancia.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="axis">Eje a lo largo del cual efectuar el desplazamiento.</param>
        /// <param name="value">Valor a añadir a lo largo del eje especificado.</param>
        public static void TranslateByAxis(this Transform transform, TransformAxis axis, float value)
        {
            switch (axis)
            {
                case TransformAxis.X:
                    transform.position += new Vector3(value, 0.0f, 0.0f);
                    break;
                
                case TransformAxis.Y:
                    transform.position += new Vector3(0.0f, value, 0.0f);
                    break;
                
                case TransformAxis.Z:
                    transform.position += new Vector3(0.0f, 0.0f, value);
                    break;
                
            }
        }
        
        /// <summary>
        /// Desplaza uno solo de los componentes de la posición de esta instancia en el espacio de coordenadas
        /// especificado.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="axis">Eje a lo largo del cual efectuar el desplazamiento.</param>
        /// <param name="value">Valor a añadir a lo largo del eje especificado.</param>
        /// <param name="transformSpace">Espacio de coordenadas a utilizar.</param>
        public static void TranslateByAxis(this Transform transform, TransformAxis axis, float value,
                                           Space transformSpace)
        {
            switch (axis)
            {
                case TransformAxis.X:
                    if (transformSpace == Space.World)
                        transform.position += new Vector3(value, 0.0f, 0.0f);
                    else
                        transform.localPosition += new Vector3(value, 0.0f, 0.0f);
                    break;
                
                case TransformAxis.Y:
                    if (transformSpace == Space.World)
                        transform.position += new Vector3(0.0f, value, 0.0f);
                    else
                        transform.localPosition += new Vector3(0.0f, value, 0.0f);
                    break;
                
                case TransformAxis.Z:
                    if (transformSpace == Space.World)
                        transform.position += new Vector3(0.0f, 0.0f, value);
                    else
                        transform.localPosition += new Vector3(0.0f, 0.0f, value);
                    break;
                
            }
        }
    }
    
}
#endif