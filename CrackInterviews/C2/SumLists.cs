using System.Reflection;
using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;
using System.Text;

namespace C2
{
    [TestFixture]
    class SumLists
    {
        public static string Calculate1(SinglyLinkedListNode head1, SinglyLinkedListNode head2)
        {
            var current1 = head1;
            var current2 = head2;

            while (current1 != null || current2 != null)
            {

            }

            return "";
        }

        public static SinglyLinkedListNode Calculate2(SinglyLinkedListNode head)
        {
            if (head == null)
                return null;

            var current = head;
            while (current != null)
            {
                var runner = current;
                while (runner != null && runner.Next != null)
                {
                    if (runner.Next.Data == current.Data)
                    {
                        runner.Next = runner.Next.Next;
                    }
                    runner = runner.Next;
                }
            }

            return head;
        }

        [TestCaseSource(nameof(SumLists.GetTestData))]
        public void SumListsTest(SinglyLinkedListNode head)
        {
            var result = SumLists.Calculate1(head);

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