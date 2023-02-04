namespace DataStructures.Models
{
    using System.Diagnostics;

    public class SinglyLinkedListNode
    {
        public SinglyLinkedListNode()
        {
            Id = Guid.NewGuid();
        }

        public SinglyLinkedListNode(SinglyLinkedListNode node, bool copyNext = false)
        {
            Debug.Assert(node != null);

            Id = Guid.NewGuid();
            Data = node.Data;
            Next = copyNext ? node.Next : null;
        }

        public Guid Id { get; }

        public int Data { get; set; }

        public SinglyLinkedListNode? Next { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}; {nameof(Data)}: {Data}; {nameof(Next)}: {Next?.Data}";
        }
    }
}