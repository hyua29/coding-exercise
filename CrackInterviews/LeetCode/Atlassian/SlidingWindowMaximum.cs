namespace LeetCode.Atlassian;

/// <summary>
/// https://leetcode.com/problems/sliding-window-maximum/
/// </summary>
public class SlidingWindowMaximum
{
    public int[] MaxSlidingWindow(int[] nums, int k)
    {
        if (k > nums.Length) throw new ArgumentException();

        var result = new int[nums.Length - k + 1];

        var currentMax = GetMax(nums, k, 0, k - 1, out var numMax);
        result[0] = currentMax;

        for (int i = k; i < nums.Length; i++)
        {
            var left = i - k + 1;
            if (nums[i] > currentMax)
            {
                currentMax = nums[i];
                numMax = 1;
            }
            else if (nums[i] == currentMax)
            {
                if (nums[left - 1] != currentMax) numMax++;
            }
            else if (nums[i] < currentMax)
            {
                if (nums[left - 1] == currentMax)
                {
                    if (numMax == 1)
                    {
                        currentMax = GetMax(nums, k, left, i, out numMax);
                    }
                    else
                    {
                        numMax--;
                    }
                }
            }

            result[left] = currentMax;
        }

        return result;
    }

    private static int GetMax(int[] nums, int k, int left, int right, out int numMax)
    {
        numMax = 0;
        var currentMax = int.MinValue;

        for (int i = left; i <= right; i++)
        {
            if (nums[i] > currentMax)
            {
                currentMax = nums[i];
            }
        }

        for (int i = left; i <= right; i++)
        {
            if (nums[i] == currentMax)
            {
                numMax++;
            }
        }

        return currentMax;
    }
}

public class SlidingWindowMaximumTests
{
    private SlidingWindowMaximum _slidingWindowMaximum = new SlidingWindowMaximum();

    [Test]
    public void SlidingWindowMaximum_Test()
    {
        CollectionAssert.AreEqual(_slidingWindowMaximum.MaxSlidingWindow(new[] {1, 3, -1, -3, 5, 3, 6, 7}, 3),
            new int[] {3, 3, 5, 5, 6, 7});
    }
}