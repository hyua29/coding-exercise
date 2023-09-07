namespace LeetCode.LeetCode75;

/// <summary>
/// https://leetcode.com/problems/longest-common-subsequence/solutions/350993/python-dp-and-recursive/?envType=study-plan-v2&envId=leetcode-75
/// dp(i,j) means the longest common subsequence of text1[:i] and text2[:j]. If text1[i]==text2[j], then dp(i,j) should equal dp(i-1,j-1)+1 Otherwise, dp(i,j)=max(dp(i-1,j), dp(i,j-1))
/// </summary>
public class LongestCommonSubsequenceClass
{
    public int LongestCommonSubsequence(string text1, string text2)
    {
        var buffer = new int[2, text2.Length + 1];

        for (var i = 0; i < text1.Length; i++)
        {
            for (var j = 0; j < text2.Length; j++)
            {
                if (text1[i] == text2[j])
                {
                    buffer[(i + 1) % 2, j + 1] = buffer[i % 2, j] + 1;
                }
                else
                {
                    buffer[(i + 1) % 2, j + 1] = Math.Max(buffer[(i + 1) % 2, j], buffer[i % 2, j + 1]);
                }
            }
        }

        return buffer[text1.Length % 2, text2.Length];
    }
}

[TestFixture]
public class LongestCommonSubsequenceTests
{
    private LongestCommonSubsequenceClass _solution;

    [SetUp]
    public void Setup()
    {
        _solution = new LongestCommonSubsequenceClass();
    }

    [Test]
    public void Test1()
    {
        string text1 = "abcde";
        string text2 = "ace";
        int expected = 3;
        int result = _solution.LongestCommonSubsequence(text1, text2);
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void Test2()
    {
        string text1 = "abc";
        string text2 = "def";
        int expected = 0;
        int result = _solution.LongestCommonSubsequence(text1, text2);
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void Test3()
    {
        string text1 = "abc";
        string text2 = "abc";
        int expected = 3;
        int result = _solution.LongestCommonSubsequence(text1, text2);
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void Test4()
    {
        string text1 = "abccccccd";
        string text2 = "abcd";
        int expected = 4;
        int result = _solution.LongestCommonSubsequence(text1, text2);
        Assert.AreEqual(expected, result);
    }
}