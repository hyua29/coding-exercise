namespace LeetCode.LeetCode75;

public class MinimumNumberOfArrowsToBurstBalloons
{
    public int FindMinArrowShots(int[][] points)
    {
        if (points.Length == 0) return 0;

        Array.Sort(points, (ints, ints1) => Comparer<int>.Default.Compare(ints[1], ints1[1]));

        int last = points[0][1];
        int arrow = 1;
        for (int i = 0; i < points.Length; i++)
        {
            if (points[i][0] > last)
            {
                last = points[i][1];
                arrow++;
            }
        }

        return arrow;
    }
}

public class MinimumNumberOfArrowsToBurstBalloonsTests
{
    private MinimumNumberOfArrowsToBurstBalloons _minimumNumberOfArrowsToBurstBalloons =
        new MinimumNumberOfArrowsToBurstBalloons();

    [Test]
    public void FindMinArrowShots_Test1()
    {
        var input = new[] {new[] {-2147483646, -2147483645}, new[] {2147483646, 2147483647}};
        Assert.That(_minimumNumberOfArrowsToBurstBalloons.FindMinArrowShots(input), Is.EqualTo(2));
    }

    [Test]
    public void FindMinArrowShots_Test2()
    {
        var input = new[]
        {
            new[] {3, 9}, new[] {7, 12}, new[] {3, 8}, new[] {6, 8}, new[] {9, 10}, new[] {2, 9}, new[] {0, 9},
            new[] {3, 9}, new[] {0, 6}, new[] {2, 8}
        };
        Assert.That(_minimumNumberOfArrowsToBurstBalloons.FindMinArrowShots(input), Is.EqualTo(2));
    }
}