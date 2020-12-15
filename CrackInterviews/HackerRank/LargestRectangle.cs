using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace HackerRank
{
    public class LargestRectangle
    {
        public static int Calculate(int[] h)
        {
            if (h == null || h.Length == 0) return 0;

            var stack = new Stack<int>();
            int maxSquare = 0;
            int i = 0;
            while (i < h.Length)
            {
                if (stack.Count == 0 || h[stack.Peek()] <= h[i])
                {
                    stack.Push(i);
                    i++;
                }
                else
                {
                    var current = stack.Pop();
                    maxSquare = Math.Max(maxSquare, (stack.Count != 0 ? h[current] * (i-1-stack.Peek()) : h[current] * i));
                }
            }

            while (stack.TryPop(out var current))
            {
                maxSquare = Math.Max(maxSquare, stack.Count != 0 ? h[current] * (i - 1 - stack.Peek()) : h[current] * i);
            }

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
            yield return new TestCaseData(new int[] {1, 2, 3}, 4);
            yield return new TestCaseData(new int[] {1, 2, 4, 1}, 4);
            yield return new TestCaseData(new int[] {3}, 3);
            yield return new TestCaseData(new int[] {4, 3, 1}, 6);
            yield return new TestCaseData(new int[] {1, 4, 3, 1}, 6);
        }
    }
}