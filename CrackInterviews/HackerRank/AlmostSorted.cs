using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace HackerRank
{
    public class AlmostSorted
    {
        public static void Calculate(int[] arr)
        {
            if (arr == null) Console.WriteLine("no");

            var sortedArr = arr.OrderBy(x => x).ToArray();

            var outOfOrderCount = 0;
            var outOfOrderStart = -1;
            var outOfOrderEnd = -1;
            var isContinuous = true;
            for (var i = 0; i < arr.Length; i++)
                if (arr[i] != sortedArr[i])
                {
                    isContinuous = outOfOrderEnd == -1 || outOfOrderEnd == i - 1;
                    if (outOfOrderStart == -1) outOfOrderStart = i;
                    outOfOrderEnd = i;
                    outOfOrderCount++;
                }

            if (outOfOrderCount == 0)
            {
                Console.WriteLine("yes");
            }
            else if (outOfOrderCount == 2)
            {
                Console.WriteLine("yes");
                Console.WriteLine($"swap {outOfOrderStart + 1} {outOfOrderEnd + 1}");
            }
            else if (isContinuous)
            {
                var canBeSorted = true;
                var offset = 0;
                for (var i = outOfOrderStart; i <= outOfOrderEnd; i++)
                {
                    var index = outOfOrderEnd - offset;
                    if (arr[i] != sortedArr[index]) canBeSorted = false;
                    offset++;
                }

                if (canBeSorted)
                {
                    Console.WriteLine("yes");
                    Console.WriteLine($"reverse {outOfOrderStart + 1} {outOfOrderEnd + 1}");
                }
                else
                {
                    Console.WriteLine("no");
                }
            }
            else
            {
                Console.WriteLine("no");
            }
        }
    }

    [TestFixture]
    public class AlmostSortedTests
    {
        [TestCaseSource(nameof(GetTestCaseData))]
        public void AlmostSorted_Tests(int[] array)
        {
            AlmostSorted.Calculate(array);
        }

        public static IEnumerable<TestCaseData> GetTestCaseData()
        {
            yield return new TestCaseData(new[] {1, 5, 4, 3, 2, 6});
        }
    }
}