namespace C4;

using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using DataStructures.Models;

public class FindCommonAncestor
{
    public static BinaryTreeNode<int> Calculate(BinaryTreeNode<int> root, BinaryTreeNode<int> p, BinaryTreeNode<int> q)
    {
        if (root == null || p == null | q == null)
        {
            return null;
        }

        if (root == p || root == q)
        {
            return root;
        }

        if (!NodePresent(root, p) || !NodePresent(root, q))
        {
            return null;
        }

        return CommonAncestorHelper(root, p, q);
    }

    private static BinaryTreeNode<int> CommonAncestorHelper(BinaryTreeNode<int> root, BinaryTreeNode<int> p,
        BinaryTreeNode<int> q)
    {
        Debug.Assert(root != null);

        if (root == p || root == q)
        {
            return root;
        }

        var pOnLeft = NodePresent(root.LeftNode, p);
        var qOnLeft = NodePresent(root.LeftNode, q);

        if (pOnLeft != qOnLeft)
        {
            return root;
        }

        var nextRoot = pOnLeft ? root.LeftNode : root.RightNode;

        return CommonAncestorHelper(nextRoot, p, q);
    }

    private static bool NodePresent(BinaryTreeNode<int> root, BinaryTreeNode<int> nodeToSearch)
    {
        if (root == null || nodeToSearch == null)
        {
            return false;
        }

        if (root == nodeToSearch)
        {
            return true;
        }

        return NodePresent(root.LeftNode, nodeToSearch) || NodePresent(root.RightNode, nodeToSearch);
    }

    [TestFixture]
    public class FindCommonAncestorTests
    {
        [TestCaseSource(nameof(GetTestData))]
        public void Calculate_Test(BinaryTreeNode<int> root, BinaryTreeNode<int> p, BinaryTreeNode<int> q,
            BinaryTreeNode<int> expectedCommonAncestor)
        {
            Assert.That(FindCommonAncestor.Calculate(root, p, q), Is.EqualTo(expectedCommonAncestor));
        }

        private static IEnumerable<TestCaseData> GetTestData()
        {
            yield return new TestCaseData(null, null, null, null);
            var p1 = new BinaryTreeNode<int>(4)
                {LeftNode = new BinaryTreeNode<int>(5), RightNode = new BinaryTreeNode<int>(6)};
            var q1 = new BinaryTreeNode<int>(7)
                {LeftNode = new BinaryTreeNode<int>(8), RightNode = new BinaryTreeNode<int>(9)};

            var root1 = new BinaryTreeNode<int>(1)
            {
                LeftNode = new BinaryTreeNode<int>(2) {LeftNode = p1, RightNode = q1},
                RightNode = new BinaryTreeNode<int>(3)
            };

            yield return new TestCaseData(root1, p1, q1, root1.LeftNode);

            var p2 = new BinaryTreeNode<int>(4)
                {LeftNode = new BinaryTreeNode<int>(5), RightNode = new BinaryTreeNode<int>(6)};
            var q2 = new BinaryTreeNode<int>(7)
                {LeftNode = new BinaryTreeNode<int>(8), RightNode = new BinaryTreeNode<int>(9) {LeftNode = p2}};

            var root2 = q2;

            yield return new TestCaseData(root2, p2, q2, q2);

            var p3 = new BinaryTreeNode<int>(4)
                {LeftNode = new BinaryTreeNode<int>(5), RightNode = new BinaryTreeNode<int>(6)};
            var q3 = new BinaryTreeNode<int>(7)
                {LeftNode = new BinaryTreeNode<int>(8), RightNode = new BinaryTreeNode<int>(9)};

            var root3 = new BinaryTreeNode<int>(1)
            {
                LeftNode = new BinaryTreeNode<int>(2) {LeftNode = p3, RightNode = new BinaryTreeNode<int>(10)},
                RightNode = new BinaryTreeNode<int>(3)
            };

            yield return new TestCaseData(root3, p3, q3, null);
        }
    }
}