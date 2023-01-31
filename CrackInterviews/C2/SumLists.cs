using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace C2
{
    [TestFixture]
    internal class SumLists
    {
        public static SinglyLinkedListNode Calculate1(SinglyLinkedListNode head1, SinglyLinkedListNode head2, int carry)
        {
            if (head1 == null && head2 == null && carry == 0)
            {
                return null;
            }

            var value = carry;

            if (head1 != null)
            {
                value += head1.Data;
            }

            if (head2 != null)
            {
                value += head2.Data;
            }

            int remainder = value % 10;
            int nextCarry = value >= 10 ? 1 : 0;

            var currentNode = new SinglyLinkedListNode
            {
                Data = remainder,
                Next = Calculate1(head1?.Next, head2?.Next, nextCarry)
            };

            return currentNode;
        }

        [TestCaseSource(nameof(GetTestData))]
        public void SumListsTest(SinglyLinkedListNode head1, SinglyLinkedListNode head2, string expectedResult)
        {
            var results = Calculate1(head1, head2, 0).ToList().Select(r => r.Data).Reverse();

            Assert.That(string.Join("", results), Is.EqualTo(expectedResult));
        }

        private static IEnumerable<TestCaseData> GetTestData()
        {
            yield return new TestCaseData(new[] { 1, 2, 3 }.ToLinkedList(), new[] { 1, 2, 3 }.ToLinkedList(), "642");
            yield return new TestCaseData(new[] { 1, 2, 3 }.ToLinkedList(), new[] { 1, 2, 3, 4 }.ToLinkedList(), "4642");
            yield return new TestCaseData(new[] { 1, 2, 3, 4 }.ToLinkedList(), new[] { 1, 2, 3 }.ToLinkedList(), "4642");
            yield return new TestCaseData(null, new[] { 1, 2, 3 }.ToLinkedList(), "321");
            yield return new TestCaseData(new[] { 1, 2, 3, 4 }.ToLinkedList(), null, "4321");
            yield return new TestCaseData(new[] { 6, 5, 6, 9 }.ToLinkedList(), new[] { 6, 4, 5 }.ToLinkedList(), "10202");
            yield return new TestCaseData(null, null, string.Empty);
        }
    }
}