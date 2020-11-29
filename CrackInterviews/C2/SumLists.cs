using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace C2
{
    [TestFixture]
    internal class SumLists
    {
        public static string Calculate1(SinglyLinkedListNode head1, SinglyLinkedListNode head2)
        {
            var current1 = head1;
            var current2 = head2;

            var result = new StringBuilder();
            var carry = 0;
            while (current1 != null || current2 != null)
            {
                var value1 = current1?.Data ?? 0;
                var value2 = current2?.Data ?? 0;

                var sum = value1 + value2 + carry;

                var remainder = 0;
                if (sum > 9)
                {
                    carry = 1;
                    remainder = sum - 10;
                }
                else
                {
                    carry = 0;
                    remainder = sum;
                }

                result.Insert(0, remainder.ToString());
                current1 = current1?.Next;
                current2 = current2?.Next;
            }

            if (carry == 1)
                result.Insert(0, "1");

            return result.ToString();
        }

        [TestCaseSource(nameof(GetTestData))]
        public void SumListsTest(SinglyLinkedListNode head1, SinglyLinkedListNode head2, string expectedResult)
        {
            Assert.That(Calculate1(head1, head2), Is.EqualTo(expectedResult));
        }

        private static IEnumerable<TestCaseData> GetTestData()
        {
            yield return new TestCaseData(new[] {1, 2, 3}.ToLinkedList(), new[] {1, 2, 3}.ToLinkedList(), "642");
            yield return new TestCaseData(new[] {1, 2, 3}.ToLinkedList(), new[] {1, 2, 3, 4}.ToLinkedList(), "4642");
            yield return new TestCaseData(new[] {1, 2, 3, 4}.ToLinkedList(), new[] {1, 2, 3}.ToLinkedList(), "4642");
            yield return new TestCaseData(null, new[] {1, 2, 3}.ToLinkedList(), "321");
            yield return new TestCaseData(new[] {1, 2, 3, 4}.ToLinkedList(), null, "4321");
            yield return new TestCaseData(null, null, string.Empty);
        }
    }
}