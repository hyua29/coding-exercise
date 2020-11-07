using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace C4
{
    class RouteBetweenNodes
    {
        internal static bool CalculateByDFS(GraphNode<Guid> node1, GraphNode<Guid> node2)
        {
            if (node1 == node2)
                return true;

            node1.HasVisited = true;

            foreach (var n in node1.AdjcentNodes)
            {
                if (n == node1) {
                    return true;
                }
                
                if (!n.HasVisited)
                {
                    RouteBetweenNodes.CalculateByDFS(n, node2);
                }
            }

            return false;
        }

        internal static bool CalculateByBFS(Graph<Guid> graph, GraphNode<Guid> node1, GraphNode<Guid> node2)
        {
            if (node1 == node2)
                return true;

            return false;
        }
    }

    [TestFixture]
    public class RouteBetweenNodesTest
    {
        [TestCaseSource(nameof(GetTestData))]
        public void RouteBetweenNodes_Test(Graph<Guid> graph, GraphNode<Guid> node1, GraphNode<Guid> node2, bool expectedResult)
        {
            Assert.That(RouteBetweenNodes.CalculateByDFS(node1, node2), Is.EqualTo(expectedResult));
        }

        private static IEnumerable<TestCaseData> GetTestData()
        {
            yield return new TestCaseData(1);
        }
    }
}