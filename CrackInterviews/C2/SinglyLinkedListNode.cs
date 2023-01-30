namespace C2
{
    using System;

    public class SinglyLinkedListNode
    {
        public SinglyLinkedListNode()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }

        public int Data { get; set; }

        public SinglyLinkedListNode Next { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}; {nameof(Data)}: {Data}; {nameof(Next)}: {Next?.Data}";
        }
    }
}