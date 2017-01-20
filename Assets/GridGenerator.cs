using Extensions.System;
using Extensions.UnityEngine;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public SpriteRenderer wall;

    public float width;
    public float height;
    
    private Grid2D<bool> grid = new Grid2D<bool>(96, 48);
    
    // Use this for initialization
	void Start ()
    {
        this.GenerateMaze(8);
        this.RenderMaze();
	}

    private void RenderMaze()
    {
        // Renderizar laberinto
        for (int i = 0; i < this.grid.Width; i++)
        {
            for (int j = 0; j < this.grid.Height; j++)
            {
                if (this.grid.GetItem(i, j))
                {
                    var wall = GameObject.Instantiate<SpriteRenderer>(this.wall);

                    float cameraSize = Camera.main.orthographicSize;
                    float itemWidth = this.width / this.grid.Width;

                    float hPosition = i.ToFloat().RemapTo(0, this.grid.Width - 1, -this.width * 0.5f, this.width * 0.5f);
                    float vPosition = j.ToFloat().RemapTo(0, this.grid.Height - 1, -this.height * 0.5f, this.height * 0.5f);
                    wall.transform.position = new Vector2(hPosition, vPosition);
                    wall.transform.localScale = Vector3.one * itemWidth;
                }
            }
        }
    }

    private void GenerateMaze(int iterations)
    {
        // Paso inicial
        for (int i = 0; i < this.grid.Width; i++)
        {
            for (int j = 0; j < this.grid.Height; j++)
            {
                grid.SetItem(i, j, RandomUtil.Chance(0.48f));
                if (j.InRange(0, 3) || j.InRange(grid.Height - 1, grid.Height - 4))
                    grid.SetItem(i, j, true);
            }
        }

        // Automata celular
        for (int iteration = 0; iteration < iterations; iteration++)
        {
            for (int i = 0; i < this.grid.Width; i++)
            {
                for (int j = 0; j < this.grid.Height; j++)
                {
                    bool[] neigbours = grid.ChessboardNeighbours(i, j);
                    int wallCount = this.CountWalls(neigbours);

                    bool currentItem = grid.GetItem(i, j);
                    if (currentItem) // Current item es pared
                    {
                        if (wallCount < 5) currentItem = false;
                    }
                    else // Current es espacio
                    {
                        if (wallCount > 4) currentItem = true;
                    }

                    grid.SetItem(i, j, currentItem);
                }
            }

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private int CountWalls(bool[] wallList)
    {
        int total = 0;
        foreach (var item in wallList)
        {
            if (item) total++;
        }
        return total;
    }
}
