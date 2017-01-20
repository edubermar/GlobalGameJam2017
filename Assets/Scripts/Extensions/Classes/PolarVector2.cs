using System;
using UnityEngine;

public struct PolarVector2
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Fields
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Components
    public float radius;
    public float azimuth;
    
    // Predefined values
    public static readonly PolarVector2 Up = new PolarVector2(1.0f, 1.57079632f);
    public static readonly PolarVector2 Down = new PolarVector2(1.0f, -1.57079632f);
    public static readonly PolarVector2 Left = new PolarVector2(1.0f, 3.14159265f);
    public static readonly PolarVector2 Right = new PolarVector2(1.0f, 0.0f);
    
    public static readonly PolarVector2 Zero = new PolarVector2(0.0f, 0.0f);
    
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Constructors
    // ---- ---- ---- ---- ---- ---- ---- ----
    public PolarVector2(float radius, float azimuth)
    {
        this.radius = radius;
        this.azimuth = ((((azimuth + 3.14159265f) % 6.2831853072f) + 6.2831853072f) % 6.2831853072f) - 3.14159265f;
    }
    
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Methods
    // ---- ---- ---- ---- ---- ---- ---- ----
    public static PolarVector2 CartesianToPolar(Vector2 a)
    {
        return new PolarVector2(a.magnitude, Mathf.Atan2(a.y, a.x));
    }
    
    public static Vector2 PolarToCartesian(PolarVector2 a)
    {
        return new Vector2(a.radius * Mathf.Cos(a.azimuth), a.radius * Mathf.Sin(a.azimuth));
    }
    
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Operators
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Conversion
    public static implicit operator Vector2(PolarVector2 a) { return PolarVector2.PolarToCartesian(a); }
    public static implicit operator PolarVector2(Vector2 a) { return PolarVector2.CartesianToPolar(a); }
    
}