using System;
using System.Collections.Generic;

namespace Extensions.Graph
{
    public class GraphNode<T>
    {
        // Private member-variables
        private T data;
        private List<GraphNode<T>> neighbors = null;
        private List<int> costs;
        
        public GraphNode()
        {
            
        }
        
        public GraphNode(T value)
        {
            this.data = value;
        }
        public GraphNode(T value, List<GraphNode<T>> neighbors)
        {
            this.data = value;
            this.neighbors = neighbors;
        }
        
        public T Value
        {
            get { return this.data; }
            set { this.data = value; }
        }
        
        public List<GraphNode<T>> Neighbors
        {
            get
            {
                if (this.neighbors == null)
                    this.neighbors = new List<GraphNode<T>>();
                
                return this.neighbors;
            }
        }
        
        public List<int> Costs
        {
            get
            {
                if (costs == null)
                    costs = new List<int>();
                
                return costs;
            }
        }
    }
    
    public class Graph<T> : IEnumerable<T>
    {
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Atributos
        // ---- ---- ---- ---- ---- ---- ---- ----
        private List<GraphNode<T>> nodeSet;
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Propiedades
        // ---- ---- ---- ---- ---- ---- ---- ----
        public List<GraphNode<T>> Nodes
        {
            get { return this.nodeSet; }
        }
        
        public int Count
        {
            get { return this.nodeSet.Count; }
        }
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Constructores
        // ---- ---- ---- ---- ---- ---- ---- ----
        public Graph()
        {
            this.nodeSet = new List<GraphNode<T>>();
        }
        
        public Graph(List<GraphNode<T>> nodeSet)
        {
            if (nodeSet == null) this.nodeSet = new List<GraphNode<T>>();
            else this.nodeSet = nodeSet;
        }
        
        // ---- ---- ---- ---- ---- ---- ---- ----
        // Métodos
        // ---- ---- ---- ---- ---- ---- ---- ----
        public void AddNode(GraphNode<T> node)
        {
            nodeSet.Add(node);
        }
        
        public void AddNode(T value)
        {
            nodeSet.Add(new GraphNode<T>(value));
        }
        
        public void AddDirectedEdge(GraphNode<T> from, GraphNode<T> to, int cost)
        {
            from.Neighbors.Add(to);
            from.Costs.Add(cost);
        }
        
        public void AddUndirectedEdge(GraphNode<T> from, GraphNode<T> to, int cost)
        {
            from.Neighbors.Add(to);
            from.Costs.Add(cost);
            
            to.Neighbors.Add(from);
            to.Costs.Add(cost);
        }
        
        public bool Contains(T value)
        {
            foreach (var item in this.nodeSet)
            {
                if (item.Value.Equals(value))
                    return true;
            }
            return false;
        }
        
        public bool Remove(T value)
        {
            // Eliminar el nodo especificado
            GraphNode<T> nodeToRemove = null;
            foreach (var item in this.nodeSet)
            {
                if (item.Value.Equals(value))
                    nodeToRemove = item;
            }
            
            if (nodeToRemove == null)
                return false;
            
            nodeSet.Remove(nodeToRemove);
            
            // Eliminar todas las aristas que referenciaban al nodo eliminado
            foreach (GraphNode<T> gnode in nodeSet)
            {
                int index = gnode.Neighbors.IndexOf(nodeToRemove);
                if (index != -1)
                {
                    gnode.Neighbors.RemoveAt(index);
                    gnode.Costs.RemoveAt(index);
                }
            }
            
            return true;
        }
        
        // Métodos de IEnumerable<T>
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in this.nodeSet)
                yield return item.Value;
        }
        
        global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        
    }
    
}