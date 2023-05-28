namespace LeetCode.Atlassian;

/// <summary>
/// https://leetcode.com/problems/single-number/
/// https://leetcode.com/problems/single-number/solutions/1771771/think-it-through-time-o-n-space-o-1-python-explained/
/// </summary>
public class SingleNumberSolution
{
    public int SingleNumber(int[] nums)
    {
        var results = 0;
        foreach (var n in nums)
        {
            results ^= n;
        }

        return results;
    }
}