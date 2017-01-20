using System;
using System.Collections.Generic;

namespace Extensions.UnityEngine
{
    public class Grid2D<T> : IEnumerable<T>
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Campos
        // ---- ---- ---- ---- ---- ---- ---- ----
        private int width;
        private int height;
        
        private bool wrapping;
        
        private T[,] gridData;
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Propiedades
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Propiedades básicas
        public int Area
        {
            get { return this.height * width; }
        }
        
        public int Height
        {
            get { return this.height; }
        }
        
        public int Width
        {
            get { return this.width; }
        }
        
        public bool Wrapping
        {
            get { return this.wrapping; }
            set { this.wrapping = value; }
        }
        
        // Indizadores
        public T this[Point2D p]
        {
            get { return this.GetItem(p.X, p.Y); }
        }
        
        public T this[int x, int y]
        {
            get { return this.GetItem(x, y); }
        }
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Constructores
        // ---- ---- ---- ---- ---- ---- ---- ----
        public Grid2D(int width, int height)
        {
            this.width = width;
            this.height = height;
            
            this.gridData = new T[width, height];
        }
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos de gestión
        public T GetItem(int x, int y)
        {
            if (this.wrapping)
                return this.gridData[this.ModWidth(x), this.ModHeight(y)];
            else
                return this.gridData[x, y];
        }
        
        public void SetItem(int x, int y, T item)
        {
            if (this.wrapping)
                this.gridData[this.ModWidth(x), this.ModHeight(y)] = item;
            else
                this.gridData[x, y] = item;
        }
        
        // Métodos de información sobre adyacencia
        public T[] ManhattanNeighbours(int x, int y)
        {
            if (wrapping)
            {
                return new T[]
                {
                    this.gridData[this.ModWidth(x), this.ModHeight(y - 1)],
                    this.gridData[this.ModWidth(x - 1), this.ModHeight(y)],
                    this.gridData[this.ModWidth(x + 1), this.ModHeight(y)],
                    this.gridData[this.ModWidth(x), this.ModHeight(y + 1)]
                };
            }
            else
            {
                List<T> result = new List<T>(4);
                if (y > 0)
                    result.Add(this.gridData[x, y - 1]);
                if (x > 0)
                    result.Add(this.gridData[x - 1, y]);
                if (x < this.width - 1)
                    result.Add(this.gridData[x + 1, y]);
                if (y < this.height - 1)
                    result.Add(this.gridData[x, y + 1]);
                return result.ToArray();
            }
        }
        
        public T[] ChessboardNeighbours(int x, int y)
        {
            if (wrapping)
            {
                return new T[]
                {
                    this.gridData[this.ModWidth(x - 1), this.ModHeight(y - 1)],
                    this.gridData[this.ModWidth(x), this.ModHeight(y - 1)],
                    this.gridData[this.ModWidth(x + 1), this.ModHeight(y - 1)],
                    this.gridData[this.ModWidth(x - 1), this.ModHeight(y)],
                    this.gridData[this.ModWidth(x + 1), this.ModHeight(y)],
                    this.gridData[this.ModWidth(x - 1), this.ModHeight(y + 1)],
                    this.gridData[this.ModWidth(x), this.ModHeight(y + 1)],
                    this.gridData[this.ModWidth(x + 1), this.ModHeight(y + 1)]
                };
            }
            else
            {
                List<T> result = new List<T>(4);
                for (int i = x - 1; i <= x + 1; i++)
                {
                    for (int j = y - 1; j <= y + 1; j++)
                    {
                        if ((i >= 0) && (i < width) && (j >= 0) && (j < height))
                            result.Add(this.gridData[i, j]);
                    }
                }
                return result.ToArray();
            }
        }
        
        // Métodos de manipulación
        public void Fill(T item)
        {
            for (int i = 0; i < this.width; i++)
            {
                for (int j = 0; j < this.height; j++)
                {
                    this.gridData[i, j] = item;
                }
            }
        }
        
        public void Swap(int sourceX, int sourceY, int targetX, int targetY)
        {
            if (this.wrapping)
            {
                sourceX = this.ModWidth(sourceX);
                sourceY = this.ModHeight(sourceY);
                targetX = this.ModWidth(targetX);
                targetY = this.ModHeight(targetY);
            }
            
            T tempTarget = this.gridData[targetX, targetX];
            this.gridData[targetX, targetY] = this.gridData[sourceX, sourceY];
            this.gridData[sourceX, sourceY] = tempTarget;
        }
        
        // Métodos auxiliares
        private int ModHeight(int y)
        {
            int r = y % this.height;
            return r < 0 ? r + this.height : r;
        }
        
        private int ModWidth(int x)
        {
            int r = x % this.width;
            return r < 0 ? r + this.width : r;
        }
        
        // Métodos de IEnumerable<T>
        public IEnumerator<T> GetEnumerator()
        {
            foreach (T item in this.gridData)
                yield return item;
        }
        
        global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
    
}