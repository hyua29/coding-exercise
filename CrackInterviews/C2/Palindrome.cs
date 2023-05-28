namespace C2;

using System.Collections.Generic;
using DataStructures.Extensions;
using DataStructures.Models;
using NUnit.Framework;

public class Palindrome
{
    public static bool Calculate1(SinglyLinkedListNode head)
    {
        if (head == null) return false;

        SinglyLinkedListNode parent = null;

        var current = head;

        do
        {
            var tempParent = parent;
            parent = new SinglyLinkedListNode(current)
            {
                Next = tempParent
            };

            current = current.Next;
        } while (current != null);

        var c1 = head;
        var c2 = parent;

        while (c2 != null || c1 != null)
        {
            if (c1.Data != c2!.Data)
                return false;

            c1 = c1.Next;
            c2 = c2.Next;
        }

        return true;
    }

    [TestCaseSource(nameof(GetTestData))]
    public void PalindromeTest(SinglyLinkedListNode head, bool expectedResult)
    {
        var results = Calculate1(head);

        Assert.That(results, Is.EqualTo(expectedResult));
    }

    private static IEnumerable<TestCaseData> GetTestData()
    {
        yield return new TestCaseData(new[] {1, 2, 3}.ToLinkedList(), false);
        yield return new TestCaseData(new[] {1, 2, 3, 3, 2, 1}.ToLinkedList(), true);
        yield return new TestCaseData(new[] {1, 2, 3, 4, 3, 2, 1}.ToLinkedList(), true);
        yield return new TestCaseData(new[] {1}.ToLinkedList(), true);
        yield return new TestCaseData(null, false);
    }
}