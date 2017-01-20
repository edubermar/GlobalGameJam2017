using System;
using UnityEngine;

[Serializable]
public class ColorObjectPair
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    [SerializeField]
    private Color color;
    [SerializeField]
    private GameObject gameObject;

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Propiedades
    // ---- ---- ---- ---- ---- ---- ---- ----
    public Color Color
    {
        get { return this.color; }
        set { this.color = value; }
    }

    public GameObject GameObject
    {
        get { return this.gameObject; }
        set { this.gameObject = value; }
    }

}
