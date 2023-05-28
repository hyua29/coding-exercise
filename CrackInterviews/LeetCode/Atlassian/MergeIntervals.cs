namespace LeetCode.Atlassian;

public class MergeIntervals
{
    public int[][] Merge(int[][] intervals)
    {
        Array.Sort(intervals, (a, b) =>
        {
            var order = a[0] - b[0];
            if (order == 0) order = a[1] - b[1];

            return order;
        });

        var results = new List<int[]>();

        for (var i = 0; i < intervals.Length; i++)
        {
            // we can reuse the existing interval to save memory but it's probably not worth it
            // var temp = intervals[i];
            var temp = new[] {intervals[i][0], intervals[i][1]};

            var nextIndex = i + 1;
            while (nextIndex < intervals.Length)
            {
                var next = intervals[nextIndex];

                if (temp[1] >= next[0])
                {
                    temp[1] = Math.Max(next[1], temp[1]);
                    i++;
                    nextIndex = i + 1;
                }
                else
                {
                    break;
                }
            }

            results.Add(temp);
        }

        return results.ToArray();
    }
}

[TestFixture]
public class MergeIntervalsTests
{
    [Test]
    public void Merge_EmptyIntervals_ReturnsEmpty()
    {
        // Arrange
        var mergeIntervals = new MergeIntervals();
        var intervals = Array.Empty<int[]>();

        // Act
        var result = mergeIntervals.Merge(intervals);

        // Assert
        Assert.IsEmpty(result);
    }

    [Test]
    public void Merge_SingleInterval_ReturnsSameInterval()
    {
        // Arrange
        var mergeIntervals = new MergeIntervals();
        int[][] intervals = {new[] {1, 3}};

        // Act
        var result = mergeIntervals.Merge(intervals);

        // Assert
        Assert.That(result, Is.EqualTo(intervals));
    }

    [Test]
    public void Merge_NonOverlappingIntervals_ReturnsSameIntervals()
    {
        // Arrange
        var mergeIntervals = new MergeIntervals();
        int[][] intervals = {new[] {1, 3}, new[] {4, 6}, new[] {7, 9}};

        // Act
        var result = mergeIntervals.Merge(intervals);

        // Assert
        Assert.That(result, Is.EqualTo(intervals));
    }

    [Test]
    public void Merge_OverlappingIntervals_ReturnsMergedInterval()
    {
        // Arrange
        var mergeIntervals = new MergeIntervals();
        int[][] intervals = {new[] {1, 4}, new[] {2, 5}, new[] {7, 9}};
        int[][] expected = {new[] {1, 5}, new[] {7, 9}};

        // Act
        var result = mergeIntervals.Merge(intervals);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Merge_MultipleOverlappingIntervals_ReturnsMergedInterval()
    {
        // Arrange
        var mergeIntervals = new MergeIntervals();
        int[][] intervals = {new[] {8, 10}, new[] {2, 6}, new[] {1, 3}, new[] {15, 18}};
        int[][] expected = {new[] {1, 6}, new[] {8, 10}, new[] {15, 18}};

        // Act
        var result = mergeIntervals.Merge(intervals);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Merge_OneBigInterval_ReturnsSingleInterval()
    {
        // Arrange
        var mergeIntervals = new MergeIntervals();
        int[][] intervals = {new[] {1, 20}, new[] {6, 7}, new[] {13, 4}, new[] {15, 21}};
        int[][] expected = {new[] {1, 21}};

        // Act
        var result = mergeIntervals.Merge(intervals);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }
}