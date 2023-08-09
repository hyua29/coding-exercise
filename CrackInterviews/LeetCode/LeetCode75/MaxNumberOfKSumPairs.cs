namespace LeetCode.LeetCode75;

/// <summary>
/// https://leetcode.com/problems/max-number-of-k-sum-pairs/solutions/2005922/going-from-o-n-2-o-nlogn-o-n-meme/?envType=study-plan-v2&envId=leetcode-75
/// </summary>
public class MaxNumberOfKSumPairs
{
    public int MaxOperations(int[] nums, int k)
    {
        Array.Sort(nums);
        int count = 0;
        int i = 0;
        int j = nums.Length - 1;
        while (i < j)
        {
            int sum = nums[i] + nums[j];
            if (sum == k)
            {
                count++;
                i++;
                j--;
            }
            else if (sum > k) j--;
            else i++;
        }

        return count;
    }
}