namespace C4
{
    public class BinaryTreeNode<T>
    {
        public BinaryTreeNode(T data)
        {
            Data = data;
        }

        public T Data { get; set; }

        public BinaryTreeNode<T> LeftNode { get; set; }

        public BinaryTreeNode<T> RightNode { get; set; }
    }
}