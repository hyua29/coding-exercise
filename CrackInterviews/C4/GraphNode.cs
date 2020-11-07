using System.Collections.Generic;

namespace C4
{
    public class GraphNode<T>
    {
        public T Data { get; }

        public IList<GraphNode<T>> AdjcentNodes { get; private set; }

        public bool HasVisited;

        public GraphNode(T data)
        {
            this.Data = data;
            this.AdjcentNodes = new List<GraphNode<T>>();
            this.HasVisited = false;
        }

        public GraphNode(T data, IList<GraphNode<T>> adjcentNodes) : this(data)
        {
            this.AdjcentNodes = adjcentNodes;
        }

        public override string ToString()
        {
            return this.Data.ToString();
        }
    }
}