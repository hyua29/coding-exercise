namespace C4;

using System;
using System.Collections.Generic;
using DataStructures.Models;
using NUnit.Framework;

public class PathsWithSum
{
    public static int CalculateWithCache(BinaryTreeNode<int> root, int targetSum)
    {
        if (root == null) return 0;


        return CalculateWithCacheImpl(root, 0, targetSum, new Dictionary<int, int>());
        ;
    }

    private static int CalculateWithCacheImpl(BinaryTreeNode<int> node, int currentSum, int targetSum,
        IDictionary<int, int> cache)
    {
        if (node == null) return 0;

        var updatedSum = currentSum + node.Data;
        var delta = updatedSum - targetSum;

        var pathCount = 0;
        if (cache.TryGetValue(delta, out var count) && count > 0) pathCount += count;

        if (cache.ContainsKey(updatedSum))
            cache[updatedSum]++;
        else
            cache.Add(updatedSum, 1);

        pathCount += CalculateWithCacheImpl(node.LeftNode, updatedSum, targetSum, cache);
        pathCount += CalculateWithCacheImpl(node.RightNode, updatedSum, targetSum, cache);

        cache[updatedSum]--;

        return pathCount;
    }

    public static int BruteForceCalculate(BinaryTreeNode<int> root, int sum)
    {
        if (root == null) return 0;

        return GetPaths(root, sum);
    }

    private static int GetPaths(BinaryTreeNode<int> node, int sum)
    {
        if (node == null) return 0;

        var pathFromCurrent = GetPathsFromNode(node, sum, 0);

        var pathFromLeft = GetPaths(node.LeftNode, sum);
        var pathFromRight = GetPaths(node.RightNode, sum);

        return pathFromCurrent + pathFromLeft + pathFromRight;
    }

    private static int GetPathsFromNode(BinaryTreeNode<int> node, int targerSum, int currentSum)
    {
        if (node == null) return 0;

        var newSum = currentSum + node.Data;

        var pathCount = 0;
        if (newSum == targerSum) pathCount++;

        pathCount += GetPathsFromNode(node.LeftNode, targerSum, newSum);
        pathCount += GetPathsFromNode(node.RightNode, targerSum, newSum);

        return pathCount;
    }
}

[TestFixture]
public class PathsWithSumTests
{
    [TestCaseSource(nameof(GetTestData))]
    public void BruteForceCalculate_Test(BinaryTreeNode<int> root, int sum, int expectedResult)
    {
        Assert.That(PathsWithSum.BruteForceCalculate(root, sum), Is.EqualTo(expectedResult));
    }

    [TestCaseSource(nameof(GetTestData))]
    public void CalculateWithCache_Test(BinaryTreeNode<int> root, int sum, int expectedResult)
    {
        Assert.That(PathsWithSum.BruteForceCalculate(root, sum), Is.EqualTo(expectedResult));
    }

    [Test]
    public void Bit_Test()
    {
        var one = 4;
        var shifted = one >> 4;


        Console.WriteLine(Convert.ToString(int.MaxValue, 2));
        Console.WriteLine(Convert.ToString(shifted, 2));
    }

    private static IEnumerable<TestCaseData> GetTestData()
    {
        yield return new TestCaseData(null, 8, 0);
        yield return new TestCaseData(new BinaryTreeNode<int>(10)
        {
            LeftNode = new BinaryTreeNode<int>(5)
            {
                LeftNode = new BinaryTreeNode<int>(3)
                {
                    LeftNode = new BinaryTreeNode<int>(3),
                    RightNode = new BinaryTreeNode<int>(-2)
                },
                RightNode = new BinaryTreeNode<int>(2)
                {
                    LeftNode = new BinaryTreeNode<int>(-4)
                    {
                        RightNode = new BinaryTreeNode<int>(5)
                        {
                            LeftNode = new BinaryTreeNode<int>(6)
                            {
                                RightNode = new BinaryTreeNode<int>(-6)
                            }
                        }
                    },
                    RightNode = new BinaryTreeNode<int>(1)
                }
            },
            RightNode = new BinaryTreeNode<int>(-3)
            {
                RightNode = new BinaryTreeNode<int>(11)
            }
        }, 8, 5);
    }
}