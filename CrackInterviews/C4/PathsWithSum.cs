using System.Collections.Generic;
using DataStructures.Models;
using NUnit.Framework;

namespace C4
{
    public class PathsWithSum
    {
        public static int Calculate(BinaryTreeNode<int> root, int sum)
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
        public void Calculate_Test(BinaryTreeNode<int> root, int sum, int expectedResult)
        {
            Assert.That(PathsWithSum.Calculate(root, sum), Is.EqualTo(expectedResult));
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
                        RightNode = new BinaryTreeNode<int>(1)
                    }
                },
                RightNode = new BinaryTreeNode<int>(-3)
                {
                    RightNode = new BinaryTreeNode<int>(11)
                }
            }, 8, 3);
        }
    }
}