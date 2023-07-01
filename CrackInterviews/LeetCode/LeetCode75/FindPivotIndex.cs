namespace LeetCode.LeetCode75;

/// <summary>
/// https://leetcode.com/problems/find-pivot-index/description/?envType=study-plan-v2&envId=leetcode-75
/// </summary>
public class FindPivotIndex
{
    public int PivotIndex(int[] nums)
    {
        int totalSum = 0;
        foreach (int num in nums)
        {
            totalSum += num;
        }

        int leftSum = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (leftSum == totalSum - leftSum - nums[i])
            {
                return i;
            }

            leftSum += nums[i];
        }

        return -1;
    }
}

[TestFixture]
public class PivotIndexTests
{
    private FindPivotIndex _solution = new FindPivotIndex();

    [Test]
    public void TestPivotIndex_WithSingleElementArray_ShouldReturnZero()
    {
        int[] nums = new int[] {0};
        int expected = 0;

        int result = _solution.PivotIndex(nums);

        Assert.AreEqual(expected, result);
    }
    //
    // [Test]
    // public void TestPivotIndex_WithPositiveAndNegativeNumbers_ShouldReturnCorrectIndex()
    // {
    //     int[] nums = new int[] { -1, 3, 0, -8, 2, -4 };
    //     int expected = 2;
    //
    //     int result = FindPivotIndex(nums);
    //
    //     Assert.AreEqual(expected, result);
    // }

    [Test]
    public void TestPivotIndex_WithAllPositiveNumbers_ShouldReturnMinusOne()
    {
        int[] nums = new int[] {1, 2, 3, 4, 5, 6};
        int expected = -1;

        int result = _solution.PivotIndex(nums);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestPivotIndex_WithAllNegativeNumbers_ShouldReturnMinusOne()
    {
        int[] nums = new int[] {-1, -2, -3, -4, -5, -6};
        int expected = -1;
        var result = _solution.PivotIndex(nums);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestPivotIndex_WithRepeatedElements_ShouldReturnCorrectIndex()
    {
        int[] nums = new int[] {1, 7, 3, 6, 5, 6};
        int expected = 3;

        int result = _solution.PivotIndex(nums);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestPivotIndex_WithEmptyArray_ShouldReturnMinusOne()
    {
        int[] nums = new int[] { };
        int expected = -1;

        int result = _solution.PivotIndex(nums);

        Assert.AreEqual(expected, result);
    }
}