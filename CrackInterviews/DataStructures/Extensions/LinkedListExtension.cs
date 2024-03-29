namespace DataStructures.Extensions;

using DataStructures.Models;

public static class LinkedListExtension
{
    public static SinglyLinkedListNode? ToLinkedList(this int[] array)
    {
        var dummyHead = new SinglyLinkedListNode();
        var current = dummyHead;
        foreach (var t in array)
        {
            current.Next = new SinglyLinkedListNode {Data = t};
            current = current.Next;
        }

        return dummyHead.Next;
    }

    public static List<SinglyLinkedListNode> ToList(this SinglyLinkedListNode singlyLinkedListNode)
    {
        var list = new List<SinglyLinkedListNode>();
        var current = singlyLinkedListNode;
        while (current != null)
        {
            list.Add(current);
            current = current.Next;
        }

        return list;
    }

    public static SinglyLinkedListNode GetLastNode(this SinglyLinkedListNode node)
    {
        var current = node;
        while (current.Next != null) current = current.Next;

        return current;
    }
}