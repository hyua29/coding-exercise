namespace LeetCode.Atlassian;

/// <summary>
/// https://leetcode.com/problems/maximum-number-of-occurrences-of-a-substring/
/// </summary>
public class MaximumNumberOccurrencesSubstring
{
    public int MaxFreq(string s, int maxLetters, int minSize, int maxSize)
    {
        // Debug.Assert(minSize >= 1);
        // Debug.Assert(minSize <= maxSize);
        // Debug.Assert(minSize <= s.Length);

        var buffer = new Dictionary<string, int>();

        for (int i = 0; i < s.Length - minSize + 1; i++)
        {
            var substirngs = GetSubStrings(s, i, minSize, maxSize);

            foreach (var sub in substirngs)
            {
                if (buffer.Keys.Contains(sub))
                {
                    buffer[sub]++;
                }
                else
                {
                    buffer.Add(sub, 1);
                }
            }
        }

        return buffer.DefaultIfEmpty().Max(b => b.Value);
    }

    private static IList<string> GetSubStrings(string s, int currentIndex, int minSize, int maxSize)
    {
        var substrings = new List<string>();

        var start = s.Substring(currentIndex, minSize);
        substrings.Add(start);
        for (int i = 0; i < maxSize - minSize; i++)
        {
            if (currentIndex + i + minSize >= s.Length)
            {
                break;
            }

            start += s[currentIndex + i + minSize];
            substrings.Add(start);
        }

        return substrings;
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
        Assert.That(result, Is.EqualTo(2));
    }
}