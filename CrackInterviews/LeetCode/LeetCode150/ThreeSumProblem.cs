namespace LeetCode.LeetCode150;

/// <summary>
/// https://leetcode.com/problems/3sum/?envType=study-plan-v2&amp;envId=top-interview-150
/// </summary>
public class ThreeSumProblem
{
    public IList<IList<int>> ThreeSum(int[] nums)
    {
        var results = new List<IList<int>>();

        if (nums.Length < 3) throw new ArgumentException();

        Array.Sort(nums);

        var previousFirst = nums[0] - 1;
        for (var i = 1; i < nums.Length; i++)
        {
            if (nums[i - 1] == previousFirst)
            {
                continue;
            }

            previousFirst = nums[i - 1];

            var left = i;
            var right = nums.Length - 1;
            while (left < right)
            {
                var previousLeft = nums[left];
                var previousRight = nums[right];

                var sum = nums[i - 1] + nums[left] + nums[right];

                if (sum == 0)
                {
                    results.Add(new List<int> {nums[i - 1], nums[left], nums[right]});

                    FindNextLeft(nums, previousLeft, ref left);

                    FindNextRight(nums, previousRight, ref right);
                }
                else if (sum < 0)
                {
                    FindNextLeft(nums, previousLeft, ref left);
                }
                else
                {
                    FindNextRight(nums, previousRight, ref right);
                }
            }
        }

        return results;
    }

    private static int FindNextRight(int[] nums, int previousRight, ref int currentRight)
    {
        do
        {
            currentRight--;
        } while (currentRight >=0 && nums[currentRight] == previousRight);

        return currentRight;
    }

    private static int FindNextLeft(int[] nums, int previousLeft, ref int currentLeft)
    {
        do
        {
            currentLeft++;
        } while (currentLeft < nums.Length && nums[currentLeft] == previousLeft);

        return currentLeft;
    }
}