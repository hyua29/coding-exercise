namespace C4
{
    using System.Collections.Generic;
    using DataStructures.Models;
    using NUnit.Framework;

    public class ValidateBST
    {
        public static bool Calculate(BinaryTreeNode<int> root)
        {
            if (root == null) return false;

            return IsBst(root, int.MinValue, int.MaxValue);
        }

        private static bool IsBst(BinaryTreeNode<int> node, int min, int max)
        {
            if (node == null)
            {
                return true;
            }

            var isCurrentBst = min < node.Data && node.Data <= max;

            return isCurrentBst && IsBst(node.LeftNode, min, node.Data) && IsBst(node.RightNode, node.Data, max);
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