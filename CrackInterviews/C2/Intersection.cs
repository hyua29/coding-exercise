using DataStructures.Models;

namespace C2
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;

    public class Intersection
    {
        public static SinglyLinkedListNode Calculate1(SinglyLinkedListNode head1, SinglyLinkedListNode head2)
        {
            if (head1 == null || head2 == null)
            {
                return null;
            }

            var last1 = GetLastNode(head1, 1);
            var last2 = GetLastNode(head2, 1);

            // These two linked lists don't converge on the same node, meaning that there is no intersection
            if (last1.LastNode != last2.LastNode)
            {
                return null;
            }

            var lengthDiff = last1.Length - last2.Length;

            var longerHead = lengthDiff >= 0 ? head1 : head2;
            var shorterHead = longerHead == head1 ? head2 : head1;

            var i = Math.Abs(lengthDiff);
            // Adjust both link lists such that they are of the same length
            while (i > 0)
            {
                longerHead = longerHead.Next;
                i--;
            }

            var c1 = longerHead;
            var c2 = shorterHead;

            // Look for the intersection
            while (c1 != c2)
            {
                c1 = c1.Next;
                c2 = c2.Next;
            }

            return c1;
        }

        private static (SinglyLinkedListNode LastNode, int Length) GetLastNode(SinglyLinkedListNode head,
            int currentLength)
        {
            if (head.Next == null)
            {
                return (head, currentLength);
            }

            return GetLastNode(head.Next, currentLength + 1);
        }

        [TestCaseSource(nameof(GetTestData))]
        public void IntersectionTest(SinglyLinkedListNode head1, SinglyLinkedListNode head2,
            SinglyLinkedListNode intersectedNode)
        {
            var results = Calculate1(head1, head2);

            Assert.That(results, Is.EqualTo(intersectedNode));
        }

        private static IEnumerable<TestCaseData> GetTestData()
        {
            var intersectedNode = new SinglyLinkedListNode();

            yield return new TestCaseData(
                new SinglyLinkedListNode {Next = intersectedNode},
                new SinglyLinkedListNode {Next = new SinglyLinkedListNode {Next = intersectedNode}}, intersectedNode);

            intersectedNode = new SinglyLinkedListNode();
            intersectedNode.Next = new SinglyLinkedListNode
                {Next = new SinglyLinkedListNode {Next = new SinglyLinkedListNode()}};
            yield return new TestCaseData(
                intersectedNode,
                new SinglyLinkedListNode {Next = new SinglyLinkedListNode {Next = intersectedNode}}, intersectedNode);

            intersectedNode = new SinglyLinkedListNode();
            intersectedNode.Next = new SinglyLinkedListNode
                {Next = new SinglyLinkedListNode {Next = new SinglyLinkedListNode()}};
            yield return new TestCaseData(
                new SinglyLinkedListNode {Next = intersectedNode},
                new SinglyLinkedListNode {Next = new SinglyLinkedListNode {Next = intersectedNode}}, intersectedNode);
            yield return new TestCaseData(null, null, null);

            intersectedNode = new SinglyLinkedListNode();
            yield return new TestCaseData(
                new SinglyLinkedListNode {Next = new SinglyLinkedListNode {Next = intersectedNode}}, null, null);
            yield return new TestCaseData(null,
                new SinglyLinkedListNode {Next = new SinglyLinkedListNode {Next = intersectedNode}}, null);
        }
    }
}