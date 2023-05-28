namespace LeetCode.Atlassian;

using System.Diagnostics;

/// <summary>
///     See this for details:
///     https://leetcode.com/problems/unique-paths-ii/solutions/1180249/easy-solutions-w-explanation-comments-optimization-from-brute-force-approach/?orderBy=most_votes
/// </summary>
public class UniquePathsII
{
    public int UniquePathsWithObstacles(int[][] obstacleGrid)
    {
        Debug.Assert(obstacleGrid?.Length > 0);

        var buffer = new int[obstacleGrid.Length][];
        for (var i = 0; i < buffer.Length; i++) buffer[i] = new int[obstacleGrid[0].Length];

        if (obstacleGrid[obstacleGrid.Length - 1][obstacleGrid[0].Length - 1] == 1 || obstacleGrid[0][0] == 1) return 0;

        return ExploreOtherRoutes((0, 0), obstacleGrid, buffer);
    }

    private int ExploreOtherRoutes((int X, int Y) currentPosition, int[][] obstacleGrid, int[][] buffer)
    {
        if (currentPosition.X >= obstacleGrid[0].Length || currentPosition.Y >= obstacleGrid.Length ||
            obstacleGrid[currentPosition.Y][currentPosition.X] == 1)
            return 0;

        if (currentPosition.X == obstacleGrid[0].Length - 1 && currentPosition.Y == obstacleGrid.Length - 1) return 1;

        if (buffer[currentPosition.Y][currentPosition.X] == 0)
            buffer[currentPosition.Y][currentPosition.X] =
                ExploreOtherRoutes((currentPosition.X + 1, currentPosition.Y), obstacleGrid, buffer) +
                ExploreOtherRoutes((currentPosition.X, currentPosition.Y + 1), obstacleGrid, buffer);

        return buffer[currentPosition.Y][currentPosition.X];
    }
}

[TestFixture]
public class UniquePathsIITest
{
    [Test]
    public void UniquePathsWithObstacles_OneElementNotObstacle_ReturnsOne()
    {
        int[][] obstacleGrid = {new[] {0}};
        var uniquePathsII = new UniquePathsII();
        var uniquePaths = uniquePathsII.UniquePathsWithObstacles(obstacleGrid);
        Assert.That(uniquePaths, Is.EqualTo(1));
    }

    [Test]
    public void UniquePathsWithObstacles_OneElementObstacle_ReturnsZero()
    {
        int[][] obstacleGrid = {new[] {1}};
        var uniquePathsII = new UniquePathsII();
        var uniquePaths = uniquePathsII.UniquePathsWithObstacles(obstacleGrid);
        Assert.That(uniquePaths, Is.EqualTo(0));
    }

    [Test]
    public void UniquePathsWithObstacles_AllObstacles_ReturnsZero()
    {
        int[][] obstacleGrid =
        {
            new[] {1, 1, 1},
            new[] {1, 1, 1},
            new[] {1, 1, 1}
        };
        var uniquePathsII = new UniquePathsII();
        var uniquePaths = uniquePathsII.UniquePathsWithObstacles(obstacleGrid);
        Assert.That(uniquePaths, Is.EqualTo(0));
    }

    [Test]
    public void UniquePathsWithObstacles_NoObstacles_ReturnsSix()
    {
        int[][] obstacleGrid =
        {
            new[] {0, 0, 0},
            new[] {0, 0, 0},
            new[] {0, 0, 0}
        };
        var uniquePathsII = new UniquePathsII();
        var uniquePaths = uniquePathsII.UniquePathsWithObstacles(obstacleGrid);
        Assert.That(uniquePaths, Is.EqualTo(6));
    }

    [Test]
    public void UniquePathsWithObstacles_Mixed_ReturnsTwo()
    {
        int[][] obstacleGrid =
        {
            new[] {0, 0, 0},
            new[] {0, 1, 0},
            new[] {0, 0, 0}
        };
        var uniquePathsII = new UniquePathsII();
        var uniquePaths = uniquePathsII.UniquePathsWithObstacles(obstacleGrid);
        Assert.That(uniquePaths, Is.EqualTo(2));
    }

    [Test]
    public void UniquePathsWithObstacles_FirstCellObstacle_ReturnsZero()
    {
        int[][] obstacleGrid =
        {
            new[] {1, 0},
            new[] {0, 0}
        };
        var uniquePathsII = new UniquePathsII();
        var uniquePaths = uniquePathsII.UniquePathsWithObstacles(obstacleGrid);
        Assert.That(uniquePaths, Is.EqualTo(0));
    }

    [Test]
    public void UniquePathsWithObstacles_LastCellObstacle_ReturnsZero()
    {
        int[][] obstacleGrid =
        {
            new[] {0, 0},
            new[] {0, 1}
        };
        var uniquePathsII = new UniquePathsII();
        var uniquePaths = uniquePathsII.UniquePathsWithObstacles(obstacleGrid);
        Assert.That(uniquePaths, Is.EqualTo(0));
    }
}