using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;

namespace C4
{
    public class RouteBetweenNodes
    {
        public static bool CalculateByDFS<T>(GraphNode<T> currentNode, GraphNode<T> nodeToFind)
        {
            if (currentNode == nodeToFind)
                return true;

            currentNode.HasVisited = true;

            var nodeFound = false;
            foreach (var n in currentNode.AdjcentNodes)
            {
                if (!n.HasVisited)
                {
                    currentNode.HasVisited = true;
                    if (CalculateByDFS(n, nodeToFind))
                    {
                        nodeFound = true;
                        break;
                    }
                }
            }

            return nodeFound;
        }

        public static bool CalculateByBFS<T>(GraphNode<T> root, GraphNode<T> nodeToFind)
        {
            var queue = new Queue<GraphNode<T>>();
            
            queue.Enqueue(root);
            root.HasVisited = true;

            while (queue.TryDequeue(out var currentNode))
            {
                if (currentNode == nodeToFind) return true;

                foreach (var n in currentNode.AdjcentNodes)
                {
                    if (!n.HasVisited)
                    {
                        n.HasVisited = true;
                        queue.Enqueue(n);
                    }
                }
            }

            return false;
        }
    }

    [TestFixture]
    public class RouteBetweenNodesTest
    {
        [Test, Repeat(5)]
        public void CalculateByDFS_Test()
        {
            var timer = new Timer((e) => Assert.Fail("Test has taken to long; It is likely to be stuck in an infinite loop"), null, 5000, -1);
            var graph1 = GraphHelper.GenerateSingleDirectedGraphWithNoCycle();
            Assert.That(RouteBetweenNodes.CalculateByDFS(graph1.Nodes[0], graph1.GetArbitraryNode()), Is.EqualTo(true));

            var graph2 = GraphHelper.GenerateSingleDirectedGraphWithNoCycle();
            Assert.That(RouteBetweenNodes.CalculateByDFS(graph2.Nodes[0], new GraphNode<Guid>(Guid.NewGuid())), Is.EqualTo(false));

            var graph3 = GraphHelper.GenerateSingleDirectedGraphWithCycles();
            Assert.That(RouteBetweenNodes.CalculateByDFS(graph3.Nodes[0], graph3.GetArbitraryNode()), Is.EqualTo(true));
            
            var graph4 = GraphHelper.GenerateSingleDirectedGraphWithCycles();
            Assert.That(RouteBetweenNodes.CalculateByDFS(graph4.Nodes[0], new GraphNode<Guid>(Guid.NewGuid())), Is.EqualTo(false));
        }

        [Test, Repeat(5)]
        public void CalculateByBFS_Test()
        {
            var timer = new Timer((e) => Assert.Fail("Test has taken to long; It is likely to be stuck in an infinite loop"), null, 5000, -1); 
            var graph1 = GraphHelper.GenerateSingleDirectedGraphWithNoCycle();
            Assert.That(RouteBetweenNodes.CalculateByBFS(graph1.Nodes[0], graph1.GetArbitraryNode()), Is.EqualTo(true));
            
            var graph2 = GraphHelper.GenerateSingleDirectedGraphWithNoCycle();
            Assert.That(RouteBetweenNodes.CalculateByBFS(graph2.Nodes[0], new GraphNode<Guid>(Guid.NewGuid())), Is.EqualTo(false));
            
            var graph3 = GraphHelper.GenerateSingleDirectedGraphWithCycles();
            Assert.That(RouteBetweenNodes.CalculateByBFS(graph3.Nodes[0], graph3.GetArbitraryNode()), Is.EqualTo(true));
            
            var graph4 = GraphHelper.GenerateSingleDirectedGraphWithCycles();
            var size = graph4.GetSize();
            Console.WriteLine(size);
            Assert.That(RouteBetweenNodes.CalculateByBFS(graph4.Nodes[0], new GraphNode<Guid>(Guid.NewGuid())), Is.EqualTo(false));
        }
    }
}