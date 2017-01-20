﻿using Extensions.UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public float lowerLimit = -4.0f;
    public float upperLimit = 4.0f;

    public float movementSpeed = 1.0f;
    
    private float worldPosition = 0.0f;
    private float perlinHeight;
    public int iterations = 0;

    public List<GridGenerator> gridList = new List<GridGenerator>();

    // Update is called once per frame
	private void Start()
    {
        this.perlinHeight = Random.Range(-1000.0f, 1000.0f);

        foreach (var grid in this.gridList)
        {
            grid.GenerateMaze(this.worldPosition, this.perlinHeight, iterations);
            grid.RenderMaze();
        }
    }
    
    private void Update()
    {
        // Calcular desplazamiento
        float displacement = movementSpeed * Time.deltaTime;

        // Actualizar posición global y posiciones de los grids
        this.worldPosition += displacement;
        foreach (var grid in this.gridList)
        {
            grid.transform.TranslateByAxis(TransformAxis.X, -displacement);
            if (grid.transform.position.x < this.lowerLimit)
            {
                grid.transform.TranslateByAxis(TransformAxis.X, this.upperLimit - this.lowerLimit);
                grid.GenerateMaze(this.worldPosition, this.perlinHeight, iterations);
                grid.RenderMaze();
            }
        }
	}

}