namespace LeetCode.Atlassian;

public class FindCity
{
    public int FindTheCity(int n, int[][] edges, int distanceThreshold)
    {
        if (distanceThreshold == 0)
        {
            return -1;
        }

        var buffer = new int[n, n];
        for (var i = 0; i < n; i++)
        for (var j = 0; j < n; j++)
            buffer[i, j] = 10001;


        for (int i = 0; i < n; i++)
        {
            buffer[i, i] = 0;
        }

        foreach (var e in edges) (buffer[e[0], e[1]], buffer[e[1], e[0]]) = (e[2], e[2]);

        for (var k = 0; k < n; k++)
        for (var i = 0; i < n; i++)
        for (var j = 0; j < n; j++)
        {
            buffer[i, j] = Math.Min(buffer[i, j], buffer[i, k] + buffer[k, j]);
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write(buffer[j, i] + " ");
            }

            Console.WriteLine();
        }

        int res = 0, smallest = n;
        for (int i = 0; i < n; i++)
        {
            int count = 0;
            for (int j = 0; j < n; j++)
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

[TestFixture]
public class FindCityTests
{
    [Test]
    public void Test_FindTheCity_Should_Return_Correct_Result()
    {
        // Arrange
        int n = 4;
        int[][] edges = new int[][]
        {
            new int[] {0, 1, 3},
            new int[] {1, 2, 1},
            new int[] {1, 3, 4},
            new int[] {2, 3, 1}
        };
        int distanceThreshold = 4;
        int expected = 3;
        FindCity findCity = new FindCity();

        // Act
        int actual = findCity.FindTheCity(n, edges, distanceThreshold);

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Test_FindTheCity_Should_Return_Zero_When_No_City_Is_Found()
    {
        // Arrange
        int n = 2;
        int[][] edges = new int[][]
        {
            new int[] {0, 1, 1}
        };
        int distanceThreshold = 0;
        int expected = -1;
        FindCity findCity = new FindCity();

        // Act
        int actual = findCity.FindTheCity(n, edges, distanceThreshold);

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}