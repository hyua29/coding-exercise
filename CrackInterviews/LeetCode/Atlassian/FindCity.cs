namespace LeetCode.Atlassian;

public class FindCity
{
    public int FindTheCity(int n, int[][] edges, int distanceThreshold)
    {
        var buffer = new int[n, n];

        // Init the buffer with max values
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                buffer[j, i] = int.MaxValue;
            }
        }

        for (int i = 0; i < n; ++i)
        {
            buffer[i, i] = 0;
        }

        foreach (var e in edges)
        {
            buffer[e[0], e[1]] = e[2];
            buffer[e[1], e[0]] = e[2];
        }

        for (int k = 1; k < n; k++)
        {
            for (int i = 1; i < n; i++)
            {
                for (int j = 1; j < n; j++)
                {
                    buffer[i, j] = Math.Min(buffer[i, j], buffer[i, k] + buffer[k, j]);
                }
            }
        }

        //   1 2 3 4 5 6 7 89
        //  1
        //  2
        //  3
        //  4
        //  5
        //  6
        //  7
        //  8
        //  9
        int res = 0, smallest = n;

        for (int i = 0; i < n; i++)
        {
            int count = 0;
            for (int j = 0; j < n; ++j)
                if (buffer[i, j] <= distanceThreshold)
                    count++;
            if (count <= smallest)
            {
                res = i;
                smallest = count;
            }
        }

        return res;
    }
}