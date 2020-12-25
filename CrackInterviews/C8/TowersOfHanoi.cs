using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace C8
{
    public class TowersOfHanoi
    {
        public static Stack<int> Calculate(Stack<int> first)
        {
            var second = new Stack<int>();
            var third = new Stack<int>();
            if (first == null) return null;

            MoveTower(first.Count, first, third, second);
            return third;
        }

        private static void MoveTower(int count, Stack<int> from, Stack<int> to, Stack<int> buffer)
        {
            if (count > 0)
            {
                MoveTower(count - 1, from, buffer, to);
                to.Push(from.Pop());
                MoveTower(count - 1, buffer, to, from);
            }
        }
    }

    [TestFixture]
    public class TwoersOfHanoiTests
    {

        [TestCaseSource(nameof(GetTestData))]
        public void CalculateTest(Stack<int> first, Stack<int> expectedResult)
        {
            Stack<int> result;
            if (first == null)
            {
                result = TowersOfHanoi.Calculate(first);
                Assert.That(result, Is.EqualTo(expectedResult));
                return;
            }

            var firstCopy = new Stack<int>(first);
            result = TowersOfHanoi.Calculate(first);
            Assert.That(first.Count, Is.EqualTo(0));
            Assert.That(result.Count, Is.EqualTo(expectedResult.Count));

            while (firstCopy.TryPop(out var item))
            {
                Assert.That(item, Is.EqualTo(expectedResult.Pop()));
            }
        }

        private static IEnumerable<TestCaseData> GetTestData()
        {
            yield return new TestCaseData(null, null);

            yield return new TestCaseData(new Stack<int>(), new Stack<int>());

            var stack1 = new Stack<int>();
            stack1.Push(1);
            var stack2 = new Stack<int>(stack1);
            yield return new TestCaseData(stack1, stack2);

            var stack3 = new Stack<int>();
            stack3.Push(6);
            stack3.Push(5);
            stack3.Push(4);
            stack3.Push(3);
            stack3.Push(2);
            stack3.Push(1);
            var stack4 = new Stack<int>(stack3);
            yield return new TestCaseData(stack3, stack4);

        }
    }
}