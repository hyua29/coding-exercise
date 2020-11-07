using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace C4
{
    public class Graph<T>
    {
        private readonly Random _random;
        
        public IList<GraphNode<T>> Nodes { get; }

        public Graph(IList<GraphNode<T>> nodes)
        {
            if (nodes == null) throw new ArgumentNullException(nameof(this.Nodes), "Nodes list cannot be null");
            this.Nodes = nodes;
            _random = new Random();
        }

        public GraphNode<T> FindNode(Predicate<GraphNode<T>> predicate)
        {
            var queue = new Queue<GraphNode<T>>();
            foreach (var n in this.Nodes)
            {
                n.HasVisited = true;
                queue.Enqueue(n);
            }

            while (queue.TryDequeue(out var node))
            {
                if (predicate(node))
                    return node;
                
                foreach (var n in node.AdjcentNodes)
                {
                    if (!n.HasVisited)
                    {
                        node.HasVisited = true;
                        queue.Enqueue(n);
                    }
                }
            }

            return null;
        }

        public int GetSize()
        {
            int nodeCount = 0;
            var queue = new Queue<GraphNode<T>>();
            foreach (var n in this.Nodes)
            {
                nodeCount++;
                n.HasVisited = true;
                queue.Enqueue(n);
            }

            while (queue.TryDequeue(out var node))
            {
                foreach (var n in node.AdjcentNodes)
                {
                    if (!n.HasVisited)
                    {
                        nodeCount++;
                        node.HasVisited = true;
                        queue.Enqueue(n);
                    }
                }
            }

            return nodeCount;
        }

        public GraphNode<T> GetArbitraryNode()
        {
            var size = this.GetSize();
            var nodeToGet = _random.Next(1, size);

            var queue = new Queue<GraphNode<T>>();
            foreach (var n in this.Nodes)
            {
                nodeToGet--;
                n.HasVisited = true;
                if (nodeToGet == 0) return n;
                queue.Enqueue(n);
            }

            while (queue.TryDequeue(out var node))
            {
                foreach (var n in node.AdjcentNodes)
                {
                    if (!n.HasVisited)
                    {
                        nodeToGet--;
                        if (nodeToGet == 0) return n;
                        node.HasVisited = true;
                        queue.Enqueue(n);
                    }
                }
            }
            return null;
        }
    }

    [TestFixture]
    public class GraphTests
    {
        [TestCaseSource(nameof(GetTestData))]
        public void FindNode_Test(Graph<Guid> graph, Guid data, GraphNode<Guid> expectedResult)
        {
            Assert.That(graph.FindNode(node => node.Data.Equals(data)), Is.EqualTo(expectedResult));
        }
        
        [Test]
        public void GetSize_NoCycle_Test()
        {
            var graph = GetBasicGraph();
            Assert.That(graph.GetSize(), Is.EqualTo(24));
        }

        [Test]
        public void GetSize_HasCycles_Test()
        {
            Graph<Guid> graph3 = GetBasicGraph();
            graph3.Nodes[2].AdjcentNodes[1].AdjcentNodes.Add(graph3.Nodes[0]);

            // Create a circle between first level nodes
            graph3.Nodes[0].AdjcentNodes.Add(graph3.Nodes[2]);
            Assert.That(graph3.GetSize(), Is.EqualTo(24));

        }

        private static IEnumerable<TestCaseData> GetTestData()
        {
            Graph<Guid> graph1 = GetBasicGraph();

            yield return new TestCaseData(graph1, graph1.Nodes[0].Data, graph1.Nodes[0]);
            yield return new TestCaseData(graph1, graph1.Nodes[0].AdjcentNodes[3].Data, graph1.Nodes[0].AdjcentNodes[3]);
            yield return new TestCaseData(graph1, Guid.NewGuid(), null);
            
            Graph<Guid> graph2 = GetBasicGraph();
            graph2.Nodes[2].AdjcentNodes[1].AdjcentNodes.Add(graph2.Nodes[0]);
            yield return new TestCaseData(graph2, graph2.Nodes[0].AdjcentNodes[3].Data, graph2.Nodes[0].AdjcentNodes[3]);
            yield return new TestCaseData(graph2, Guid.NewGuid(), null);

            Graph<Guid> graph3 = GetBasicGraph();
            graph3.Nodes[2].AdjcentNodes[1].AdjcentNodes.Add(graph3.Nodes[0]);

            // Create a circle between first level nodes
            graph3.Nodes[0].AdjcentNodes.Add(graph3.Nodes[2]);
            yield return new TestCaseData(graph3, graph3.Nodes[0].AdjcentNodes[3].Data, graph3.Nodes[0].AdjcentNodes[3]);
            yield return new TestCaseData(graph3 , Guid.NewGuid(), null);
        }

        private static Graph<Guid> GetBasicGraph()
        {
            GraphNode<Guid> node1 = new GraphNode<Guid>(Guid.NewGuid());
            GraphNode<Guid> node2 = new GraphNode<Guid>(Guid.NewGuid());
            GraphNode<Guid> node3 = new GraphNode<Guid>(Guid.NewGuid());
            
            Graph<Guid> graph1 = new Graph<Guid>(new List<GraphNode<Guid>>() {node1, node2, node3});

            node1.AdjcentNodes.Add(new GraphNode<Guid>(Guid.NewGuid()));
            node1.AdjcentNodes.Add(new GraphNode<Guid>(Guid.NewGuid()));
            node1.AdjcentNodes.Add(new GraphNode<Guid>(Guid.NewGuid()));
            node1.AdjcentNodes.Add(new GraphNode<Guid>(Guid.NewGuid()));
            node1.AdjcentNodes.Add(new GraphNode<Guid>(Guid.NewGuid()));
            node1.AdjcentNodes.Add(new GraphNode<Guid>(Guid.NewGuid()));
            node1.AdjcentNodes.Add(new GraphNode<Guid>(Guid.NewGuid()));
            
            node1.AdjcentNodes[0].AdjcentNodes.Add(new GraphNode<Guid>(Guid.NewGuid()));
            node1.AdjcentNodes[3].AdjcentNodes.Add(new GraphNode<Guid>(Guid.NewGuid()));
            node1.AdjcentNodes[5].AdjcentNodes.Add(new GraphNode<Guid>(Guid.NewGuid()));
            
            node2.AdjcentNodes.Add(new GraphNode<Guid>(Guid.NewGuid()));
            node2.AdjcentNodes.Add(new GraphNode<Guid>(Guid.NewGuid()));
            
            node2.AdjcentNodes[0].AdjcentNodes.Add(new GraphNode<Guid>(Guid.NewGuid()));
            node2.AdjcentNodes[1].AdjcentNodes.Add(new GraphNode<Guid>(Guid.NewGuid()));
            
            node3.AdjcentNodes.Add(new GraphNode<Guid>(Guid.NewGuid()));
            node3.AdjcentNodes.Add(new GraphNode<Guid>(Guid.NewGuid()));
            node3.AdjcentNodes.Add(new GraphNode<Guid>(Guid.NewGuid()));
            node3.AdjcentNodes.Add(new GraphNode<Guid>(Guid.NewGuid()));
            node3.AdjcentNodes.Add(new GraphNode<Guid>(Guid.NewGuid()));
            node3.AdjcentNodes.Add(new GraphNode<Guid>(Guid.NewGuid()));
            
            node3.AdjcentNodes[3].AdjcentNodes.Add(new GraphNode<Guid>(Guid.NewGuid()));

            return graph1;
        }
    }
}