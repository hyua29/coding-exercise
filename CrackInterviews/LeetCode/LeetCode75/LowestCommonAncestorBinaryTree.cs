using System.Collections;

namespace LeetCode.LeetCode75;

/// <summary>
/// https://leetcode.com/problems/lowest-common-ancestor-of-a-binary-tree/?envType=study-plan-v2&envId=leetcode-75
/// https://leetcode.com/problems/lowest-common-ancestor-of-a-binary-tree/solutions/65226/my-java-solution-which-is-easy-to-understand/?envType=study-plan-v2&envId=leetcode-75
/// </summary>
public class LowestCommonAncestorBinaryTree
{
    public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
    {
        var node = Aux(root, p, q);

        if (root == p)
        {
            return NodeFound(p, q) ? p : null;
        }
        else if (root == q)
        {
            return NodeFound(q, p) ? q : null;
        }

        return node;
    }

    private bool NodeFound(TreeNode current, TreeNode nodeToFind)
    {
        var queue = new Queue<TreeNode>();
        queue.Enqueue(current);

        while (queue.TryDequeue(out var n))
        {
            if (n == nodeToFind)
                return true;

            if (n.left !=null)
                queue.Enqueue(n.left);
            
            if (n.right !=null)
                queue.Enqueue(n.right);
        }

        return false;
    }

    private TreeNode? Aux(TreeNode? root, TreeNode p, TreeNode q)
    {
        if (root == null)
        {
            return null;
        }

        if (root == p || root == q) return root;

        var left = Aux(root.left, p, q);
        var right = Aux(root.right, p, q);

        if (left != null && right != null)
        {
            return root;
        }

        return left ?? right;
    }
}