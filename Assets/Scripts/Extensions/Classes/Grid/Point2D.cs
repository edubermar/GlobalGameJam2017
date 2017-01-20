using System;

namespace Extensions.UnityEngine
{
    [Serializable]
    public struct Point2D
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Campos
        // ---- ---- ---- ---- ---- ---- ---- ----
        private int x;
        private int y;
        
        public static readonly Point2D Zero = new Point2D();
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Propiedades
        // ---- ---- ---- ---- ---- ---- ---- ----
        public bool IsZero
        { 
            get { return this.x == 0 && this.y == 0; }
        }
        
        public int X
        {
            get { return this.x; }
            set { this.x = value; }
        }
        
        public int Y
        { 
            get { return this.y; }
            set { this.y = value; }
        }
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Constructores
        // ---- ---- ---- ---- ---- ---- ---- ----
        public Point2D(int x, int y)
        {
            this.x = x;
            this.y = y; 
        }
        
        public Point2D(int dw)
        {
            this.x = (short)LOWORD(dw);
            this.y = (short)HIWORD(dw);
        }
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Operadores sobrecargados
        public static bool operator ==(Point2D left, Point2D right)
        {
            return left.X == right.X && left.Y == right.Y;
        }
        
        public static bool operator !=(Point2D left, Point2D right)
        { 
            return !(left == right);
        }
        
        // Métodos reemplazados
        public override bool Equals(object obj)
        {
            if (!(obj is Point2D))
                return false;
            
            Point2D comp = (Point2D)obj;
            return comp.X == this.X && comp.Y == this.Y; 
        }
        
        public override int GetHashCode()
        { 
            return x ^ y;
        }
        
        public override string ToString()
        {
            return string.Format("{X: {0}, Y: {1}}", this.x, this.y);
        }
        
        // Métodos estáticos
        private static int HIWORD(int n)
        { 
            return(n >> 16) & 0xffff;
        }
        
        private static int LOWORD(int n)
        {
            return n & 0xffff;
        }
    }
    
}