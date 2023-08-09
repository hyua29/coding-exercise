// namespace LeetCode.LeetCode75;
//
// /**
//  * https://leetcode.com/problems/binary-tree-right-side-view/submissions/?envType=study-plan-v2&envId=leetcode-75
//  * Definition for a binary tree node.
//  * public class TreeNode {
//  *     public int val;
//  *     public TreeNode left;
//  *     public TreeNode right;
//  *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
//  *         this.val = val;
//  *         this.left = left;
//  *         this.right = right;
//  *     }
//  * }
//  */
// public class Solution {
//     public IList<int> RightSideView(TreeNode root) {
//         var results = new List<int>();
//         if (root == null)
//         {
//             return results;
//         }
//
//         Inspect(root, 0, results);
//
//         return results;
//     }
//
//     private void Inspect(TreeNode node, int parentHeight, IList<int> results)
//     {
//         if (node == null)
//         {
//             return;
//         }
//
//         if (parentHeight + 1 > results.Count)
//         {
//             results.Add(node.val);
//         }
//
//         Inspect(node.right, parentHeight + 1, results);
//         Inspect(node.left, parentHeight + 1, results);
//     }
// }