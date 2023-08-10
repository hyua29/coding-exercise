namespace LeetCode.LeetCode75;

/// <summary>
/// https://leetcode.com/problems/maximum-level-sum-of-a-binary-tree/
/// </summary>
public class MaximumLevelSumOfABinaryTree
{
    public int MaxLevelSum(TreeNode root)
    {
        if (root == null) return 0;

        var valueKeeper = new Dictionary<int, int>();
        var queue = new Queue<(int Level, TreeNode Node)>();
        queue.Enqueue((1, root));

        while (queue.TryDequeue(out var currentNode))
        {
            if (!valueKeeper.ContainsKey(currentNode.Level))
            {
                valueKeeper[currentNode.Level] = currentNode.Node.val;
            }
            else
            {
                valueKeeper[currentNode.Level] += currentNode.Node.val;
            }

            if (currentNode.Node.left != null)
            {
                queue.Enqueue((currentNode.Level + 1, currentNode.Node.left));
            }

            if (currentNode.Node.right != null)
            {
                queue.Enqueue((currentNode.Level + 1, currentNode.Node.right));
            }
        }

        return valueKeeper.MaxBy(p => p.Value).Key;
    }
}