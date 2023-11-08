namespace LeetCode.LeetCode150;

/// <summary>
/// https://leetcode.com/problems/two-sum-ii-input-array-is-sorted/description/?envType=study-plan-v2&amp;envId=top-interview-150
/// </summary>
public class TwoSum2
{
    public int[] TwoSum(int[] numbers, int target)
    {
        var left = 0;
        var right = numbers.Length - 1;

        do
        {
            var sum = numbers[left] + numbers[right];

            if (sum > target)
            {
                right--;
            }
            else if (sum < target)
            {
                left++;
            }
            else
            {
                break;
            }
        } while (true);

        return new[] {left + 1, right + 1};
    }
}