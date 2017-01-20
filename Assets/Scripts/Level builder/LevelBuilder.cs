using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    [SerializeField]
    private Texture2D textureMap = null;
    [SerializeField]
    private Vector2 cellSize = Vector2.one;
    [SerializeField]
    private List<ColorObjectPair> colorObjectList = new List<ColorObjectPair>();

    [SerializeField]
    private GameObject defaultObject = null;

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos de MonoBehaviour
    private void Awake()
    {
        for (int i = 0; i < this.textureMap.height; i++)
        {
            for (int j = 0; j < this.textureMap.width; j++)
            {
                GameObject currentGameObject = this.GetGameObjectForColor(textureMap.GetPixel(j, i));
                if (currentGameObject != null)
                {
                    GameObject createdObject = GameObject.Instantiate<GameObject>(currentGameObject);
                    createdObject.transform.position = new Vector3((float)j * cellSize.x, (float)i * cellSize.y, 0.0f);
                    createdObject.transform.SetParent(this.transform, false);
                }   
            }
        }
	}
	
    // Métodos auxiliares
    private GameObject GetGameObjectForColor(Color color)
    {
        foreach (ColorObjectPair item in this.colorObjectList)
        {
            if (LevelBuilder.ColorEqualsIgnoringAlpha(color, item.Color))
                return item.GameObject;
        }
        return this.defaultObject;
    }

    private SortedDictionary<Color, GameObject> GetGameObjectFunction()
    {
        SortedDictionary<Color, GameObject> result = new SortedDictionary<Color, GameObject>();
        foreach (ColorObjectPair item in this.colorObjectList)
            result.Add(item.Color, item.GameObject);

        return result;
    }

    // Métodos estáticos
    private static bool ColorEqualsIgnoringAlpha(Color a, Color b)
    {
        return (a.r == b.r) && (a.g == b.g) && (a.b == b.b);
    }

}