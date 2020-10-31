using System.Collections.Generic;
using System.Linq;

namespace C2
{
    public static class LinkedListExtenstion
    {
        public static SinglyLinkedListNode ToLinkedList(this int[] array)
        {
            if (array == null)
            {
                return null;
            }

            var dummyHead = new SinglyLinkedListNode();
            var current = dummyHead;
            for (int i = 0; i < array.Count(); i++)
            {
                current.Next = new SinglyLinkedListNode(i) {Data = array[i]};
                current = current.Next;
            }

            return dummyHead.Next;
        }

        public static List<SinglyLinkedListNode> ToList(this SinglyLinkedListNode singlyLinkedListNode)
        {
            var list = new List<SinglyLinkedListNode>();
            while (singlyLinkedListNode != null)
            {
                list.Add(singlyLinkedListNode);
                singlyLinkedListNode = singlyLinkedListNode.Next;
            }

            return list;
        }

        public static SinglyLinkedListNode GetLastNode(this SinglyLinkedListNode node)
        {
            if (node == null)
            {
                return null;
            }

            var current = node;
            while (current.Next != null)
            {
                current = current.Next;
            }

            return current;
        }
    }
}