namespace LeetCode.Atlassian;

/// <summary>
/// https://leetcode.com/problems/longest-increasing-subsequence/
/// https://leetcode.com/problems/longest-increasing-subsequence/submissions/
/// </summary>
public class LongestIncreasingSubsequence
{
    public int LengthOfLIS(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return 0;

        // This will be our array to track longest sequence length
        int[] dp = new int[nums.Length];
        Array.Fill(dp, 1);

        int result = 1;
        for (int i = 1; i < nums.Length; i++)
        {
            for (int j = 0; j < i; j++)
            {
                // It means next number contributes to increasing sequence.
                if (nums[i] > nums[j])
                {
                    // But increase the value only if it results in a larger value of the sequence than T[i]
                    // It is possible that T[i] already has larger value from some previous j'th iteration
                    dp[i] = Math.Max(dp[j] + 1, dp[i]);
                }
            }

            result = Math.Max(result, dp[i]);
        }

        return result;
    }
}