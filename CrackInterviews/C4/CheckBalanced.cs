using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace C4
{
    public class CheckBalanced
    {
        public static bool Calculate(BinaryTreeNode<int> root)
        {
            if (root == null) return false;
            bool isBalanced = true;
            GetHeight(root, ref isBalanced);
            return isBalanced;
        }

        public static int GetHeight(BinaryTreeNode<int> node, ref bool isBalanced)
        {
            if (node == null) return 0;

            var leftHeight = GetHeight(node.LeftNode, ref isBalanced);
            var rightHeight = GetHeight(node.RightNode, ref isBalanced);
            if (isBalanced) isBalanced = Math.Abs(leftHeight - rightHeight) <= 1;

            var height = Math.Max(leftHeight, rightHeight) + 1;
            return height;
        }
    }

    [TestFixture]
    public class CheckBalancedTests
    {
        [TestCaseSource(nameof(GetTestData))]
        public void Calculate_Test(BinaryTreeNode<int> root, bool expectedResult)
        {
            Assert.That(CheckBalanced.Calculate(root), Is.EqualTo(expectedResult));
        }

        private static IEnumerable<TestCaseData> GetTestData()
        {
            yield return new TestCaseData(null, false);
            yield return new TestCaseData(new BinaryTreeNode<int>(1), true);

            var node1 = new BinaryTreeNode<int>(1) {LeftNode = new BinaryTreeNode<int>(1)};
            yield return new TestCaseData(node1, true);

            var node2 = new BinaryTreeNode<int>(1)
                {LeftNode = new BinaryTreeNode<int>(1), RightNode = new BinaryTreeNode<int>(1)};
            yield return new TestCaseData(node2, true);

            var node3 = new BinaryTreeNode<int>(1)
            {
                LeftNode = new BinaryTreeNode<int>(1) {LeftNode = new BinaryTreeNode<int>(1)},
                RightNode = new BinaryTreeNode<int>(1)
            };
            yield return new TestCaseData(node3, true);

            var node4 = new BinaryTreeNode<int>(1)
                {LeftNode = new BinaryTreeNode<int>(1) {LeftNode = new BinaryTreeNode<int>(1)}};
            yield return new TestCaseData(node4, false);
        }
    }
}