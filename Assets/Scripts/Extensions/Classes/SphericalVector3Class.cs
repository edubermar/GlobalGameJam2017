using System;
using UnityEngine;

[Serializable]
public class SphericalVector3
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    public float radius;
    public float azimuthAngle;
    public float inclinationAngle;
    
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos de conversión
    /// <summary>
    /// Convierte un punto de coordenadas esféricas a cartesianas (usando el sentido positivo del eje Y como cénit).
    /// </summary>
    public Vector3 ToCartesian()
    {
        Vector3 res = new Vector3();
        SphericalToCartesian(radius, azimuthAngle, inclinationAngle, out res);
        return res;
    }
    
    // Métodos estáticos
    /// <summary>
    /// Convierte un punto de coordenadas cartesianas (usando el sentido positivo del eje Y como cénit) a esféricas, y
    /// devuelve el resultado.
    /// </summary>
    public static SphericalVector3 CartesianToSpherical(Vector3 cartCoords)
    {
        SphericalVector3 result = new SphericalVector3();
        CartesianToSpherical(cartCoords, out result.radius, out result.azimuthAngle, out result.inclinationAngle);
        return result;
    }
    
    /// <summary>
    /// Convierte un punto de coordenadas esféricas a cartesianas (usando el sentido positivo del eje Y como cénit).
    /// </summary>
    public static void SphericalToCartesian(float radius, float azimuth, float inclination, out Vector3 outCartesian)
    {
        float a = radius * Mathf.Cos(inclination);
        outCartesian.x = a * Mathf.Cos(azimuth);
        outCartesian.y = radius * Mathf.Sin(inclination);
        outCartesian.z = a * Mathf.Sin(azimuth);
    }
    
    /// <summary>
    /// Convierte un punto de coordenadas cartesianas (usando el sentido positivo del eje Y como cénit) a esféricas, y
    /// guarda los componentes del resultado en las variables pasadas como referencia.
    /// </summary>
    public static void CartesianToSpherical(Vector3 cart, out float outRadius, out float outAzimuth, out float outIncl)
    {
        if (cart.x == 0)
            cart.x = Mathf.Epsilon;
        
        outRadius = Mathf.Sqrt((cart.x * cart.x) + (cart.y * cart.y) + (cart.z * cart.z));
        outAzimuth = Mathf.Atan(cart.z / cart.x);
        
        if (cart.x < 0)
            outAzimuth += Mathf.PI;
        
        outIncl = Mathf.Asin(cart.y / outRadius);
    }
    
}