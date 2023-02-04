using DataStructures.Models;

namespace C4
{
    using System.Collections.Generic;
    using NUnit.Framework;

    public class MinimalTree
    {
        public static BinaryTreeNode<int> GetMinimalTree(int[] sortedArray)
        {
            return GetMidWithChildren(sortedArray, 0, sortedArray.Length - 1);
        }

        private static BinaryTreeNode<int> GetMidWithChildren(int[] sortedArray, int left, int right)
        {
            if (left > right) return null;

            var mid = (left + right) / 2;
            var midNode = new BinaryTreeNode<int>(sortedArray[mid])
            {
                LeftNode = GetMidWithChildren(sortedArray, left, mid - 1),
                RightNode = GetMidWithChildren(sortedArray, mid + 1, right)
            };

            return midNode;
        }

        [TestFixture]
        public class GetMinimalTreeTests
        {
            [TestCaseSource(nameof(GetTestData))]
            public void GetMinimalTree_Test(int[] sortedArray, int expectedRootValue)
            {
                var results = GetMinimalTree(sortedArray);

                Assert.True(ValidateBST.Calculate(results));

                Assert.That(results.Data, Is.EqualTo(expectedRootValue));
            }

            private static IEnumerable<TestCaseData> GetTestData()
            {
                yield return new TestCaseData(new int[] {1, 2, 3, 4, 5, 6, 7}, 4);
                yield return new TestCaseData(new int[] {1, 2, 3, 4}, 2);
                yield return new TestCaseData(new int[] {1}, 1);
            }
        }
    }
}