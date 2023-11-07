namespace LeetCode.LeetCode150;

public class RotateImage
{
    public void Rotate(int[][] matrix)
    {
        var edgeLength = matrix.Length;
        for (int y = 0; y < edgeLength - 1; y++)
        {
            for (int x = 0; x < edgeLength - 1 - y; x++)
            {
                (matrix[edgeLength - 1 - x][edgeLength - 1 - y], matrix[y][x]) =
                    (matrix[y][x], matrix[edgeLength - 1 - x][edgeLength - 1 - y]);
            }
        }

        for (int y = 0; y < edgeLength / 2; y++)
        {
            for (int x = 0; x < edgeLength; x++)
            {
                (matrix[y][x], matrix[edgeLength - 1 - y][x]) = (matrix[edgeLength - 1 - y][x], matrix[y][x]);
            }
        }

    }
}