namespace LeetCode.Atlassian;

public class FindCity
{
    public int FindTheCity(int n, int[][] edges, int distanceThreshold)
    {
        if (distanceThreshold == 0) return -1;

        var buffer = new int[n, n];
        for (var i = 0; i < n; i++)
        for (var j = 0; j < n; j++)
            buffer[i, j] = 10001;


        for (var i = 0; i < n; i++) buffer[i, i] = 0;

        foreach (var e in edges) (buffer[e[0], e[1]], buffer[e[1], e[0]]) = (e[2], e[2]);

        for (var k = 0; k < n; k++)
        for (var i = 0; i < n; i++)
        for (var j = 0; j < n; j++)
            buffer[i, j] = Math.Min(buffer[i, j], buffer[i, k] + buffer[k, j]);

        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++) Console.Write(buffer[j, i] + " ");

            Console.WriteLine();
        }

        int res = 0, smallest = n;
        for (var i = 0; i < n; i++)
        {
            var count = 0;
            for (var j = 0; j < n; j++)
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
        var n = 4;
        int[][] edges =
        {
            new[] {0, 1, 3},
            new[] {1, 2, 1},
            new[] {1, 3, 4},
            new[] {2, 3, 1}
        };
        var distanceThreshold = 4;
        var expected = 3;
        var findCity = new FindCity();

        // Act
        var actual = findCity.FindTheCity(n, edges, distanceThreshold);

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Test_FindTheCity_Should_Return_Zero_When_No_City_Is_Found()
    {
        // Arrange
        var n = 2;
        int[][] edges =
        {
            new[] {0, 1, 1}
        };
        var distanceThreshold = 0;
        var expected = -1;
        var findCity = new FindCity();

        // Act
        var actual = findCity.FindTheCity(n, edges, distanceThreshold);

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}