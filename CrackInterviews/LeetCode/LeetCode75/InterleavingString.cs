namespace LeetCode.LeetCode75;

/// <summary>
/// https://leetcode.com/problems/interleaving-string/
/// </summary>
public class InterleavingString
{
    public bool IsInterleave(string s1, string s2, string s3)
    {
        int m = s1.Length;
        int n = s2.Length;
        int l = s3.Length;

        if (m + n != l)
        {
            return false;
        }

        // dp[i,j] == true means first i + j chars are matched by first j chars from s1 and first i chars from s2
        bool[,] dp = new bool[m + 1, n + 1];
        dp[0, 0] = true;

        for (int i = 1; i <= m; i++)
        {
            dp[i, 0] = dp[i - 1, 0] && s1[i - 1] == s3[i - 1];
        }

        for (int j = 1; j <= n; j++)
        {
            dp[0, j] = dp[0, j - 1] && s2[j - 1] == s3[j - 1];
        }

        for (int i = 1; i <= m; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                dp[i, j] = (dp[i - 1, j] && s1[i - 1] == s3[i + j - 1]) || (dp[i, j - 1] && s2[j - 1] == s3[i + j - 1]);
            }
        }

        return dp[m, n];
    }
}