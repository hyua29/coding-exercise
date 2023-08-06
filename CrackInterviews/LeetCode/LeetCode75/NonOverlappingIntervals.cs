namespace LeetCode.LeetCode75;

public class NonOverlappingIntervals
{
    public int EraseOverlapIntervals(int[][] intervals)
    {
        Array.Sort(intervals, (x, y) => x[1] - y[1]);

        var toRemove = intervals.Length - 1;

        var end = intervals[0][1];

        for (int i = 1; i < intervals.Length; i++)
        {
            if (intervals[i][0] >= end)
            {
                toRemove--;
                end = intervals[i][1];
            }

        }

        return toRemove;
    }
}

[TestFixture]
public class NonOverlappingIntervalsTests
{
    private readonly NonOverlappingIntervals _solution = new NonOverlappingIntervals();

    [Test]
    public void Test1()
    {
        int[][] intervals = new int[][]
        {
            new int[] {1, 2},
            new int[] {2, 3},
            new int[] {3, 4},
            new int[] {1, 3}
        };
        Assert.That(_solution.EraseOverlapIntervals(intervals), Is.EqualTo(1));
    }

    [Test]
    public void Test2()
    {
        int[][] intervals = new int[][]
        {
            new int[] {1, 2},
            new int[] {2, 3},
            new int[] {3, 4},
            new int[] {1, 3},
            new int[] {4, 5}
        };
        Assert.That(_solution.EraseOverlapIntervals(intervals), Is.EqualTo(1));
    }

    [Test]
    public void Test3()
    {
        int[][] intervals = new int[][]
        {
            new int[] {1, 2},
            new int[] {2, 3},
            new int[] {3, 4},
            new int[] {4, 5}
        };
        Assert.That(_solution.EraseOverlapIntervals(intervals), Is.EqualTo(0));
    }

    [Test]
    public void Test4()
    {
        int[][] intervals = new int[][]
        {
            new int[] {1, 2},
            new int[] {2, 3},
            new int[] {3, 4},
            new int[] {1, 5}
        };
        Assert.That(_solution.EraseOverlapIntervals(intervals), Is.EqualTo(1));
    }


    [Test]
    public void Test5()
    {
        // [[-52,31],[-73,-26],[82,97],[-65,-11],[-62,-49],[95,99],[58,95],[-31,49],[66,98],[-63,2],[30,47],[-40,-26]]
        int[][] intervals = new int[][]
        {
            new int[] {-52, 31}, new int[] {-73, -26}, new int[] {82, 97}, new int[] {-65, -11}, new int[] {-62, -49},
            new int[] {95, 99}, new int[] {58, 95}, new int[] {-31, 49}, new int[] {66, 98}, new int[] {-63, 2},
            new int[] {30, 47}, new int[] {-40, -26}
        };
        Assert.That(_solution.EraseOverlapIntervals(intervals), Is.EqualTo(7));
    }
}