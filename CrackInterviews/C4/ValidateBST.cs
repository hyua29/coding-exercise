using System.Collections.Generic;
using NUnit.Framework;

namespace C4
{
    internal class ValidateBST
    {
        internal static bool Calculate(BinaryTreeNode<int> root)
        {
            if (root == null) return false;

            return IsBST(root);
        }

        internal static bool IsBST(BinaryTreeNode<int> node)
        {
            if (node == null) return true;

            return (node.LeftNode == null || node.LeftNode.Data <= node.Data) &&
                   (node.RightNode == null || node.RightNode.Data > node.Data) && IsBST(node.LeftNode) &&
                   IsBST(node.RightNode);
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
        }
    }
}