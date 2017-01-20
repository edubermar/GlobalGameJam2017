#if (UNITY_5 || UNITY_4)
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions.UnityEngine
{
    public static class VectorExtensions
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos
        // ---- ---- ---- ---- ---- ---- ---- ----
        public static Vector2 ClampComponents(this Vector2 vector2, float min, float max)
        {
            if (min < max)
                return new Vector2(vector2.x < min ? min : (vector2.x > max ? max : vector2.x),
                                   vector2.y < min ? min : (vector2.y > max ? max : vector2.y));
            else
                return new Vector2(vector2.x < max ? max : (vector2.x > min ? min : vector2.x),
                                   vector2.y < max ? max : (vector2.y > min ? min : vector2.y));
        }
        
        public static Vector3 ClampComponents(this Vector3 vector3, float min, float max)
        {
            if (min < max)
                return new Vector3(vector3.x < min ? min : (vector3.x > max ? max : vector3.x),
                                   vector3.y < min ? min : (vector3.y > max ? max : vector3.y),
                                   vector3.z < min ? min : (vector3.z > max ? max : vector3.z));
            else
                return new Vector3(vector3.x < max ? max : (vector3.x > min ? min : vector3.x),
                                   vector3.y < max ? max : (vector3.y > min ? min : vector3.y),
                                   vector3.z < max ? max : (vector3.z > min ? min : vector3.z));
        }
        
        public static Vector2 ClampMagnitude(this Vector2 vector2, float maxMagnitude)
        {
            return Vector2.ClampMagnitude(vector2, maxMagnitude);
        }
        
        public static Vector3 ClampMagnitude(this Vector3 vector3, float maxMagnitude)
        {
            return Vector3.ClampMagnitude(vector3, maxMagnitude);
        }
    }
    
}
#endif