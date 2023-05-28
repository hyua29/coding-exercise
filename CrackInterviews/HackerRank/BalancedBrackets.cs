namespace HackerRank;

using System.Collections.Generic;
using NUnit.Framework;

public class BalancedBrackets
{
    public static bool Calculate(string s)
    {
        if (s == null) return false;

        var stack = new Stack<char>();

        foreach (var c in s)
            if (IsLeftBracket(c))
            {
                stack.Push(c);
            }
            else
            {
                if (stack.Count == 0) return false;
                var left = stack.Pop();
                if (!IsBracketMatching(left, c)) return false;
            }

        return stack.Count == 0;
    }

    private static bool IsLeftBracket(char c)
    {
        if (c == '{' || c == '[' || c == '(') return true;
        return false;
    }

    private static bool IsBracketMatching(char left, char right)
    {
        if (left == '{' && right == '}') return true;
        if (left == '[' && right == ']') return true;
        if (left == '(' && right == ')') return true;
        return false;
    }
}

[TestFixture]
public class BalancedBracketsTests
{
    [TestCase("{{}}{}", true)]
    [TestCase("{{[[(())]]}}", true)]
    [TestCase("{[(])}", false)]
    [TestCase("{]{}", false)]
    [TestCase("", true)]
    [TestCase(null, false)]
    public void CalculateTests(string s, bool expectedResult)
    {
        Assert.That(BalancedBrackets.Calculate(s), Is.EqualTo(expectedResult));
    }
}