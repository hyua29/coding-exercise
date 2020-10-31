using System.Runtime.CompilerServices;
using System.Reflection;
using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;
using System.Text;

namespace C2
{
    [TestFixture]
    class ReverseLinkedList
    {
        public static SinglyLinkedListNode Calculate1(SinglyLinkedListNode head)
        {
            if (head?.Next == null)
                return head;

            SinglyLinkedListNode current = head;
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

        [TestCaseSource(nameof(ReverseLinkedList.GetTestData))]
        public void ReverseLinkedListTest(SinglyLinkedListNode head)
        {
            var result = ReverseLinkedList.Calculate1(head);
            if (head == null)
                Assert.IsNull(result);

            var list = head.ToList();
            var current = head;
            for (int i = 1; i < list.Count + 1; i++)
            {
                Assert.That(head.Data, Is.EqualTo(list[^i].Data));
                current = current.Next;
            }
        }

        private static IEnumerable<TestCaseData> GetTestData()
        {
            yield return new TestCaseData(new int[] { 1, 2, 1, 3 }.ToLinkedList());
            yield return new TestCaseData(new int[] { 1, 1, 1, 1 }.ToLinkedList());
            yield return new TestCaseData(new int[] { 1, 2, 3, 4 }.ToLinkedList());
            yield return new TestCaseData(null);
        }
    }
}