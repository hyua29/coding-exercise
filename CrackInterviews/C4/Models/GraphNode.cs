namespace C4.Models
{
    using System.Collections.Generic;

    public class GraphNode<T>
    {
        public bool HasVisited;

        public GraphNode(T data)
        {
            Data = data;
            AdjcentNodes = new List<GraphNode<T>>();
            HasVisited = false;
        }

        public GraphNode(T data, IList<GraphNode<T>> adjcentNodes) : this(data)
        {
            AdjcentNodes = adjcentNodes;
        }

        public T Data { get; }

        public IList<GraphNode<T>> AdjcentNodes { get; }

        public override string ToString()
        {
            return Data.ToString();
        }
    }
}