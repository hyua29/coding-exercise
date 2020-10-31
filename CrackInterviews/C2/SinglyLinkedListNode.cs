namespace C2
{
    public class SinglyLinkedListNode
    {
        public int Id { get; }

        public int Data { get; set; }

        public SinglyLinkedListNode Next { get; set; }

        public SinglyLinkedListNode()
        {

        }

        public SinglyLinkedListNode(int id)
        {
            Id = id;
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}; {nameof(Data)}: {Data}; {nameof(Next)}: {Next.Data}";
        }
    }
}