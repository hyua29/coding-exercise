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

        int[,] cost = new int[m + 1, n + 1];
        for (int i = 0; i <= m; i++)
            cost[i, 0] = i;
        for (int i = 1; i <= n; i++)
            cost[0, i] = i;

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (word1[i] == word2[j])
                    cost[i + 1, j + 1] = cost[i, j];
                else
                {
                    int a = cost[i, j];
                    int b = cost[i, j + 1];
                    int c = cost[i + 1, j];
                    cost[i + 1, j + 1] = a < b ? (a < c ? a : c) : (b < c ? b : c);
                    cost[i + 1, j + 1]++;
                }
            }
        }

        return cost[m, n];
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