namespace LeetCode.LeetCode150;

public class SpiralMatrix
{
    public IList<int> SpiralOrder(int[][] matrix)
    {
        bool[][] discovered = new bool[matrix.Length][];
        for (int i = 0; i < discovered.Length; i++)
        {
            discovered[i] = new bool[matrix[0].Length];
        }

        List<int> results = new List<int>(matrix.Length * matrix[0].Length);

        GoRight(0, 0, matrix, discovered, results);

        return results;
    }

    private void GoRight(int y, int x, int[][] matrix, bool[][] discovered, List<int> results)
    {
        if (x < 0 ||  x >= matrix[0].Length || y < 0 || y >= matrix.Length || discovered[y][x])
        {
            return;
        }

        do
        {
            results.Add(matrix[y][x]);
            discovered[y][x] = true;
            x++;
        } while (x < matrix[0].Length && !discovered[y][x]);

        GoDown(y + 1, x - 1, matrix, discovered, results);
    }

    private void GoDown(int y, int x, int[][] matrix, bool[][] discovered, List<int> results)
    {
        if (x < 0 ||  x >= matrix[0].Length || y < 0 || y >= matrix.Length || discovered[y][x])
        {
            return;
        }

        do
        {
            results.Add(matrix[y][x]);
            discovered[y][x] = true;
            y++;
        } while (y < matrix.Length && !discovered[y][x]);

        GoLeft(y - 1, x - 1, matrix, discovered, results);
    }

    private void GoLeft(int y, int x, int[][] matrix, bool[][] discovered, List<int> results)
    {
        if (x < 0 ||  x >= matrix[0].Length || y < 0 || y >= matrix.Length || discovered[y][x])
        {
            return;
        }

        do
        {
            results.Add(matrix[y][x]);
            discovered[y][x] = true;
            x--;
        } while (x >= 0 && !discovered[y][x]);

        GoUp(y - 1, x + 1, matrix, discovered, results);
    }

    private void GoUp(int y, int x, int[][] matrix, bool[][] discovered, List<int> results)
    {
        if (x < 0 ||  x >= matrix[0].Length || y < 0 || y >= matrix.Length || discovered[y][x])
        {
            return;
        }

        do
        {
            results.Add(matrix[y][x]);
            discovered[y][x] = true;
            y--;
        } while (y >= 0 && !discovered[y][x]);

        GoRight(y + 1, x + 1, matrix, discovered, results);
    }
}