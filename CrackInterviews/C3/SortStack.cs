using System;
using System.Linq;
using NUnit.Framework;

namespace C3
{
    using System.Collections.Generic;

    public class SortStack
    {
        public static Stack<int> Calculate1(Stack<int> stack)
        {
            var buffer = new Stack<int>();

            while (stack.Count > 0)
            {
                var temp = stack.Pop();

                while (buffer.TryPeek(out var top) && top < temp)
                {
                    stack.Push(buffer.Pop());
                }

                buffer.Push(temp);
            }

            return buffer;
        }

        [TestCaseSource(nameof(GetTestData))]
        public void SortStackTest(Stack<int> input)
        {
            var expectedResult = input.ToArray();
            Array.Sort(expectedResult);

            var results = Calculate1(input);

            Assert.That(results.Count, Is.EqualTo(expectedResult.Length));
            foreach (var r in expectedResult)
            {
                Assert.That(results.Pop(), Is.EqualTo(r));
            }
        }

        private static IEnumerable<TestCaseData> GetTestData()
        {
            var input = new Stack<int>();
            input.Push(10);
            input.Push(12);
            input.Push(10);
            input.Push(11);
            input.Push(0);
            yield return new TestCaseData(input);

            input = new Stack<int>();
            input.Push(0);
            yield return new TestCaseData(input);

            input = new Stack<int>();
            input.Push(1);
            input.Push(2);
            input.Push(3);
            yield return new TestCaseData(input);

            input = new Stack<int>();
            input.Push(3);
            input.Push(2);
            input.Push(1);
            yield return new TestCaseData(input);

            input = new Stack<int>();
            input.Push(1);
            input.Push(1);
            input.Push(1);
            yield return new TestCaseData(input);
        }
    }
}