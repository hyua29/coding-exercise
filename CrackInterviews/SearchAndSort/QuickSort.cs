using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

using SearchAndSort;

namespace Searching
{
    public class QuickSort
    {
        public static int[] Calculate1(int[] arrayToSort)
        {
            if (arrayToSort == null || arrayToSort.Length == 0)
                return arrayToSort;

            Sort(0, arrayToSort.Length - 1, arrayToSort);

            return arrayToSort;
        }

        private static void Sort(int start, int end, int[] array)
        {
            if (start < end)
            {
                var pivotIndex = QuickSort.Partition(start, end, array);
                Sort(start, pivotIndex - 1, array);
                Sort(pivotIndex + 1, end, array);
            }
        }

        private static int Partition(int start, int end, int[] array)
        {
            int pointer = start;
            for (int i = start; i < end; i++)
            {
                if (array[i] < array[end])
                {
                    array.Swap(i, pointer);
                    pointer++;
                }
            }

            array.Swap(end, pointer);
            return pointer;
        }

        [TestCaseSource(nameof(GetTestData))]
        public void QuickSortTest(int[] arrayToSort)
        {
            var expectedResult = arrayToSort.OrderBy( x => x).ToArray();
            var result = QuickSort.Calculate1(arrayToSort);
            Assert.That(result.Length, Is.EqualTo(expectedResult.Length));
            for (int i = 0; i < arrayToSort.Length; i++)
            {
                Assert.That(result[i], Is.EqualTo(expectedResult[i]));
            }
        }

        private static IEnumerable<TestCaseData> GetTestData()
        {
            yield return new TestCaseData(new[] { 1, 2, 4 });
            yield return new TestCaseData(new[] { 4, 1, 2 });
            yield return new TestCaseData(new[] { 4, 2, 3, 1 });
            yield return new TestCaseData(new int[0]);
            yield return new TestCaseData(new[] { 1, 1, 1 });
            yield return new TestCaseData(new[] { 2, 1, 2 });
            yield return new TestCaseData(new[] { 2, 1, 2, 5, 6, 2, 10, 31, 1, 234, 1, 74, 65});
        }
    }
}