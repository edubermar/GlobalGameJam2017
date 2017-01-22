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
    public List<SpriteRenderer> tiles;
    public List<SpriteRenderer> props;

    public GameObject snek;
    public GameObject spida;

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
        this.GenerateTunnel();
        this.RenderMaze(1);
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
					wall.transform.localScale = new Vector3(itemWidth, itemHeight, 1.0f);
                    wall.transform.SetParent(this.transform, false);

					wall.gameObject.layer = LayerMask.NameToLayer ("sonar");
                }
            }
        }

        for (int i = 0; i < this.grid.Width; i += step * 4)
        {
            for (int j = 0; j < this.grid.Height; j += step * 4)
            {
                bool foundSolid = true;
                for (int x = i; x < i + 4; x += step)
                {
                    for (int y = j; y < j + 4; y += step)
                    {
                        if (!this.grid.GetItem(x, y))
                        {
                            foundSolid = false;
                            break;
                        }
                    }
                }

                if (foundSolid && RandomUtil.Chance(0.4f))
                {
                    //int randomIndex = UnityEngine.Random.Range(0, this.props.Count);
                    int randomIndex = RandomUtil.Chance(0.1f) ?
                        (RandomUtil.Chance(0.1f) ? UnityEngine.Random.Range(9, 15) : UnityEngine.Random.Range(16, 19)) :
                                      UnityEngine.Random.Range(0, 8);
                    var prop = GameObject.Instantiate<SpriteRenderer>(this.props[randomIndex]);

                    float itemWidth = (this.width / this.grid.Width) * step;
                    float itemHeight = (this.height / this.grid.Height) * step;

                    float hPosition = (i + step * 2).ToFloat().RemapTo(0, this.grid.Width - 1, -this.width * 0.5f, this.width * 0.5f);
                    float vPosition = (j + step * 2).ToFloat().RemapTo(0, this.grid.Height - 1, -this.height * 0.5f, this.height * 0.5f);
                    prop.transform.localPosition = new Vector2(hPosition, vPosition);
                    prop.transform.localScale = new Vector3(itemWidth * 2.0f, itemHeight * 2.0f, 1.0f);
                    prop.transform.SetParent(this.transform, false);

                    prop.gameObject.layer = LayerMask.NameToLayer ("sonar");
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

                    if (currentItem)
                    { // Current item es pared
                        //if (wallCount < 4) aux.SetItem(i, j, false);
                        //else aux.SetItem(i, j, true);
                        if (!(wallCount < 4))
                            aux.SetItem(i, j, true);
                    }
                    else
                    { // Current es espacio
                        if (wallCount > 3)
                            aux.SetItem(i, j, true);
                        //else aux.SetItem(i, j, false);
                    }

                    if (iterations == iteration + 1)
                    {
                        bool CheckColumn = false;
                        bool CheckColumnUp = false;
                        bool CheckColumnRight = false;
                        bool CheckColumnDown = false;
                        bool CheckColumnLeft = false;

                        if (!(aux.GetItem(i, j + 1) && aux.GetItem(i, j + 2) && aux.GetItem(i, j + 3) && aux.GetItem(i, j + 4) && aux.GetItem(i, j + 5)))
                        {
                            CheckColumnUp = true;
                        }
                        if (!(aux.GetItem(i - 1, j) && aux.GetItem(i - 2, j) && aux.GetItem(i - 3, j + 3) && aux.GetItem(i - 4, j) && aux.GetItem(i - 5, j)))
                        {
                            CheckColumnRight = true;
                        }
                        if (!(aux.GetItem(i, j - 1) && aux.GetItem(i, j - 2) && aux.GetItem(i, j - 3) && aux.GetItem(i, j - 4) && aux.GetItem(i, j - 5)))
                        {
                            CheckColumnDown = true;
                        }
                        if (!(aux.GetItem(i + 1, j) && aux.GetItem(i + 2, j) && aux.GetItem(i + 3, j + 3) && aux.GetItem(i + 4, j) && aux.GetItem(i - 5, j)))
                        {
                            CheckColumnRight = true;
                        }

                        int nextToEnemy = 0;
                        List<bool> neigboursList = new List<bool>();
                        neigboursList.Add(neigbours[1]);
                        neigboursList.Add(neigbours[3]);
                        neigboursList.Add(neigbours[4]);
                        neigboursList.Add(neigbours[6]);

                        if (neigboursList.ToArray()[0])
                            nextToEnemy++;
                        if (neigboursList.ToArray()[1])
                            nextToEnemy++;
                        if (neigboursList.ToArray()[2])
                            nextToEnemy++;
                        if (neigboursList.ToArray()[3])
                            nextToEnemy++;

                        if (!aux.GetItem(i + 1, j - 1))
                            CheckColumn = true;
                        if (!aux.GetItem(i - 1, j - 1))
                            CheckColumn = true;
                        if (!aux.GetItem(i + 1, j + 1))
                            CheckColumn = true;
                        if (!aux.GetItem(i - 1, j + 1))
                            CheckColumn = true;
					

                        //Arriba
                        if (aux.GetItem(i - 1, j + 1) && aux.GetItem(i + 1, j + 1) && !aux.GetItem(i - 1, j - 1) && !aux.GetItem(i + 1, j - 1))
                            CheckColumn = true;
                        //Derecha
                        if (aux.GetItem(i + 1, j + 1) && aux.GetItem(i + 1, j - 1) && !aux.GetItem(i - 1, j + 1) && !aux.GetItem(i - 1, j - 1))
                            CheckColumn = true;
                        //Abajo
                        if (aux.GetItem(i - 1, j - 1) && aux.GetItem(i + 1, j - 1) && !aux.GetItem(i - 1, j + 1) && !aux.GetItem(i + 1, j + 1))
                            CheckColumn = true;
                        //Izquierda
                        if (aux.GetItem(i - 1, j + 1) && aux.GetItem(i - 1, j - 1) && !aux.GetItem(i + 1, j + 1) && !aux.GetItem(i + 1, j - 1))
                            CheckColumn = true;
					

                        if (nextToEnemy == 1 && RandomUtil.Chance(0.01f) && j < 32 && j > 10 && (CheckColumnUp || CheckColumnRight || CheckColumnDown || CheckColumnLeft))
                        {
                                var selectedEnemy = RandomUtil.NextInRange<GameObject>(this.snek, this.spida);
                                var enemy = GameObject.Instantiate<GameObject>(selectedEnemy);

                                float hPosition = i.ToFloat().RemapTo(0, this.grid.Width - 1, -this.width * 0.5f, this.width * 0.5f);
                                float vPosition = j.ToFloat().RemapTo(0, this.grid.Height - 1, -this.height * 0.5f, this.height * 0.5f);

                                enemy.transform.localPosition = new Vector2(hPosition, vPosition);
                                //enemy.transform.localScale = new Vector3 (0.12f, 0.12f, 0.12f);
                                enemy.transform.SetParent(this.transform, false);

                                enemy.gameObject.layer = LayerMask.NameToLayer("sonar");

                        }

                        if (nextToEnemy == 1 && RandomUtil.Chance(0.01f) && j > 32 && j < 54 && (CheckColumnUp || CheckColumnRight || CheckColumnDown || CheckColumnLeft))
                        {
                            var selectedEnemy = RandomUtil.NextInRange<GameObject>(this.snek, this.spida);
                            var enemy = GameObject.Instantiate<GameObject>(selectedEnemy);

                            float hPosition = i.ToFloat().RemapTo(0, this.grid.Width - 1, -this.width * 0.5f, this.width * 0.5f);
                            float vPosition = j.ToFloat().RemapTo(0, this.grid.Height - 1, -this.height * 0.5f, this.height * 0.5f);

                            enemy.transform.localPosition = new Vector2(hPosition, vPosition);
                            enemy.transform.localScale.Scale(new Vector3(1f, -1f, 1f));
                            enemy.transform.SetRotationEulerZ(90f);
                            enemy.transform.SetParent(this.transform, false);

                            enemy.gameObject.layer = LayerMask.NameToLayer("sonar");

                        }
                        holeCount = 0;
                    

                    }
                }
            }
            this.grid = aux;
        }   

    }
	
    public void GenerateTunnel()
    {
        this.transform.DestroyChildren();

        // Paso inicial
        this.grid.Wrapping = true;

        for (int i = 0; i < this.grid.Width; i++)
        {
            for (int j = 0; j < this.grid.Height; j++)
            {
                float probability = Mathf.Abs(j - this.grid.Height / 2);
                grid.SetItem(i, j, RandomUtil.Chance(probability * 0.02f));
                if (j.InRange(0, 2) || j.InRange(grid.Height - 1, grid.Height - 3))
                    grid.SetItem(i, j, true);
            }
        }

        // Autómata celular
        for (int iteration = 0; iteration < 3; iteration++)
        {
            Grid2D<bool> aux = new Grid2D<bool>(this.grid.Width, this.grid.Height);
            aux.Wrapping = true;

            for (int i = 0; i < this.grid.Width; i++) {
                int holeCount = 0;
                for (int j = 0; j < this.grid.Height; j++) {
                    bool[] neigbours = this.grid.ChessboardNeighbours (i, j);
                    int wallCount = this.CountWalls (neigbours);
                    bool currentItem = this.grid.GetItem (i, j);

                    if (currentItem) // Current item es pared
                    {
                        if (!(wallCount < 4))
                            aux.SetItem(i, j, true);
                    }
                    else // Current es espacio
                    {
                        if (wallCount > 3)
                            aux.SetItem(i, j, true);
                    }
                }
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
