namespace C4
{
    public class BinaryTreeNode<T>
    {
        public T Data { get; set; }
        
        public BinaryTreeNode<T> LeftNode { get; set; }
        
        public BinaryTreeNode<T> RightNode { get; set; }

        public BinaryTreeNode(T data)
        {
            this.Data = data;
        }
    }
}