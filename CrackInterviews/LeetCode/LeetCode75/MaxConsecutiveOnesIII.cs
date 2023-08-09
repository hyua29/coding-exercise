namespace LeetCode.LeetCode75;

/// <summary>
/// https://leetcode.com/problems/max-consecutive-ones-iii/solutions/719833/python3-sliding-window-with-clear-example-explains-why-the-soln-works/?envType=study-plan-v2&envId=leetcode-75
/// </summary>
public class MaxConsecutiveOnesIII
{
    public int LongestOnes(int[] nums, int k)
    {
        int i = 0, j;
        for (j = 0; j < nums.Length; ++j)
        {
            if (nums[j] == 0) k--;
            if (k < 0 && nums[i++] == 0) k++;
        }

        return j - i;
    }
}

[TestFixture]
public class MaxConsecutiveOnesIIITests
{
    private MaxConsecutiveOnesIII _solution = new MaxConsecutiveOnesIII();

    [Test]
    public void Test1()
    {
        // Test Case 1
        int[] A1 = {1, 1, 1, 1, 0};
        int K1 = 2;
        int expected1 = 5;
        int result1 = _solution.LongestOnes(A1, K1);
        Assert.That(result1, Is.EqualTo(expected1));
    }

    [Test]
    public void Test2()
    {
        // Test Case 2
        int[] A2 = {0, 0, 0, 0, 0};
        int K2 = 2;
        int expected2 = 2;
        int result2 = _solution.LongestOnes(A2, K2);
        Assert.That(result2, Is.EqualTo(expected2));
    }

    [Test]
    public void Test3()
    {
        // Test Case 3
        int[] A3 = {1, 1, 1, 0, 0, 1, 1, 0, 1};
        int K3 = 1;
        int expected3 = 4;
        int result3 = _solution.LongestOnes(A3, K3);
        Assert.That(result3, Is.EqualTo(expected3));
    }

    [Test]
    public void Test4()
    {
        // Test Case 4
        int[] A4 = {1, 0, 0, 0, 0, 1, 1, 1, 1};
        int K4 = 3;
        int expected4 = 7;
        int result4 = _solution.LongestOnes(A4, K4);
        Assert.That(result4, Is.EqualTo(expected4));
    }

    [Test]
    public void Test5()
    {
        // Test Case 5
        int[] A5 = {0, 0, 1, 1, 0, 0, 1, 1, 1, 0, 1, 1, 0, 0, 0, 1, 1, 1, 1};
        int K5 = 3;
        int expected5 = 10;
        int result5 = _solution.LongestOnes(A5, K5);
        Assert.That(result5, Is.EqualTo(expected5));
    }

    [Test]
    public void Test6()
    {
        // Test Case 6
        int[] A6 = {1, 1, 1, 0, 0, 0, 1, 1, 1, 1};
        int K6 = 0;
        int expected6 = 4;
        int result6 = _solution.LongestOnes(A6, K6);
        Assert.That(result6, Is.EqualTo(expected6));
    }
}