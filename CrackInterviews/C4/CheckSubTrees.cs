using System.Collections.Generic;
using C4.Models;
using NUnit.Framework;

namespace C4
{
    public class CheckSubtrees
    {
        public static bool Calculate(BinaryTreeNode<int> t1, BinaryTreeNode<int> t2)
        {
            if (t1 == null || t2 == null) return false;

            return RecursiveSearch(t1, t2);
        }

        private static bool RecursiveSearch(BinaryTreeNode<int> mainTree, BinaryTreeNode<int> t2)
        {
            if (mainTree == null) return false;

            if (mainTree.Data == t2.Data && IsTheSame(mainTree, t2)) return true;

            var matchFound = false;
            if (mainTree.LeftNode != null) matchFound = RecursiveSearch(mainTree.LeftNode, t2);
            if (mainTree.RightNode != null) matchFound = matchFound || RecursiveSearch(mainTree.RightNode, t2);

            return matchFound;
        }

        private static bool IsTheSame(BinaryTreeNode<int> t1, BinaryTreeNode<int> t2)
        {
            var isTheSame = false;
            if (t1.LeftNode != null && t2.LeftNode != null)
            {
                isTheSame = IsTheSame(t1.LeftNode, t2.LeftNode);
            }
            else if (t1.LeftNode == null && t2.LeftNode == null)
            {
                isTheSame = true;
            }
            else
            {
                isTheSame = false;
            }

            if (t1.RightNode != null && t2.RightNode != null)
            {
                isTheSame = isTheSame && IsTheSame(t1.RightNode, t2.RightNode);
            }
            else if (t1.RightNode == null && t2.RightNode == null)
            {
                isTheSame = isTheSame && true;
            }
            else
            {
                isTheSame = false;
            }

            return isTheSame;
        }
    }

    [TestFixture]
    public class CheckSubtreesTests
    {

        [TestCaseSource(nameof(GetTestData))]
        public void CalculateTests(BinaryTreeNode<int> t1, BinaryTreeNode<int> t2, bool expectedResult)
        {
            Assert.That(CheckSubtrees.Calculate(t1, t2), Is.EqualTo(expectedResult));
        }

        private static IEnumerable<TestCaseData> GetTestData()
        {
            yield return new TestCaseData(null, null, false);

            yield return new TestCaseData(
                new BinaryTreeNode<int>(1),
                new BinaryTreeNode<int>(1),
                true
            );

            yield return new TestCaseData(
                new BinaryTreeNode<int>(1),
                new BinaryTreeNode<int>(1)
                {
                    LeftNode = new BinaryTreeNode<int>(2)
                },
                false
            );

            yield return new TestCaseData(
                new BinaryTreeNode<int>(1)
                {
                    LeftNode = new BinaryTreeNode<int>(2)
                    {
                        LeftNode = new BinaryTreeNode<int>(4),
                        RightNode = new BinaryTreeNode<int>(5)
                    },
                    RightNode = new BinaryTreeNode<int>(3)
                },
                new BinaryTreeNode<int>(2)
                {
                    LeftNode = new BinaryTreeNode<int>(4)
                },
                false
            );

            yield return new TestCaseData(
                new BinaryTreeNode<int>(1)
                {
                    LeftNode = new BinaryTreeNode<int>(2)
                    {
                        LeftNode = new BinaryTreeNode<int>(4),
                        RightNode = new BinaryTreeNode<int>(5)
                    },
                    RightNode = new BinaryTreeNode<int>(3)
                },
                new BinaryTreeNode<int>(2)
                {
                    LeftNode = new BinaryTreeNode<int>(4),
                    RightNode = new BinaryTreeNode<int>(5)
                },
                true
            );
        }
    }
}