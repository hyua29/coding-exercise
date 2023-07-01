namespace LeetCode.LeetCode75;

public class LongestSubarrayOf1AfterDeletingOneElement
{
    public int LongestSubarray(int[] nums)
    {
        return new MaxConsecutiveOnesIII().LongestOnes(nums, 1) - 1;
    }
}