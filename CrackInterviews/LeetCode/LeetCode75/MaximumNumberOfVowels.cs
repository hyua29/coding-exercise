namespace LeetCode.LeetCode75;

/// <summary>
/// https://leetcode.com/problems/maximum-number-of-vowels-in-a-substring-of-given-length/description/
/// </summary>
public class MaximumNumberOfVowels
{
    private static readonly ISet<char> Vowels = new HashSet<char> {'a', 'e', 'i', 'o', 'u'};

    public int MaxVowels(string s, int k)
    {
        var vowelCount = 0;
        for (int i = 0; i < k; i++)
        {
            if (Vowels.Contains(s[i]))
            {
                vowelCount++;
            }
        }

        var maxVowelCount = vowelCount;

        for (int i = k; i < s.Length; i++)
        {
            if (Vowels.Contains(s[i - k]))
            {
                vowelCount--;
            }

            if (Vowels.Contains(s[i]))
            {
                vowelCount++;
            }

            maxVowelCount = Math.Max(maxVowelCount, vowelCount);
        }

        return maxVowelCount;
    }
}

[TestFixture]
public class SolutionTests
{
    private readonly MaximumNumberOfVowels _solution = new MaximumNumberOfVowels();

    [Test]
    public void Test1()
    {
        string s = "abciiidef";
        int k = 3;
        int expected = 3;

        int actual = _solution.MaxVowels(s, k);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Test2()
    {
        string s = "leetcode";
        int k = 2;
        int expected = 2;

        int actual = _solution.MaxVowels(s, k);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Test3()
    {
        string s = "rhythms";
        int k = 4;
        int expected = 0;

        int actual = _solution.MaxVowels(s, k);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Test4()
    {
        string s = "aeiou";
        int k = 1;
        int expected = 1;

        int actual = _solution.MaxVowels(s, k);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Test6()
    {
        string s = "";
        int k = 5;
        int expected = 0;

        int actual = _solution.MaxVowels(s, k);

        Assert.That(actual, Is.EqualTo(expected));
    }
}