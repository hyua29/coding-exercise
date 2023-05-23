namespace LeetCode.Atlassian;

public class FindUsersActiveMinutes
{
    public int[] FindingUsersActiveMinutes(int[][] logs, int k)
    {
        var buffer = new Dictionary<int, HashSet<int>>();
        foreach (var l in logs)
        {
            if (buffer.TryGetValue(l[0], out var set))
            {
                set.Add(l[1]);
            }
            else
            {
                buffer.Add(l[0], new HashSet<int> {l[1]});
            }
        }

        var answers = new int[k];

        foreach (var b in buffer)
        {
            var count = b.Value.Count;
            if (count <= k)
            {
                answers[count - 1]++;
            }
        }

        return answers;
    }
}

[TestFixture]
public class FindUsersActiveMinutesTests
{
    [Test]
    public void FindingUsersActiveMinutes_ExampleInput_ReturnsExpectedResult()
    {
        // Arrange
        var logs = new int[][]
        {
            new int[] {0, 5},
            new int[] {1, 2},
            new int[] {0, 2},
            new int[] {0, 5},
            new int[] {1, 3}
        };
        var k = 5;
        var expectedResult = new int[] {0, 2, 0, 0, 0};

        var sut = new FindUsersActiveMinutes();

        // Act
        var result = sut.FindingUsersActiveMinutes(logs, k);

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    public void FindingUsersActiveMinutes_ExampleInput_ReturnsExpectedResult2()
    {
        // Arrange
        var logs = new int[][]
        {
            new int[] {1, 1},
            new int[] {2, 2},
            new int[] {2, 3}
        };
        var k = 4;
        var expectedResult = new int[] {1, 1, 0, 0};

        var sut = new FindUsersActiveMinutes();

        // Act
        var result = sut.FindingUsersActiveMinutes(logs, k);

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    public void FindingUsersActiveMinutes_EmptyInput_ReturnsAllZeroes()
    {
        // Arrange
        var logs = new int[0][];
        var k = 3;
        var expectedResult = new int[] {0, 0, 0};

        var sut = new FindUsersActiveMinutes();

        // Act
        var result = sut.FindingUsersActiveMinutes(logs, k);

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }
}