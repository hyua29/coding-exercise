namespace HackerRank;

using NUnit.Framework;

[TestFixture]
internal class HighestValuePalindrome
{
    private static string Calculate1(string s, int n, int k)
    {
        var charArray = s.ToCharArray();

        // Obtain Palindrome
        var iterations = n / 2 + n % 2;
        var buffer = new bool[iterations];
        for (var i = 0; i < iterations; i++)
            if (s[i] > s[n - i - 1])
            {
                buffer[i] = true;
                charArray[n - i - 1] = charArray[i];
                k--;
            }
            else if (s[i] < s[n - i - 1])
            {
                buffer[i] = true;
                charArray[i] = charArray[n - i - 1];
                k--;
            }

        if (k < 0) return "-1";

        // Get max palindrome
        for (var i = 0; i < iterations; i++)
        {
            if (buffer[i] && k >= 1 && charArray[i] != '9')
            {
                charArray[i] = '9';
                charArray[n - i - 1] = '9';
                k--;
            }
            else if (!buffer[i] && k >= 2 && charArray[i] != '9')
            {
                charArray[i] = '9';
                charArray[n - i - 1] = '9';
                k -= 2;
            }
            else if (n % 2 != 0 && i == iterations - 1 && k >= 1 && charArray[i] != '9')
            {
                charArray[i] = '9';
                k--;
            }

            if (k < 0) break;
        }

        return new string(charArray);
    }

    [TestCase(4, 87888, "1231", "9999")]
    public void HighestValuePalindromeSuccessfulTest(int n, int k, string s, string expectedResult)
    {
        var result = Calculate1(s, n, k);
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    // [TestCase("qwerasdf")]
    // public void HighestValuePalindromeFailedTest(string input)
    // {
    //     Assert.True(HighestValuePalindrome.Calculate1(input));
    // }
}