namespace LeetCode.Atlassian;

/// <summary>
/// https://leetcode.com/problems/maximum-number-of-occurrences-of-a-substring/
/// </summary>
public class MaximumNumberOccurrencesSubstring
{
    /// <summary>
    /// maxSize has no impact on the results
    /// </summary>
    public int MaxFreq(string s, int maxLetters, int minSize, int maxSize)
    {
        var freq = new Dictionary<string, int>();

        for (int i = 0; i < s.Length - minSize + 1; i++)
        {
            var substring = s.Substring(i, minSize);
            IsSubstringValid(substring, maxLetters, freq);
        }

        return freq.DefaultIfEmpty().Max(b => b.Value);
    }

    private static void IsSubstringValid(string substring, int maxLetters, Dictionary<string, int> freq)
    {
        if (new HashSet<char>(substring).Count <= maxLetters)
        {
            freq[substring] = freq.TryGetValue(substring, out var value) ? value + 1 : 1;
        }
    }
}

[TestFixture]
public class SolutionTests
{
    [Test]
    public void MaxFreq_ExampleCase1()
    {
        // Arrange
        var solution = new MaximumNumberOccurrencesSubstring();
        string s = "aababcaab";
        int maxLetters = 2;
        int minSize = 3;
        int maxSize = 4;

        // Act
        int result = solution.MaxFreq(s, maxLetters, minSize, maxSize);

        // Assert
        Assert.That(result, Is.EqualTo(2), "aab is repeated twice");
    }

    [Test]
    public void MaxFreq_ExampleCase2()
    {
        // Arrange
        var solution = new MaximumNumberOccurrencesSubstring();
        string s = "aaaa";
        int maxLetters = 1;
        int minSize = 3;
        int maxSize = 3;

        // Act
        int result = solution.MaxFreq(s, maxLetters, minSize, maxSize);

        // Assert
        Assert.That(result, Is.EqualTo(2), "a is repeated three times");
    }
}