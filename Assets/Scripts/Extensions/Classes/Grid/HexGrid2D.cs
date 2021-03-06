using System;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions.UnityEngine
{
    public class HexGrid2D<T> : IEnumerable<T>
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Campos
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Propiedades básicas
        private int width;
        private int height;
        
        // Propiedades específicas
        private float edgeSize;
        private HexGridType gridType;
        private bool wrapping;
        
        // Estructura de datos
        private T[,] gridData;
        
        // Valores constantes
        private const float sqrt3 = 1.73205080757f;
        private const float areaFactor = 2.59807621135f; // 3 * sqrt3 / 2
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Propiedades
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Propiedades básicas
        public float Area
        {
            get
            {
                return (HexGrid2D<T>.areaFactor * this.edgeSize * this.edgeSize) * (this.height * width);
            }
        }
        
        public int Height
        {
            get { return this.height; }
        }
        
        public int Width
        {
            get { return this.width; }
        }
        
        // Propiedades específicas
        public float EdgeSize
        {
            get { return this.edgeSize; }
            set { this.edgeSize = value > 0.0f ? value : 0.0f; }
        }
        
        public HexGridType GridType
        {
            get { return this.gridType; }
            set { this.gridType = value; }
        }
        
        public bool Wrapping
        {
            get { return this.wrapping; }
            set { this.wrapping = value; }
        }
        
        // Indizadores
        public T this[HexPoint2D p]
        {
            get { return this.GetItem(p.X, p.Y); }
        }
        
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
        public HexGrid2D(int width, int height, float edgeSize = 0, HexGridType hexGridType = HexGridType.FlatTopEvenOffset)
        {
            if (width <= 0)
                throw new ArgumentOutOfRangeException("width");
            if (height <= 0)
                throw new ArgumentOutOfRangeException("height");
            
            this.width = width;
            this.height = height;
            
            this.gridData = new T[width, height];
            
            this.EdgeSize = edgeSize;
            this.gridType = hexGridType;
        }
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos de obtención de elementos
        public T GetItem(HexPoint2D hexPoint)
        {
            Point2D point = this.NormalizedToRectangular(hexPoint);
            return this.GetItem(point.X, point.Y);
        }
        
        public T GetItem(Point2D point)
        {
            return this.GetItem(point.X, point.Y);
        }
        
        private T GetItem(int x, int y)
        {
            if (this.wrapping)
                return this.gridData[this.ModWidth(x), this.ModHeight(y)];
            else
                return this.gridData[x, y];
        }
        
        // Métodos de asignación de elementos
        public void SetItem(HexPoint2D hexPoint, T item)
        {
            Point2D point = this.NormalizedToRectangular(hexPoint);
            this.SetItem(point.X, point.Y, item);
        }
        
        public void SetItem(Point2D point, T item)
        {
            this.SetItem(point.X, point.Y, item);
        }
        
        private void SetItem(int x, int y, T item)
        {
            if (this.wrapping)
                this.gridData[this.ModWidth(x), this.ModHeight(y)] = item;
            else
                this.gridData[x, y] = item;
        }
        
        // Métodos de información sobre adyacencia
        public T[] Neighbours(int x, int y)
        {
            HexPoint2D h = this.RectangularToNormalized(new Point2D(x, y));
            if (wrapping)
            {
                return new T[]
                {
                    this.GetItem(new HexPoint2D(h.X + 1, h.Y - 1)),
                    this.GetItem(new HexPoint2D(h.X + 1, h.Y    )),
                    this.GetItem(new HexPoint2D(h.X - 1, h.Y + 1)),
                    this.GetItem(new HexPoint2D(h.X    , h.Y + 1)),
                    this.GetItem(new HexPoint2D(h.X - 1, h.Y    )),
                    this.GetItem(new HexPoint2D(h.X    , h.Y - 1)),
                };
            }
            else
            {
                // TODO
                return null;
            }
        }
        
        // Métodos de manipulación
        public void Fill(T item)
        {
            for (int i = 0; i < this.width; i++)
                for (int j = 0; j < this.height; j++)
                    this.gridData[i, j] = item;
        }
        
        private void Swap(int sourceX, int sourceY, int targetX, int targetY)
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
        
        // Métodos de transformación de coordenadas
        public Vector2 ToRectangular(int x, int y)
        {
            switch (this.gridType)
            {
                case HexGridType.FlatTopOddOffset:
                    return new Vector2(this.edgeSize * 1.5f * x,
                        this.edgeSize * HexGrid2D<T>.sqrt3 * (y + (0.5f * (x & 1))));
                case HexGridType.FlatTopEvenOffset:
                    return new Vector2(this.edgeSize * 1.5f * x,
                        this.edgeSize * HexGrid2D<T>.sqrt3 * (y - (0.5f * (x & 1))));
                case HexGridType.VertexTopOddOffset:
                    return new Vector2(this.edgeSize * HexGrid2D<T>.sqrt3 * (x + (0.5f * (y & 1))),
                                       this.edgeSize * 1.5f * y);
                case HexGridType.VertexTopEvenOffset:
                    return new Vector2(this.edgeSize * HexGrid2D<T>.sqrt3 * (x - (0.5f * (y & 1))),
                                       this.edgeSize * 1.5f * y);
                default:
                    throw new InvalidOperationException();
            }
        }
        
        // Métodos de transformación de coordenadas
        public HexPoint2D RectangularToNormalized(Point2D p)
        {
            switch (this.gridType)
            {
                case HexGridType.FlatTopOddOffset:
                    return new HexPoint2D(p.X, p.Y - (p.X - (p.X & 1)) / 2);
                case HexGridType.FlatTopEvenOffset:
                    return new HexPoint2D(p.X, p.Y - (p.X + (p.X & 1)) / 2);
                case HexGridType.VertexTopOddOffset:
                    return new HexPoint2D(p.X - (p.Y - (p.Y & 1)) / 2, p.Y);
                case HexGridType.VertexTopEvenOffset:
                    return new HexPoint2D(p.X - (p.Y + (p.Y & 1)) / 2, p.Y);
                default:
                    throw new InvalidOperationException();
            }
        }
        
        public Point2D NormalizedToRectangular(HexPoint2D p)
        {
            switch (this.gridType)
            {
                case HexGridType.FlatTopOddOffset: // odd-q v-layout
                    return new Point2D(p.X, p.Y + (p.X - (p.X & 1)) / 2);
                case HexGridType.FlatTopEvenOffset: // even-q v-layout
                    return new Point2D(p.X, p.Y + (p.X + (p.X & 1)) / 2);
                case HexGridType.VertexTopOddOffset: // odd-r h-layout
                    return new Point2D(p.X + (p.Y - (p.Y & 1)) / 2, p.Y);
                case HexGridType.VertexTopEvenOffset: // even-r h-layout
                    return new Point2D(p.X + (p.Y + (p.Y & 1)) / 2, p.Y);
                default:
                    throw new InvalidOperationException();
            }
        }
        
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
        
        public sealed class HexGrid2DItem<U>
        {
            public Point2D RectangularCoordinates { get; set; }
            public HexPoint2D NormalizedCoordinates { get; set; }
            public U Item { get; set; }
        }
    }
    
}