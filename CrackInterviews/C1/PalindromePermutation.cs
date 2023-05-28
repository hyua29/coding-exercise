namespace C1;

using NUnit.Framework;

[TestFixture]
internal class PalindromePermutation
{
    private static bool Calculate1(string input)
    {
        if (string.IsNullOrEmpty(input))
            return true;

        var buffer = new int[128];

        foreach (var i in input) buffer[i]++;

        var oddCount = 0;
        foreach (var b in buffer)
            if (b % 2 != 0)
                oddCount++;

        return oddCount < 1;
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase("qweqwewqeqwe")]
    [TestCase("asdfghjkl;''as;ldkfgjh")]
    public void ParlindromePermutationSuccessfulTest(string input)
    {
        Assert.True(Calculate1(input));
    }

    [TestCase("qazwsxedcqazwsxed")]
    [TestCase("1=2-304958671=2-0394857")]
    public void ParlindromePermutationFailedTest(string input)
    {
        Assert.False(Calculate1(input));
    }
}