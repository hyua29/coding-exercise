using System.Collections.Generic;
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
            var pivotIndex = QuickSort.Partition(start, end, array);
            if (pivotIndex >= 0)
            {
                Sort(start, pivotIndex - 1, array);
                Sort(pivotIndex + 1, end, array);
            }
        }

        private static int Partition(int start, int end, int[] array)
        {
            if (start >= end)
                return -1;

            int pivotValue = array[end];
            int pointer = start;
            for (int i = start; i <= end; i++)
            {
                if (array[i] < pivotValue)
                {
                    array.Swap(i, pointer);
                    pointer++;
                }
            }

            array[pointer] = pivotValue;
            return pointer;
        }

        [TestCaseSource(nameof(GetTestData))]
        public void Test1(int[] arrayToSort, int[] sortedArray)
        {
            var result = QuickSort.Calculate1(sortedArray);
            Assert.That(result.Length, Is.EqualTo(sortedArray.Length));
            for (int i = 0; i < arrayToSort.Length; i++)
            {
                Assert.That(result[i], Is.EqualTo(sortedArray[i]));
            }
        }

        private static IEnumerable<TestCaseData> GetTestData()
        {
            yield return new TestCaseData(new[] { 1, 2, 4 }, new[] { 1, 2, 4 });
            yield return new TestCaseData(new[] { 4, 2, 3, 1 }, new[] { 1, 2, 3, 4 });
            yield return new TestCaseData(new int[0], new int[0]);
            yield return new TestCaseData(new[] { 1, 1, 1 }, new[] { 1, 1, 1 });
            yield return new TestCaseData(new[] { 2, 1, 2 }, new[] { 2, 1, 2 });
        }
    }
}