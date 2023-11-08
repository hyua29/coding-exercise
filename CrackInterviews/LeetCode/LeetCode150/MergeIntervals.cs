namespace LeetCode.LeetCode150;

public class MergeIntervals
{
    public int[][] Merge(int[][] intervals)
    {
        var results = new List<int[]>();
        Array.Sort(intervals, (i1, i2) => i1[0] - i2[0]);

        var leftBoundary = intervals[0][0];
        var rightBoundary = intervals[0][1];
        for (var i = 0; i < intervals.Length - 1; i++)
        {
            if (rightBoundary >= intervals[i + 1][0])
            {
                rightBoundary = Math.Max(rightBoundary, intervals[i + 1][1]);
            }
            else
            {
                results.Add(new[] {leftBoundary, rightBoundary});
                leftBoundary = intervals[i + 1][0];
                rightBoundary = intervals[i + 1][1];
            }
        }

        results.Add(new[] {leftBoundary, rightBoundary});
        return results.ToArray();
    }
}