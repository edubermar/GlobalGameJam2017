using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Class, Inherited = true)]
public class PrefabAttribute : Attribute
{
    public readonly string Name;
    public readonly bool Persistent;
    
    public PrefabAttribute(string name, bool persistent)
    {
        this.Name = name;
        this.Persistent = persistent;
    }
    
    public PrefabAttribute(string name)
    {
        this.Name = name;
        this.Persistent = false;
    }
    
}