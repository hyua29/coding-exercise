using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace C4
{
    public static class GraphHelper
    {
        private static readonly Random Random = new Random();

        public static Graph<Guid> GenerateSingleDirectedGraphWithNoCycle(int depth = 10)
        {
            var root = new GraphNode<Guid>(Guid.NewGuid());
            GraphHelper.GenerateNextLevelNoCycle(root, depth - 1);

            return new Graph<Guid>(new List<GraphNode<Guid>>() {root});
        }

        public static Graph<Guid> GenerateDirectedGraphWithNoCycle(int depth = 10)
        {
            var dummyNode = new GraphNode<Guid>(Guid.NewGuid());
            GraphHelper.GenerateNextLevelNoCycle(dummyNode, depth);

            return new Graph<Guid>(dummyNode.AdjcentNodes);
        }
        
        public static Graph<Guid> GenerateSingleDirectedGraphWithCycles(int depth = 10, int cycleCount = 2, int cyclePercentage = 10)
        {
            var root = new GraphNode<Guid>(Guid.NewGuid());
            var nodeList = new List<GraphNode<Guid>>() { root };
            GraphHelper.GenerateNextLevelHasCycles(root, nodeList, depth - 1, cycleCount, cyclePercentage);

            return new Graph<Guid>(new List<GraphNode<Guid>>() { root });
        }

        public static Graph<Guid> GenerateDirectedGraphWithCycles(int depth = 10, int cycleCount = 2, int cyclePercentage = 10)
        {
            var nodeList = new List<GraphNode<Guid>>();
            var dummyNode = new GraphNode<Guid>(Guid.NewGuid());
            GraphHelper.GenerateNextLevelHasCycles(dummyNode, nodeList, depth, cycleCount, cyclePercentage);

            return new Graph<Guid>(dummyNode.AdjcentNodes);
        }

        private static void GenerateNextLevelHasCycles(GraphNode<Guid> node, IList<GraphNode<Guid>> nodeList, int remainedDepth, int cycleCount, int cyclePercentage)
        {
            // Total depth might go over remained depth if cycle count has not reached 0 
            if (remainedDepth < 1 && cycleCount < 1) return;

            remainedDepth--;

            // Each node has 0 to 10 adjacent nodes
            var adjNodeCount = Random.Next(0, 10);
            for (int i = 0; i < adjNodeCount; i++)
            {
                var hasCycle = Random.Next(1, 100) < cyclePercentage;
                if (hasCycle)
                {
                    // Add an arbitrary existing node to current node to form a cycle  
                    GraphNode<Guid> existingNode = null;
                    do
                    {
                        existingNode = nodeList[Random.Next(0, nodeList.Count - 1)];
                    } while (node == existingNode);

                    node.AdjcentNodes.Add(existingNode);
                    cycleCount--;
                }
                else
                {
                    var childNode = new GraphNode<Guid>(Guid.NewGuid());
                    node.AdjcentNodes.Add(childNode);
                    nodeList.Add(childNode);
                    GraphHelper.GenerateNextLevelHasCycles(childNode, nodeList, remainedDepth, cycleCount, cyclePercentage);   
                }
            }
        }

        private static void GenerateNextLevelNoCycle(GraphNode<Guid> node, int remainedDepth)
        {
            if (remainedDepth < 1) return;

            remainedDepth--;
            var adjNodeCount = Random.Next(10);
            for (int i = 0; i < adjNodeCount; i++)
            {
                var childNode = new GraphNode<Guid>(Guid.NewGuid());
                node.AdjcentNodes.Add(childNode);
                GraphHelper.GenerateNextLevelNoCycle(childNode, remainedDepth);
            }
        }
    }

    [TestFixture]
    public class GraphHelperTests
    {
        public void GenerateSingleDirectedGraphWithNoCycle_Test()
        {
            var graph = GraphHelper.GenerateSingleDirectedGraphWithNoCycle(15);
            Assert.That(graph.Nodes.Count, Is.EqualTo(1));
        }
        
        public void GenerateSingleDirectedGraphWithCycles_Test()
        {
            var graph = GraphHelper.GenerateSingleDirectedGraphWithCycles(15);
            Assert.That(graph.Nodes.Count, Is.EqualTo(1));
        }
    }
}