namespace LeetCode.LeetCode150;

public class NumberOfIslands
{
    public int NumIslands(char[][] grid)
    {
        var results = 0;
        var hasExplored = grid.Select(x => new bool[x.Length]).ToArray();

        var connected = new Queue<(int Y, int X)>();
        for (var y = 0; y < grid.Length; y++)
        {
            for (var x = 0; x < grid[0].Length; x++)
            {
                if (hasExplored[y][x]) continue;

                hasExplored[y][x] = true;

                // Found the entry point of the island, we need to find all the islands connected to it
                if (grid[y][x] == '1')
                {
                    connected.Enqueue((y, x));
                    FindAllConnected(connected, grid, hasExplored);
                    results++;
                }
            }
        }

        return results;
    }

    private static void FindAllConnected(Queue<(int Y, int X)> connected, char[][] grid, bool[][] hasExplored)
    {
        while (connected.TryDequeue(out (int Y, int X) current))
        {
            (int Y, int X) rightLocation = (current.Y, current.X + 1);
            if (rightLocation.X < grid[0].Length && !hasExplored[rightLocation.Y][rightLocation.X] &&
                grid[rightLocation.Y][rightLocation.X] == '1')
            {
                hasExplored[rightLocation.Y][rightLocation.X] = true;

                connected.Enqueue(rightLocation);
            }

            (int Y, int X) downLocation = (current.Y + 1, current.X);
            if (downLocation.Y < grid.Length && !hasExplored[downLocation.Y][downLocation.X] &&
                grid[downLocation.Y][downLocation.X] == '1')
            {
                hasExplored[downLocation.Y][downLocation.X] = true;

                connected.Enqueue(downLocation);
            }

            (int Y, int X) leftLocation = (current.Y, current.X - 1);
            if (leftLocation.X >= 0 && !hasExplored[leftLocation.Y][leftLocation.X] &&
                grid[leftLocation.Y][leftLocation.X] == '1')
            {
                hasExplored[leftLocation.Y][leftLocation.X] = true;

                connected.Enqueue(leftLocation);
            }

            (int Y, int X) upLocation = (current.Y - 1, current.X);
            if (upLocation.Y >= 0 && !hasExplored[upLocation.Y][upLocation.X] &&
                grid[upLocation.Y][upLocation.X] == '1')
            {
                hasExplored[upLocation.Y][upLocation.X] = true;

                connected.Enqueue(upLocation);
            }
        }
    }
}

public class NumberOfIslandsTests
{
    [Test]
    public void Test()
    {
        var s = new NumberOfIslands();

        var input = new char[][]
        {
            new char[] {'1','1','1','1','1','0','1','1','1','1','1','1','1','1','1','0','1','0','1','1'},
            new char[] {'0','1','1','1','1','1','1','1','1','1','1','1','1','0','1','1','1','1','1','0'},
            new char[] {'1','0','1','1','1','0','0','1','1','0','1','1','1','1','1','1','1','1','1','1'},
            new char[] {'1','1','1','1','0','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1'},
            new char[] {'1','0','0','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1'},
            new char[] {'1','0','1','1','1','1','1','1','0','1','1','1','0','1','1','1','0','1','1','1'},
            new char[] {'0','1','1','1','1','1','1','1','1','1','1','1','0','1','1','0','1','1','1','1'},
            new char[] {'1','1','1','1','1','1','1','1','1','1','1','1','0','1','1','1','1','0','1','1'},
            new char[] {'1','1','1','1','1','1','1','1','1','1','0','1','1','1','1','1','1','1','1','1'},
            new char[] {'1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1'},
            new char[] {'0','1','1','1','1','1','1','1','0','1','1','1','1','1','1','1','1','1','1','1'},
            new char[] {'1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1'},
            new char[] {'1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1'},
            new char[] {'1','1','1','1','1','0','1','1','1','1','1','1','1','0','1','1','1','1','1','1'},
            new char[] {'1','0','1','1','1','1','1','0','1','1','1','0','1','1','1','1','0','1','1','1'},
            new char[] {'1','1','1','1','1','1','1','1','1','1','1','1','0','1','1','1','1','1','1','0'},
            new char[] {'1','1','1','1','1','1','1','1','1','1','1','1','1','0','1','1','1','1','0','0'},
            new char[] {'1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1'},
            new char[] {'1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1'},
            new char[] {'1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1','1'},
        };

        Assert.That(s.NumIslands(input), Is.EqualTo(1));
    }
}