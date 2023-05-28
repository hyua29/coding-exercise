namespace C4;

using System;
using System.Collections.Generic;
using DataStructures.Models;
using NUnit.Framework;

public class Graph<T>
{
    private readonly Random _random;

    public Graph(IList<GraphNode<T>> nodes)
    {
        Nodes = nodes ?? throw new ArgumentNullException(nameof(Nodes), "Nodes list cannot be null");
        _random = new Random();
    }

    public IList<GraphNode<T>> Nodes { get; }

    public GraphNode<T> FindNode(Predicate<GraphNode<T>> predicate)
    {
        var set = new HashSet<GraphNode<T>>();
        var queue = new Queue<GraphNode<T>>();
        foreach (var n in Nodes)
        {
            set.Add(n);
            queue.Enqueue(n);
        }

        while (queue.TryDequeue(out var node))
        {
            if (predicate(node))
                return node;

            foreach (var n in node.AdjcentNodes)
                if (!set.Contains(n))
                {
                    set.Add(n);
                    queue.Enqueue(n);
                }
        }

        return null;
    }

    public int GetSize()
    {
        var set = new HashSet<GraphNode<T>>();
        var nodeCount = 0;
        var queue = new Queue<GraphNode<T>>();
        foreach (var n in Nodes)
        {
            nodeCount++;
            set.Add(n);
            queue.Enqueue(n);
        }

        while (queue.TryDequeue(out var node))
            foreach (var n in node.AdjcentNodes)
                if (!set.Contains(n))
                {
                    nodeCount++;
                    set.Add(n);
                    queue.Enqueue(n);
                }

        return nodeCount;
    }

    public GraphNode<T> GetArbitraryNode()
    {
        var set = new HashSet<GraphNode<T>>();
        var size = GetSize();
        var nodeToGet = _random.Next(1, size);

        GraphNode<T> nodeToReturn = null;

        var queue = new Queue<GraphNode<T>>();
        foreach (var n in Nodes)
        {
            nodeToGet--;
            set.Add(n);
            if (nodeToGet == 0)
            {
                nodeToReturn = n;
                break;
            }

            queue.Enqueue(n);
        }

        if (nodeToReturn == null)
            while (queue.TryDequeue(out var node))
                foreach (var n in node.AdjcentNodes)
                    if (!set.Contains(n))
                    {
                        nodeToGet--;
                        if (nodeToGet == 0)
                        {
                            nodeToReturn = n;
                            break;
                        }

                        set.Add(n);
                        queue.Enqueue(n);
                    }

        return nodeToReturn;
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
    [Repeat(5)]
    public void FindNode_GetArbitraryNode_Test()
    {
        var graph = GraphHelper.GenerateSingleDirectedGraphWithCycles();
        var node = graph.GetArbitraryNode();
        Assert.That(graph.FindNode(n => n == node), Is.EqualTo(node));
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
        var graph3 = GetBasicGraph();
        graph3.Nodes[2].AdjcentNodes[1].AdjcentNodes.Add(graph3.Nodes[0]);

        // Create a circle between first level nodes
        graph3.Nodes[0].AdjcentNodes.Add(graph3.Nodes[2]);
        Assert.That(graph3.GetSize(), Is.EqualTo(24));
    }

    private static IEnumerable<TestCaseData> GetTestData()
    {
        var graph1 = GetBasicGraph();

        yield return new TestCaseData(graph1, graph1.Nodes[0].Data, graph1.Nodes[0]);
        yield return new TestCaseData(graph1, graph1.Nodes[0].AdjcentNodes[3].Data,
            graph1.Nodes[0].AdjcentNodes[3]);
        yield return new TestCaseData(graph1, Guid.NewGuid(), null);

        var graph2 = GetBasicGraph();
        graph2.Nodes[2].AdjcentNodes[1].AdjcentNodes.Add(graph2.Nodes[0]);
        yield return new TestCaseData(graph2, graph2.Nodes[0].AdjcentNodes[3].Data,
            graph2.Nodes[0].AdjcentNodes[3]);
        yield return new TestCaseData(graph2, Guid.NewGuid(), null);

        var graph3 = GetBasicGraph();
        graph3.Nodes[2].AdjcentNodes[1].AdjcentNodes.Add(graph3.Nodes[0]);

        // Create a circle between first level nodes
        graph3.Nodes[0].AdjcentNodes.Add(graph3.Nodes[2]);
        yield return new TestCaseData(graph3, graph3.Nodes[0].AdjcentNodes[3].Data,
            graph3.Nodes[0].AdjcentNodes[3]);
        yield return new TestCaseData(graph3, Guid.NewGuid(), null);
    }

    private static Graph<Guid> GetBasicGraph()
    {
        var node1 = new GraphNode<Guid>(Guid.NewGuid());
        var node2 = new GraphNode<Guid>(Guid.NewGuid());
        var node3 = new GraphNode<Guid>(Guid.NewGuid());

        var graph1 = new Graph<Guid>(new List<GraphNode<Guid>> {node1, node2, node3});

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