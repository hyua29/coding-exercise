namespace LeetCode.Atlassian;

/// <summary>
/// https://leetcode.com/problems/greatest-common-divisor-of-strings/description/
/// </summary>
public class GreatestCommonDivisorOfStrings
{
    /// <summary>
    /// To have GCD substring, the length of it has to be a GCD of len(s1) and len(s2)
    /// </summary>
    public string GcdOfStrings(string str1, string str2)
    {
        if (!(str1 + str2).Equals(str2 + str1)) return "";

        int gcdVal = GreatestCommonString(str1.Length, str2.Length);
        return str2.Substring(0, gcdVal);
    }

    private int GreatestCommonString(int a, int b)
    {
        while (b != 0)
        {
            var remainder = a % b;
            a = b;
            b = remainder;
        }

        return a;
    }
}

[TestFixture]
public class GcdOfStringsTests
{
    [Test]
    public void TestGcdOfStrings1()
    {
        string str1 = "ABCABC";
        string str2 = "ABC";
        string expected = "ABC";

        var solution = new GreatestCommonDivisorOfStrings();
        string result = solution.GcdOfStrings(str1, str2);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void TestGcdOfStrings2()
    {
        string str1 = "ABABAB";
        string str2 = "ABAB";
        string expected = "AB";

        var solution = new GreatestCommonDivisorOfStrings();
        string result = solution.GcdOfStrings(str1, str2);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void TestGcdOfStrings3()
    {
        string str1 = "LEET";
        string str2 = "CODE";
        string expected = "";

        var solution = new GreatestCommonDivisorOfStrings();
        string result = solution.GcdOfStrings(str1, str2);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void TestGcdOfStrings4()
    {
        string str1 = "AABAAABAAACD";
        string str2 = "AABA";
        string expected = "";

        var solution = new GreatestCommonDivisorOfStrings();
        string result = solution.GcdOfStrings(str1, str2);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void TestGcdOfStrings5()
    {
        string str1 = "ABABABCABAABABAB";
        string str2 = "ABAB";
        string expected = string.Empty;

        var solution = new GreatestCommonDivisorOfStrings();
        string result = solution.GcdOfStrings(str1, str2);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void TestGcdOfStrings6()
    {
        string str1 = "ABABABABABABAB";
        string str2 = "ABAB";
        string expected = "AB";

        var solution = new GreatestCommonDivisorOfStrings();
        string result = solution.GcdOfStrings(str1, str2);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void TestGcdOfStrings7()
    {
        string str1 = "TAUXXTAUXXTAUXXTAUXXTAUXX";
        string str2 = "TAUXXTAUXXTAUXXTAUXXTAUXXTAUXXTAUXXTAUXXTAUXX";
        string expected = "TAUXX";

        var solution = new GreatestCommonDivisorOfStrings();
        string result = solution.GcdOfStrings(str1, str2);

        Assert.That(result, Is.EqualTo(expected));
    }
}