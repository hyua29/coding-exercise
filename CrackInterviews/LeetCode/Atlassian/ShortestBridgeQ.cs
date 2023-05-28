namespace LeetCode.Atlassian;

// See:https://leetcode.com/problems/shortest-bridge/solutions/2033952/python-bfs-dfs-easy-and-efficient-solution-with-explanation/?orderBy=most_votes
public class ShortestBridgeQ
{
    public int ShortestBridge(int[][] grid)
    {
        var coordinate = FindFirstIsland(grid);

        var firstIsland = new Queue<(int Y, int X)>();

        var firstIslandHash = new HashSet<(int Y, int X)>();
        MarkIsland(grid, coordinate, firstIsland, firstIslandHash);

        var queue = firstIsland;

        var level = FindClosestPathToSecond(grid, queue, firstIslandHash);

        return level;
    }

    private static int FindClosestPathToSecond(int[][] grid, Queue<(int Y, int X)> queue,
        HashSet<(int Y, int X)> firstIslandHash)
    {
        var set = new HashSet<(int Y, int X)>();
        var level = -1;
        var foundNextIsland = false;
        while (!foundNextIsland && queue.Count > 0)
        {
            level++;

            var count = queue.Count;
            for (var i = 0; i < count; i++)
            {
                var c = queue.Dequeue();

                if (c.Y < grid.Length && c.Y >= 0 && c.X < grid[0].Length && c.X >= 0 && !set.Contains((c.Y, c.X)))
                {
                    if (grid[c.Y][c.X] == 1 && !firstIslandHash.Contains((c.Y, c.X)))
                    {
                        foundNextIsland = true;
                        break;
                    }

                    set.Add((c.Y, c.X));

                    queue.Enqueue((c.Y + 1, c.X));
                    queue.Enqueue((c.Y - 1, c.X));
                    queue.Enqueue((c.Y, c.X + 1));
                    queue.Enqueue((c.Y, c.X - 1));
                }
            }
        }

        return level - 1;
    }

    private static void MarkIsland(int[][] grid, (int Y, int X) coordinate, Queue<(int Y, int X)> island,
        HashSet<(int Y, int X)> set)
    {
        var queue = new Queue<(int Y, int X)>();
        queue.Enqueue(coordinate);

        while (queue.TryDequeue(out var c))
            if (grid[c.Y][c.X] == 1 && !set.Contains((c.Y, c.X)))
            {
                set.Add(c);
                island.Enqueue(c);

                if (c.Y + 1 < grid.Length) queue.Enqueue((c.Y + 1, c.X));

                if (c.Y - 1 >= 0) queue.Enqueue((c.Y - 1, c.X));

                if (c.X + 1 < grid[0].Length) queue.Enqueue((c.Y, c.X + 1));

                if (c.X - 1 >= 0) queue.Enqueue((c.Y, c.X - 1));
            }
    }

    private static (int Y, int X) FindFirstIsland(int[][] grid)
    {
        for (var y = 0; y < grid.Length; y++)
        for (var x = 0; x < grid[0].Length; x++)
            if (grid[y][x] == 1)
                return (y, x);

        throw new ArgumentException("The grid should contain exactly 2 islands");
    }
}

[TestFixture]
public class ShortestBridgeQTests
{
    [Test]
    public void ShortestBridge_ShouldReturnCorrectResult_WhenGivenValidInput()
    {
        // Arrange
        var grid = new[]
        {
            new[] {0, 1, 0},
            new[] {0, 0, 0},
            new[] {0, 0, 1}
        };

        var shortestBridgeQ = new ShortestBridgeQ();

        // Act
        var result = shortestBridgeQ.ShortestBridge(grid);

        // Assert
        Assert.That(result, Is.EqualTo(2));
    }

    [Test]
    public void ShortestBridge_ShouldReturnCorrectResult_WhenGivenValidInput1()
    {
        // Arrange
        var grid = new[]
        {
            new[] {0, 1},
            new[] {1, 0}
        };

        var shortestBridgeQ = new ShortestBridgeQ();

        // Act
        var result = shortestBridgeQ.ShortestBridge(grid);

        // Assert
        Assert.That(result, Is.EqualTo(1));
    }

    [Test]
    public void ShortestBridge_ShouldReturnCorrectResult_WhenGivenValidInput2()
    {
        // Arrange
        var grid = new[]
        {
            new[] {1, 1, 1, 1, 1},
            new[] {1, 0, 0, 0, 1},
            new[] {1, 0, 1, 0, 1},
            new[] {1, 0, 0, 0, 1},
            new[] {1, 1, 1, 1, 1}
        };

        var shortestBridgeQ = new ShortestBridgeQ();

        // Act
        var result = shortestBridgeQ.ShortestBridge(grid);

        // Assert
        Assert.That(result, Is.EqualTo(1));
    }

    [Test]
    public void ShortestBridge_ShouldThrowArgumentException_WhenGivenInvalidInput()
    {
        // Arrange
        var grid = new[]
        {
            new[] {0, 0, 0},
            new[] {0, 0, 0},
            new[] {0, 0, 0}
        };

        var shortestBridgeQ = new ShortestBridgeQ();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => shortestBridgeQ.ShortestBridge(grid));
    }
}