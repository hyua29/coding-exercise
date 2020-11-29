namespace C4
{
    public class MinimalTree
    {
        public static BinaryTreeNode<int> GetMinimalTree(int[] sortedArray)
        {
            return AttachChildren(sortedArray, 0, sortedArray.Length - 1);
        }

        private static BinaryTreeNode<int> AttachChildren(int[] sortedArray, int left, int right)
        {
            if (left > right) return null;

            var mid = (left + right) / 2;
            var midNode = new BinaryTreeNode<int>(sortedArray[mid]);

            midNode.LeftNode = AttachChildren(sortedArray, left, mid - 1);
            midNode.RightNode = AttachChildren(sortedArray, mid + 1, right);

            return midNode;
        }
    }
}