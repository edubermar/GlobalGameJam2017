using Extensions.System;
using Extensions.UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    // Campos
    public SpriteRenderer wall;
    public EnemyScript enemy;

    public float width;
    public float height;
    
    private Grid2D<bool> grid = new Grid2D<bool>(32, 64);
    //private Grid2D<bool> gridAux = new Grid2D<bool>(32, 64);

    public static event Action OnDestroyRequest = delegate { };

    private float offset = 0.0f;
    private float currentPosition = 0.0f;

    // Propiedades
    public float Offset
    {
        get { return this.offset; }
        set { this.offset = value; }
    }

    public float CurrentPosition
    {
        get { return this.currentPosition; }
        set { this.currentPosition = value; }
    }

    // Métodos
	private void Start()
    {
        this.grid.Wrapping = true;
        //this.GenerateMaze(0);
        //this.RenderMaze();
	}

    public void RenderMaze()
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
                    wall.transform.localPosition = new Vector2(hPosition, vPosition);
                    wall.transform.localScale = Vector3.one * itemWidth;
                    wall.transform.SetParent(this.transform, false);
                }
            }
        }
    }

    public void GenerateMaze(float gameWorldPosition, float perlinHeight, int iterations)
    {
        this.transform.DestroyChildren();

        // Paso inicial
        this.grid.Wrapping = true;

        for (int i = 0; i < this.grid.Width; i++)
        {
            for (int j = 0; j < this.grid.Height; j++)
            {
                grid.SetItem(i, j, RandomUtil.Chance(0.52f));
                if (j.InRange(0, 2) || j.InRange(grid.Height - 1, grid.Height - 3))
                    grid.SetItem(i, j, true);
            }
        }
        
        // Perlin noise (Generar camino seguro)
        for (int i = 0; i < this.grid.Width; i++)
        {
            float xPos = i.ToFloat().RemapTo(0, this.grid.Width - 1, this.width * -0.5f, this.width * 0.5f);
            float noiseValue = Mathf.PerlinNoise(xPos + gameWorldPosition, perlinHeight);
            int index = (int)(noiseValue.RemapTo(0.0f, 1.0f, 4, grid.Height - 5));

            for (int r = -2; r <= 2; r++)
            {
                grid.SetItem(i, index + r, false);
            }
        }

        // Perlin noise (Generar ramas)
        int totalBranches = UnityEngine.Random.Range(1, 4);
        for (int branch = 0; branch < totalBranches; branch++)
        {
            int branchStartX = UnityEngine.Random.Range(0, this.grid.Width);
            int branchLength = (int)RandomUtil.NextGaussianRanged(0.0f, this.grid.Width.ToFloat());
            for (int i = branchStartX; i < branchStartX + branchLength; i++)
            {
                float xPos = branchStartX.ToFloat().RemapTo(0, this.grid.Width - 1, this.width * -0.5f, this.width * 0.5f);
                float yPos = i.ToFloat().RemapTo(0, this.grid.Width - 1, this.width * -0.5f, this.width * 0.5f);
                float noiseValue = Mathf.PerlinNoise(xPos, yPos);
                int index = (int)(noiseValue.RemapTo(0.0f, 1.0f, 4, grid.Height - 5));

                for (int r = -2; r <= 2; r++)
                {
                    grid.SetItem(i, index + r, false);
                }

            }
        }

        // Automata celular
        for (int iteration = 0; iteration < iterations; iteration++)
        {
            Grid2D<bool> aux = new Grid2D<bool>(this.grid.Width, this.grid.Height);
            aux.Wrapping = true;

            for (int i = 0; i < this.grid.Width; i++)
            {
                int holeCount = 0;
                for (int j = 0; j < this.grid.Height; j++)
                {
                    
                    bool[] neigbours = this.grid.ChessboardNeighbours(i, j);
                    int wallCount = this.CountWalls(neigbours);
                    bool currentItem = this.grid.GetItem(i, j);
                    
                    if (currentItem) // Current item es pared
                    {
                        if (wallCount < 4) aux.SetItem(i, j, false);
                        else aux.SetItem(i, j, true);
                    }
                    else // Current es espacio
                    {
                        holeCount++;
                        if (wallCount > 4) aux.SetItem(i, j, true);
                        else aux.SetItem(i, j, false);
                    }
                    if (holeCount > 5 && wallCount == 1)
                    {
                        var enemy = GameObject.Instantiate<EnemyScript>(this.enemy);

                        float hPosition = i.ToFloat().RemapTo(0, this.grid.Width - 1, -this.width * 0.5f, this.width * 0.5f);
                        float vPosition = j.ToFloat().RemapTo(0, this.grid.Height - 1, -this.height * 0.5f, this.height * 0.5f);

                        enemy.transform.position = new Vector2(hPosition, vPosition);
                        enemy.transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);
                    }

                }
                holeCount = 0;
            }
            this.grid = aux;
        }   
        
    }
	
	// Update is called once per frame
	private void Update()
    {
        //this.transform.SetPositionX(this.offset - this.currentPosition);
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
