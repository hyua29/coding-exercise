namespace LeetCode.LeetCode150;

public class GameOfLifeProblem
{
    public void GameOfLife(int[][] board)
    {
        var nextBoard = board.Select(row => new int[row.Length]).ToArray();

        for (int y = 0; y < board.Length; y++)
        {
            for (int x = 0; x < board[y].Length; x++)
            {
                var neighbourCount = GetLiveNeighbours(board, (y, x));

                if (neighbourCount < 2)
                {
                    nextBoard[y][x] = 0;
                }
                else if (neighbourCount == 3)
                {
                    nextBoard[y][x] = 1;
                }
                else if (neighbourCount > 3)
                {
                    nextBoard[y][x] = 0;
                }
                else
                {
                    nextBoard[y][x] = board[y][x];
                }
            }
        }

        for (int i = 0; i < board.Length; i++)
        {
            board[i] = nextBoard[i];
        }
    }

    private int GetLiveNeighbours(int[][] board, (int Y, int X) position)
    {
        var liveNeighbours = 0;

        // left
        if (position.X - 1 >= 0 && board[position.Y][position.X - 1] == 1)
        {
            liveNeighbours++;
        }

        // right
        if (position.X + 1 < board[0].Length && board[position.Y][position.X + 1] == 1)
        {
            liveNeighbours++;
        }

        // top
        if (position.Y - 1 >= 0 && board[position.Y - 1][position.X] == 1)
        {
            liveNeighbours++;
        }

        // bottom
        if (position.Y + 1 < board.Length && board[position.Y + 1][position.X] == 1)
        {
            liveNeighbours++;
        }

        // top-left
        if (position.Y - 1 >= 0 && position.X - 1 >= 0 && board[position.Y - 1][position.X - 1] == 1)
        {
            liveNeighbours++;
        }

        // top-right
        if (position.Y - 1 >= 0 && position.X + 1 < board[0].Length && board[position.Y - 1][position.X + 1] == 1)
        {
            liveNeighbours++;
        }

        // bottom-left
        if (position.Y + 1 < board.Length && position.X - 1 >= 0 && board[position.Y + 1][position.X - 1] == 1)
        {
            liveNeighbours++;
        }

        // bottom-right
        if (position.Y + 1 < board.Length && position.X + 1 < board[0].Length &&
            board[position.Y + 1][position.X + 1] == 1)
        {
            liveNeighbours++;
        }

        return liveNeighbours;
    }
}

public class GameOfLifeTests
{
    [Test]
    public void Test_1()
    {
        var sol = new GameOfLifeProblem();

        var input = new int[][]
        {
            new[] {1, 1},
            new[] {1, 0},
        };

        sol.GameOfLife(input);
        
        var expectedResult = new int[][]
        {
            new[] {1, 1},
            new[] {1, 1},
        };

        for (int i = 0; i < expectedResult.Length; i++)
        {
            Assert.That(expectedResult[i], Is.EqualTo(input[i]));
        }
    }
}