using Extensions.System;
using Extensions.Tuple;
using Extensions.UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    // Campos
    public SpriteRenderer wall;
    public EnemyScript enemy;
    public List<SpriteRenderer> tiles;

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

    public void RenderMaze(int downsample)
    {
        // Renderizar laberinto
        int step = 1 << downsample;
        for (int i = 0; i < this.grid.Width; i += step)
        {
            for (int j = 0; j < this.grid.Height; j += step)
            {
                if (this.grid.GetItem(i, j))
                {
					int flags = 0;
					if (this.grid.GetItem (i, j + step))
						flags += 1;
					if (this.grid.GetItem (i + step, j))
						flags += 2;
					if (this.grid.GetItem (i, j - step))
						flags += 4;
					if (this.grid.GetItem (i - step, j))
						flags += 8;
					
					var wall = GameObject.Instantiate<SpriteRenderer>(this.tiles[flags]);

                    //float cameraSize = Camera.main.orthographicSize;
                    
					float itemWidth = (this.width / this.grid.Width) * step;
					float itemHeight = (this.height / this.grid.Height) * step;

                    float hPosition = i.ToFloat().RemapTo(0, this.grid.Width - 1, -this.width * 0.5f, this.width * 0.5f);
                    float vPosition = j.ToFloat().RemapTo(0, this.grid.Height - 1, -this.height * 0.5f, this.height * 0.5f);
                    wall.transform.localPosition = new Vector2(hPosition, vPosition);
					wall.transform.localScale = new Vector2(itemWidth, itemHeight);
                    wall.transform.SetParent(this.transform, false);

					wall.gameObject.layer = LayerMask.NameToLayer ("sonar");
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
                grid.SetItem(i, j, RandomUtil.Chance(0.44f));
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

            for (int r = -6; r <= 6; r++)
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

                for (int r = -6; r <= 6; r++)
                {
                    grid.SetItem(i, index + r, false);
                }

            }
        }

        // Autómata celular
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
                        //if (wallCount < 4) aux.SetItem(i, j, false);
                        //else aux.SetItem(i, j, true);
						if (!(wallCount < 4))
							aux.SetItem(i, j, true);
                    }
                    else // Current es espacio
                    {
                        holeCount++;
                        if (wallCount > 3) aux.SetItem(i, j, true);
                        //else aux.SetItem(i, j, false);
                    }

                    List<bool> neigboursList = new List<bool>();
                    neigboursList.Add(neigbours[1]);
                    neigboursList.Add(neigbours[3]);
                    neigboursList.Add(neigbours[4]);
                    neigboursList.Add(neigbours[6]);

                    if (holeCount > 5 && wallCount == 1 && RandomUtil.Chance(0.003f)&& j > 15 && j < this.grid.Height - 15)
                    {
                        var enemy = GameObject.Instantiate<EnemyScript>(this.enemy);

                        float hPosition = i.ToFloat().RemapTo(0, this.grid.Width - 1, -this.width * 0.5f, this.width * 0.5f);
                        float vPosition = j.ToFloat().RemapTo(0, this.grid.Height - 1, -this.height * 0.5f, this.height * 0.5f);

                        enemy.transform.localPosition = new Vector2(hPosition, vPosition);
                        enemy.transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);
                        enemy.transform.SetParent(this.transform, false);

						enemy.gameObject.layer = LayerMask.NameToLayer ("sonar");

                        /*switch (EnemyCase(neigboursList))
                        {
                            // El enemigo tendrá un muro arriba
                            case 0:
                                break;
                            // El enemigo tendrá un muro a la izquierda
                            case 1:
                                break;
                            // El enemigo tendrá un muro a la derecha
                            case 2:
                                break;
                            // El enemigo tendrá un muro abajo
                            case 3:
                                break;
                            default:
                                break;
                        }*/
                    }

                }
                holeCount = 0;
            }
            this.grid = aux;
        }   
        
		/*
        // Flood fill
        HashSet<Point2D> closedPoints = new HashSet<Point2D>();
        for (int i = 0; i < this.grid.Width; i++)
        {
            for (int j = 0; j < this.grid.Height; j++)
            {
                if (!closedPoints.Contains(new Point2D(i, j)))
                {
                    Queue<Point2D> queuePoints = new Queue<Point2D>();
                    List<Point2D> changingPoints = new List<Point2D>();

                    var pt = new Point2D(i, j);
                    queuePoints.Enqueue(pt);
                    closedPoints.Add(pt);
                    changingPoints.Add(pt);

                    int pointCount = 1;
                    bool pointState = this.grid[i, j];

                    while (queuePoints.Count > 0)
                    {
                        var point = queuePoints.Dequeue();

                        var nPoint = new Point2D(point.X - 1, point.Y);
                        if (point.X > 0 && this.grid[nPoint] == pointState && !closedPoints.Contains(nPoint))
                        {
                            queuePoints.Enqueue(nPoint);
                            closedPoints.Add(nPoint);
                            changingPoints.Add(nPoint);
                            pointCount++;
                        }

                        nPoint = new Point2D(point.X + 1, point.Y);
                        if (point.X < this.grid.Width - 1 && this.grid[nPoint] == pointState && !closedPoints.Contains(nPoint))
                        {
                            queuePoints.Enqueue(nPoint);
                            closedPoints.Add(nPoint);
                            changingPoints.Add(nPoint);
                            pointCount++;
                        }
                        
                        nPoint = new Point2D(point.X, point.Y - 1);
                        if (point.Y > 0 && this.grid[nPoint] == pointState && !closedPoints.Contains(nPoint))
                        {
                            queuePoints.Enqueue(nPoint);
                            closedPoints.Add(nPoint);
                            changingPoints.Add(nPoint);
                            pointCount++;
                        }
                        
                        nPoint = new Point2D(point.X, point.Y + 1);
                        if (point.Y < this.grid.Height - 1 && this.grid[nPoint] == pointState && !closedPoints.Contains(nPoint))
                        {
                            queuePoints.Enqueue(nPoint);
                            closedPoints.Add(nPoint);
                            changingPoints.Add(nPoint);
                            pointCount++;
                        }

                        //if (pointCount > 12)
                            //continue;
                    }

                    if (pointCount <= 12)
                    {
                        foreach (var item in changingPoints)
                        {
                            this.grid.SetItem(item.X, item.Y, !pointState);
                        }
                    }

                }
            }
        }*/

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

    private int EnemyCase(List<bool> neightbours)
    {
        int res = 0;

        if (neightbours.ToArray()[0])
        {
            res = 0;
        }
        else
        {
            if (neightbours.ToArray()[1])
            {
                res = 1;
            }
            else
            {
                if (neightbours.ToArray()[2])
                {
                    res = 2;
                }else
                {
                    res = 3;
                }
            }

        }

        return res;
    }

}
