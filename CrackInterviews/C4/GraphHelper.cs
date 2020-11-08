using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace C4
{
    public static class GraphHelper
    {
        private static readonly Random Random = new Random();

        public static Graph<Guid> GenerateSingleDirectedGraphWithNoCycle(int depth = 8)
        {
            var root = new GraphNode<Guid>(Guid.NewGuid());
            GenerateNextLevelWithoutCycle(root, depth - 1);

            return new Graph<Guid>(new List<GraphNode<Guid>> {root});
        }

        public static Graph<Guid> GenerateDirectedGraphWithNoCycle(int depth = 8)
        {
            var dummyNode = new GraphNode<Guid>(Guid.NewGuid());
            GenerateNextLevelWithoutCycle(dummyNode, depth);

            return new Graph<Guid>(dummyNode.AdjcentNodes);
        }

        public static Graph<Guid> GenerateSingleDirectedGraphWithCycles(
            int depth = 8,
            int cycleCount = 2,
            int cyclePercentage = 10)
        {
            var root = new GraphNode<Guid>(Guid.NewGuid());
            var nodeList = new List<GraphNode<Guid>> {root};
            int cycleCountCopy = cycleCount;
            GenerateNextLevelWithCycles(root, nodeList, depth - 1, ref cycleCountCopy, cyclePercentage);

            return new Graph<Guid>(new List<GraphNode<Guid>> {root});
        }

        public static Graph<Guid> GenerateDirectedGraphWithCycles(
            int depth = 8,
            int cycleCount = 2,
            int cyclePercentage = 10)
        {
            var nodeList = new List<GraphNode<Guid>>();
            var dummyNode = new GraphNode<Guid>(Guid.NewGuid());
            int cycleCountCopy = cycleCount;
            GenerateNextLevelWithCycles(dummyNode, nodeList, depth, ref cycleCountCopy, cyclePercentage);

            return new Graph<Guid>(dummyNode.AdjcentNodes);
        }

        private static void GenerateNextLevelWithCycles(
            GraphNode<Guid> node,
            IList<GraphNode<Guid>> nodeList,
            int remainedDepth,
            ref int cycleCount,
            int cyclePercentage)
        {
            // Total depth might go over remained depth if cycle count has not reached 0 
            if (remainedDepth < 1 && cycleCount < 1) return;

            remainedDepth--;

            // Each node has 0 to 10 adjacent nodes
            var adjNodeCount = Random.Next(0, 10);
            for (var i = 0; i < adjNodeCount; i++)
            {
                if (nodeList.Count > 1 && Random.Next(1, 100) < cyclePercentage)
                {
                    // Add an arbitrary existing node to current node to form a cycle; Repeat if the arbitrary node retrieved is the same as current node
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
                    GenerateNextLevelWithCycles(childNode, nodeList, remainedDepth, ref cycleCount, cyclePercentage);
                }
            }
        }

        private static void GenerateNextLevelWithoutCycle(GraphNode<Guid> node, int remainedDepth)
        {
            if (remainedDepth < 1) return;

            remainedDepth--;
            var adjNodeCount = Random.Next(0, 10);
            for (var i = 0; i < adjNodeCount; i++)
            {
                var childNode = new GraphNode<Guid>(Guid.NewGuid());
                node.AdjcentNodes.Add(childNode);
                GenerateNextLevelWithoutCycle(childNode, remainedDepth);
            }
        }
    }

    [TestFixture]
    public class GraphHelperTests
    {
        [Test]
        public void GenerateSingleDirectedGraphWithNoCycle_Test()
        {
            var graph = GraphHelper.GenerateSingleDirectedGraphWithNoCycle();
            Assert.That(graph.Nodes.Count, Is.EqualTo(1));
        }

        [Test]
        public void GenerateSingleDirectedGraphWithCycles_Test()
        {
            var graph = GraphHelper.GenerateSingleDirectedGraphWithCycles();
            Assert.That(graph.Nodes.Count, Is.EqualTo(1));
        }
    }
}