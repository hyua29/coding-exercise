using DataStructures.Extensions;
using DataStructures.Models;

namespace C2
{
    using System.Collections.Generic;
    using NUnit.Framework;

    [TestFixture]
    internal class ReverseLinkedList
    {
        public static SinglyLinkedListNode Calculate1(SinglyLinkedListNode head)
        {
            if (head?.Next == null)
                return head;

            var current = head;
            SinglyLinkedListNode previous = null;

            while (current != null)
            {
                var next = current.Next;

                current.Next = previous;
                previous = current;

                current = next;
            }

            return previous;
        }

        [TestCaseSource(nameof(GetTestData))]
        public void ReverseLinkedListTest(SinglyLinkedListNode head)
        {
            var result = Calculate1(head);
            if (head == null)
                Assert.IsNull(result);

            var list = head.ToList();
            var current = head;
            for (var i = 1; i < list.Count + 1; i++)
            {
                Assert.That(head.Data, Is.EqualTo(list[^i].Data));
                current = current.Next;
            }
        }

        private static IEnumerable<TestCaseData> GetTestData()
        {
            yield return new TestCaseData(new[] {1, 2, 1, 3}.ToLinkedList());
            yield return new TestCaseData(new[] {1, 1, 1, 1}.ToLinkedList());
            yield return new TestCaseData(new[] {1, 2, 3, 4}.ToLinkedList());
            yield return new TestCaseData(null);
        }
    }
}