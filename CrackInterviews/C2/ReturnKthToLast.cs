using DataStructures.Extensions;
using DataStructures.Models;

namespace C2
{
    using System.Collections.Generic;
    using NUnit.Framework;

    [TestFixture]
    internal class ReturnKthToLast
    {
        public static SinglyLinkedListNode Calculate1(SinglyLinkedListNode head, int k)
        {
            if (head == null)
                return head;

            if (k == 0)
            {
                return null;
            }

            var current = head;
            var runner = current;

            var counter = k;

            while (counter > 0 && runner != null)
            {
                runner = runner.Next;
                counter--;
            }

            // The list is shorter than k. Return the entire list
            if (counter > 0)
            {
                return head;
            }

            while (runner != null)
            {
                current = current.Next;
                runner = runner.Next;
            }

            return current;
        }

        [TestCaseSource(nameof(GetTestData))]
        public void ReverseLinkedListTest(SinglyLinkedListNode head, int k, int[] expectedResults)
        {
            var results = Calculate1(head, k);
            if (head == null || k == 0)
            {
                Assert.IsNull(results);
                return;
            }

            var list = results.ToList();

            Assert.That(list.Count, Is.EqualTo(expectedResults.Length));

            for (int i = 0; i < list.Count; i++)
            {
                Assert.That(list[i].Data, Is.EqualTo(expectedResults[i]));
            }
        }

        private static IEnumerable<TestCaseData> GetTestData()
        {
            yield return new TestCaseData(new[] {1, 2, 3, 4, 5}.ToLinkedList(), 0, null);
            yield return new TestCaseData(new[] {1, 2, 3, 4, 5}.ToLinkedList(), 1, new[] {5});
            yield return new TestCaseData(new[] {1, 2, 3, 4, 5}.ToLinkedList(), 3, new[] {3, 4, 5});
            yield return new TestCaseData(new[] {1, 2, 3, 4, 5}.ToLinkedList(), 5, new[] {1, 2, 3, 4, 5});
            yield return new TestCaseData(new[] {1, 2, 3, 4, 5}.ToLinkedList(), 7, new[] {1, 2, 3, 4, 5});
            yield return new TestCaseData(null, 10, null);
        }
    }
}