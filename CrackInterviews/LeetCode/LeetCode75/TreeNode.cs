namespace LeetCode.LeetCode75;

public class TreeNode
{
    public int val;
    public TreeNode left;
    public TreeNode right;

    public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
    {
        var valueKeeper = new Dictionary<int, int>();
        foreach (var v in valueKeeper)
        {
            Console.WriteLine(v);
        }

        this.val = val;
        this.left = left;
        this.right = right;
    }
}