namespace HackerRank;

using System;
using System.Collections.Generic;
using NUnit.Framework;

public class LargestRectangle
{
    public static int Calculate(int[] h)
    {
        if (h == null || h.Length == 0) return 0;

        var stack = new Stack<int>();
        var maxSquare = 0;
        var i = 0;
        while (i < h.Length)
            if (stack.Count == 0 || h[stack.Peek()] <= h[i])
            {
                stack.Push(i);
                i++;
            }
            else
            {
                var current = stack.Pop();
                maxSquare = Math.Max(maxSquare,
                    stack.Count != 0 ? h[current] * (i - 1 - stack.Peek()) : h[current] * i);
            }

        while (stack.TryPop(out var current))
            maxSquare = Math.Max(maxSquare, stack.Count != 0 ? h[current] * (i - 1 - stack.Peek()) : h[current] * i);

        return maxSquare;
    }
}

[TestFixture]
public class LargestRectangleTests
{
    [TestCaseSource(nameof(GetTestData))]
    public void CalculateTests(int[] h, int expectedResult)
    {
        var result = LargestRectangle.Calculate(h);
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    private static IEnumerable<TestCaseData> GetTestData()
    {
        yield return new TestCaseData(new[] {1, 2, 3}, 4);
        yield return new TestCaseData(new[] {1, 2, 4, 1}, 4);
        yield return new TestCaseData(new[] {3}, 3);
        yield return new TestCaseData(new[] {4, 3, 1}, 6);
        yield return new TestCaseData(new[] {1, 4, 3, 1}, 6);
    }
}