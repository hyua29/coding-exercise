using System.Reflection;
using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;
using System.Text;

namespace C2
{
    [TestFixture]
    class RemoveDups
    {
        public static SinglyLinkedListNode Calculate1(SinglyLinkedListNode head)
        {
            if (head == null)
                return null;

            var current = head;
            ISet<int> set = new HashSet<int>();
            var previous = current;
            while (current != null)
            {
                if (set.Contains(current.Data))
                {
                    previous.Next = current.Next;
                }
                else
                {
                    set.Add(current.Data);
                    previous = current;
                }
                current = current.Next;
            }

            return head;
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

        [TestCaseSource(nameof(RemoveDups.GetTestData))]
        public void RemoveDupsTest(SinglyLinkedListNode head)
        {
            var result1 = RemoveDups.Calculate1(head);
            var result2 = RemoveDups.Calculate1(head);

            this.CheckResult(result1);
            this.CheckResult(result2);
        }

        private void CheckResult(SinglyLinkedListNode head)
        {
            if (head == null)
                Assert.IsNull(head);
            var current = head;
            ISet<int> set = new HashSet<int>();
            while (current != null)
            {
                Assert.False(set.Contains(current.Data));
                set.Add(current.Data);
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