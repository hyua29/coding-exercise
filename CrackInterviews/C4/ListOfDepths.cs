namespace C4
{
    using DataStructures.Extensions;
    using NUnit.Framework;
    using System.Collections.Generic;
    using DataStructures.Models;

    public class ListOfDepths
    {
        public static IList<SinglyLinkedListNode> GetListOfDepths(BinaryTreeNode<int> root)
        {
            if (root == null)
            {
                return null;
            }

            var results = new List<SinglyLinkedListNode>();

            var queue = new Queue<BinaryTreeNode<int>>();

            queue.Enqueue(root);
            queue.Enqueue(null);

            SinglyLinkedListNode currentLinkedListNode = null;
            while (queue.Count > 0)
            {
                if (queue.Peek() == null)
                {
                    // We've exhausted all nodes in the previous layer
                    queue.Dequeue();

                    if (queue.Count > 0)
                    {
                        queue.Enqueue(null);
                    }

                    results.Add(currentLinkedListNode);
                    currentLinkedListNode = null;

                    continue;
                }

                var currentTreeNode = queue.Dequeue();
                currentLinkedListNode = new SinglyLinkedListNode
                {
                    Data = currentTreeNode.Data,
                    Next = currentLinkedListNode
                };

                if (currentTreeNode.LeftNode != null)
                {
                    queue.Enqueue(currentTreeNode.LeftNode);
                }

                if (currentTreeNode.RightNode != null)
                {
                    queue.Enqueue(currentTreeNode.RightNode);
                }
            }

            return results;
        }
    }

    [TestFixture]
    public class ListOfDepthsTests
    {
        [TestCaseSource(nameof(GetTestData))]
        public void ListOfDepths_Test(BinaryTreeNode<int> root, IList<List<int>> expectedResult)
        {
            var results = ListOfDepths.GetListOfDepths(root);

            if (root == null)
            {
                Assert.Null(results);
                return;
            }

            Assert.That(results.Count, Is.EqualTo(expectedResult.Count));

            for (int i = 0; i < expectedResult.Count; i++)
            {
                var er = expectedResult[i];
                var ar = results[i].ToList();
                for (int j = 0; j < er.Count; j++)
                {
                    Assert.That(ar[j].Data, Is.EqualTo(er[j]));
                }
            }
        }

        private static IEnumerable<TestCaseData> GetTestData()
        {
            var root = new BinaryTreeNode<int>(1)
            {
                LeftNode = new BinaryTreeNode<int>(2),
                RightNode = new BinaryTreeNode<int>(3)
                {
                    LeftNode = new BinaryTreeNode<int>(4),
                    RightNode = new BinaryTreeNode<int>(5)
                }
            };

            var expectedResults = new List<List<int>> {new List<int> {1}, new List<int> {3, 2}, new List<int> {5, 4}};


            yield return new TestCaseData(root, expectedResults);

            var root2 = new BinaryTreeNode<int>(1)
            {
                LeftNode = new BinaryTreeNode<int>(2),
                RightNode = new BinaryTreeNode<int>(3)
                {
                    LeftNode = new BinaryTreeNode<int>(4)
                    {
                        LeftNode = new BinaryTreeNode<int>(6),
                        RightNode = new BinaryTreeNode<int>(7)
                    },
                    RightNode = new BinaryTreeNode<int>(5)
                    {
                        LeftNode = new BinaryTreeNode<int>(8),
                        RightNode = new BinaryTreeNode<int>(9)
                    }
                }
            };

            var expectedResults2 = new List<List<int>>
                {new List<int> {1}, new List<int> {3, 2}, new List<int> {5, 4}, new List<int> {9, 8, 7, 6}};

            yield return new TestCaseData(root2, expectedResults2);

            yield return new TestCaseData(null, null);
        }
    }
}