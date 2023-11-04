namespace LeetCode.LeetCode150;

/// <summary>
/// https://leetcode.com/problems/minimum-size-subarray-sum/?envType=study-plan-v2&amp;envId=top-interview-150
/// </summary>
public class MinimumSizeSubarraySum
{
    public int MinSubArrayLen(int target, int[] nums)
    {
        var slidingWindow = new Queue<int>();

        var minLength = int.MaxValue;
        var currentSum = 0;
        foreach (var n in nums)
        {
            currentSum += n;
            slidingWindow.Enqueue(n);

            while (currentSum >= target)
            {
                minLength = Math.Min(minLength, slidingWindow.Count);
                currentSum -= slidingWindow.Dequeue();
            }
        }

        return minLength > nums.Length ? 0 : minLength;
    }
}