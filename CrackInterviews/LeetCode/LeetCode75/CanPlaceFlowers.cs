namespace LeetCode.LeetCode75;

/// <summary>
/// https://leetcode.com/problems/can-place-flowers/solutions/3317839/python-simple-solution-easy-to-understand/?envType=study-plan-v2&envId=leetcode-75
/// </summary>
public class CanPlaceFlowersProblem
{
    public bool CanPlaceFlowers(int[] flowerbed, int n)
    {
        var currentOneIndex = -1;

        for (int i = 0; i < flowerbed.Length; i++)
        {
            if (flowerbed[i] == 1)
            {
                currentOneIndex = i;
                break;
            }
        }

        // There is no 1 in the sequence
        if (currentOneIndex == -1)
        {
            return Math.Ceiling((float) flowerbed.Length / 2) >= n;
        }
        else
        {
            n = n - (int) Math.Ceiling((float) (currentOneIndex - 1) / 2);
        }

        var index = currentOneIndex + 1;
        while (index < flowerbed.Length)
        {
            if (flowerbed[index] == 1)
            {
                var diff = index - currentOneIndex - 1;
                n = n - ((int) Math.Ceiling((float) diff / 2) - 1);
                currentOneIndex = index;
            }

            index++;
        }

        if (flowerbed[^1] != 1)
        {
            n = n - (int) Math.Ceiling((float) (flowerbed.Length - 2 - currentOneIndex) / 2);
        }


        return n <= 0;
    }
}

[TestFixture]
public class FlowerbedTests
{
    private CanPlaceFlowersProblem _solution = new CanPlaceFlowersProblem();

    [Test]
    public void TestCase1()
    {
        int[] flowerbed = {1, 0, 0, 0, 1};
        int n = 1;
        Assert.IsTrue(_solution.CanPlaceFlowers(flowerbed, n));
    }

    [Test]
    public void TestCase2()
    {
        int[] flowerbed = {1, 0, 0, 0, 1};
        int n = 2;
        Assert.IsFalse(_solution.CanPlaceFlowers(flowerbed, n));
    }

    [Test]
    public void TestCase3()
    {
        int[] flowerbed = {0, 0, 1, 0, 1};
        int n = 1;
        Assert.IsTrue(_solution.CanPlaceFlowers(flowerbed, n));
    }
    
    [Test]
    public void TestCase4()
    {
        int[] flowerbed = {0, 0, 0, 0, 0};
        int n = 3;
        Assert.IsTrue(_solution.CanPlaceFlowers(flowerbed, n));
    }
    
    [Test]
    public void TestCase6()
    {
        int[] flowerbed = {0, 0, 1, 0, 0};
        int n = 2;
        Assert.IsTrue(_solution.CanPlaceFlowers(flowerbed, n));
    }
    
    [Test]
    public void TestCase5()
    {
        int[] flowerbed = {1, 1, 1, 1, 1};
        int n = 1;
        Assert.IsFalse(_solution.CanPlaceFlowers(flowerbed, n));
    }

    [Test]
    public void TestCase7()
    {
        int[] flowerbed = {0,1,0};
        int n = 1;
        Assert.IsFalse(_solution.CanPlaceFlowers(flowerbed, n));
    }
    // Add more test cases as needed
}