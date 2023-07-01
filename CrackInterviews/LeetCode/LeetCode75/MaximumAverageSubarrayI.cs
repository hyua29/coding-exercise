namespace LeetCode.LeetCode75;

/// <summary>
/// https://leetcode.com/problems/maximum-average-subarray-i/?envType=study-plan-v2&envId=leetcode-75
/// </summary>
public class MaximumAverageSubarrayI
{
    public double FindMaxAverage(int[] nums, int k)
    {
        int max = 0;
        for (int i = 0; i < k; i++)
            max += nums[i];
        int temp = max;
        for (int i = k; i < nums.Length; i++)
        {
            temp = temp + nums[i] - nums[i - k];
            max = Math.Max(temp, max);
        }

        return (max * 1.0) / k;
    }
}