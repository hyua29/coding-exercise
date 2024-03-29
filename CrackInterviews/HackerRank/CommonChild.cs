namespace HackerRank;

using System;
using NUnit.Framework;

/// https://en.wikipedia.org/wiki/Longest_common_subsequence_problem
[TestFixture]
internal class CommonChild
{
    private static int Calculate1(string s1, string s2)
    {
        if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
            return 0;

        var buffer = new int[s1.Length + 1, s2.Length + 1];
        for (var i = 0; i < s1.Length; i++)
        for (var j = 0; j < s2.Length; j++)
            if (s1[i] == s2[j])
                buffer[i + 1, j + 1] = buffer[i, j] + 1;
            else
                buffer[i + 1, j + 1] = Math.Max(buffer[i + 1, j], buffer[i, j + 1]);

        return buffer[s1.Length, s2.Length];
    }

    [TestCase("", "", 0)]
    [TestCase("", null, 0)]
    [TestCase(null, null, 0)]
    [TestCase(null, "", 0)]
    [TestCase("HARRYYYA", "SALLYYYL", 4)]
    [TestCase("HARRYYY", "SALLYYY", 4)]
    [TestCase("HARRY", "SALLY", 2)]
    [TestCase("AA", "BB", 0)]
    [TestCase("HALLL", "SLLLA", 3)]
    public void CommonChildTest(string s1, string s2, int result)
    {
        Assert.That(Calculate1(s1, s2), Is.EqualTo(result));
    }
}