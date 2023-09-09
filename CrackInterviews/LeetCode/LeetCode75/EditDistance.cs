namespace LeetCode.LeetCode75;

/// <summary>
/// https://leetcode.com/problems/edit-distance/solutions/25849/java-dp-solution-o-nm/?envType=study-plan-v2&envId=leetcode-75
/// </summary>
public class EditDistance
{
    public int MinDistance(string word1, string word2)
    {
        int m = word1.Length;
        int n = word2.Length;

        var buffer = new int [m + 1, n + 1];

        for (int i = 1; i < m + 1; i++)
        {
            buffer[i, 0] = i;
        }

        for (int j = 1; j < n + 1; j++)
        {
            buffer[0, j] = j;
        }

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (word1[i] == word2[j])
                {
                    buffer[i + 1, j + 1] = buffer[i, j];
                }
                else
                {
                    buffer[i + 1, j + 1] = Math.Min(Math.Min(buffer[i, j + 1], buffer[i + 1, j]), buffer[i, j]) + 1;
                }
            }
        }

        return buffer[m, n];
    }
}

[TestFixture]
public class EditDistanceTests
{
    private EditDistance _solution;

    [SetUp]
    public void Setup()
    {
        _solution = new EditDistance();
    }

    [Test]
    public void Test1()
    {
        string text1 = "intention";
        string text2 = "execution";
        int expected = 5;
        int result = _solution.MinDistance(text1, text2);
        Assert.That(result, Is.EqualTo(expected));
    }
}