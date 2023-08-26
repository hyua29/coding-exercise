namespace LeetCode.Atlassian;

public class SlidingWindowMaximum
{
    public int[] MaxSlidingWindow(int[] nums, int k)
    {
        if (k > nums.Length) throw new ArgumentException();

        var result = new int[nums.Length - k + 1];

        for (int i = k - 1; i < nums.Length; i++)
        {
            var max = int.MinValue;
            for (int j = i - k + 1; j <= i; j++)
            {
                max = Math.Max(max, nums[j]);
            }

            result[i - k + 1] = max;
        }

        return result;
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