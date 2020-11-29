using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace C4
{
    public class ValidateBST
    {
        public static bool Calculate(BinaryTreeNode<int> root)
        {
            if (root == null) return false;

            return IsBST(root, int.MinValue, int.MaxValue);
        }

        /// <summary>
        ///     Check if the current node comply the BST constraints
        /// </summary>
        /// <param name="node"></param>
        /// Current node
        /// <param name="min"></param>
        /// Data of current node must be greater than min
        /// <param name="max"></param>
        /// Data of current node must be smaller or equal to max
        /// <returns>True if the current node and its children meet all BST constraints </returns>
        public static bool IsBST(BinaryTreeNode<int> node, int min, int max)
        {
            if (node == null) return true;
            Console.WriteLine($"current data is: {node.Data}; current min: {min}; current max: {max}");

            if (node.Data <= min || node.Data > max)
            {
                Console.WriteLine($"data {node.Data} is returning false");
                return false;
            }

            return IsBST(node.LeftNode, min, Math.Min(node.Data, max)) &&
                   IsBST(node.RightNode, Math.Max(node.Data, min), max);
        }
    }

    [TestFixture]
    public class ValidateBSTTests
    {
        [TestCaseSource(nameof(GetTestData))]
        public void Calculate_Test(BinaryTreeNode<int> root, bool expectedResult)
        {
            Assert.That(ValidateBST.Calculate(root), Is.EqualTo(expectedResult));
        }

        private static IEnumerable<TestCaseData> GetTestData()
        {
            yield return new TestCaseData(null, false);
            yield return new TestCaseData(new BinaryTreeNode<int>(1), true);

            var node1 = new BinaryTreeNode<int>(1) {LeftNode = new BinaryTreeNode<int>(0)};
            yield return new TestCaseData(node1, true);

            var node2 = new BinaryTreeNode<int>(1) {LeftNode = new BinaryTreeNode<int>(2)};
            yield return new TestCaseData(node2, false);

            var node3 = new BinaryTreeNode<int>(1)
                {LeftNode = new BinaryTreeNode<int>(1), RightNode = new BinaryTreeNode<int>(2)};
            yield return new TestCaseData(node3, true);

            var node4 = new BinaryTreeNode<int>(1)
                {LeftNode = new BinaryTreeNode<int>(1), RightNode = new BinaryTreeNode<int>(1)};
            yield return new TestCaseData(node4, false);

            var node5 = new BinaryTreeNode<int>(9)
            {
                LeftNode = new BinaryTreeNode<int>(1)
                {
                    RightNode = new BinaryTreeNode<int>(20)
                },
                RightNode = new BinaryTreeNode<int>(10)
            };
            yield return new TestCaseData(node5, false);
        }
    }
}