namespace LeetCode.LeetCode150;

public class JumpGame
{
    public bool CanJump(int[] nums)
    {
        if (nums.Length == 1) return true;

        var index = 0;
        while (index < nums.Length)
        {
            var current = nums[index];

            if (current == 0)
            {
                break;
            }

            var bestPosition = -1;
            var mValue = 0;
            for (int i = 1; i <= current; i++)
            {
                if (index + i >= nums.Length - 1)
                {
                    bestPosition = index + i;
                    break;
                }

                var v = nums[index + i] + i;
                if (v >= mValue)
                {
                    mValue = v;
                    bestPosition = index + i;
                }
            }

            index = bestPosition;
        }

        return index >= nums.Length - 1;
    }
}

public class JumpGameTests
{
    [Test]
    public void Test()
    {
        var sol = new JumpGame();
        var input = new[] {2, 5, 0, 0};

        Assert.IsTrue(sol.CanJump(input));
    }
}