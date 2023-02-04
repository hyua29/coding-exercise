using DataStructures.Extensions;
using DataStructures.Models;

namespace C2
{
    using System.Collections.Generic;
    using NUnit.Framework;

    [TestFixture]
    internal class LoopDetection
    {
        public static SinglyLinkedListNode Calculate1(SinglyLinkedListNode head)
        {
            if (head?.Next == null)
                return null;

            var fast = head.Next.Next;
            var slow = head;

            while (fast != null && fast != slow)
            {
                slow = slow.Next;
                fast = fast.Next?.Next;
            }

            // No loop is detected
            if (fast == null)
            {
                return null;
            }

            var current = head;

            while (current != slow)
            {
                current = current.Next;
                slow = slow.Next;
            }

            return current;
        }

        [TestCaseSource(nameof(GetTestData))]
        public void LoopDetectionTest(SinglyLinkedListNode head, SinglyLinkedListNode expectedNode)
        {
            var result = Calculate1(head);
            if (head == null)
                Assert.IsNull(result);

            Assert.That(result, Is.EqualTo(expectedNode));
        }

        private static IEnumerable<TestCaseData> GetTestData()
        {
            var linkedList1 = new[] {1, 2, 3, 4, 5}.ToLinkedList();
            var loop1 = new[] {7, 8, 9}.ToLinkedList();
            var linkedListLast1 = linkedList1.GetLastNode();
            linkedListLast1.Next = loop1;
            loop1.Next = linkedListLast1;
            yield return new TestCaseData(linkedList1, linkedListLast1);

            var linkedList2 = new[] {1, 2, 3}.ToLinkedList();
            var loop2 = new[] {4, 5, 6, 7, 8, 9}.ToLinkedList();
            var linkedListLast2 = linkedList2.GetLastNode();
            linkedListLast2.Next = loop2;
            loop2.Next = linkedListLast2;
            yield return new TestCaseData(linkedList2, linkedListLast2);

            var linkedList3 = new[] {1, 2, 3, 4}.ToLinkedList();
            var loop3 = new[] {5, 6, 7, 8}.ToLinkedList();
            var linkedListLast3 = linkedList3.GetLastNode();
            linkedListLast3.Next = loop3;
            loop3.Next = linkedListLast3;
            yield return new TestCaseData(linkedList3, linkedListLast3);

            var linkedList4 = new[] {1}.ToLinkedList();
            var loop4 = new[] {2}.ToLinkedList();
            var linkedListLast4 = linkedList4.GetLastNode();
            linkedListLast4.Next = loop4;
            loop4.Next = linkedListLast4;
            yield return new TestCaseData(linkedList4, linkedListLast4);

            yield return new TestCaseData(null, null);
            yield return new TestCaseData(new[] {2}.ToLinkedList(), null);
        }
    }
}