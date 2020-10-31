using System.Collections.Generic;
using NUnit.Framework;

namespace Searching
{
    public class BinarySearch
    {
        public static bool Calculate1(int[] sortedArray, int value)
        {
            if (sortedArray == null || sortedArray.Length == 0)
                return false;

            int left = 0;
            int right = sortedArray.Length - 1;

            while (left <= right)
            {
                int mid = (left + right) / 2;
                if (sortedArray[mid] == value)
                    return true;
                else if (sortedArray[mid] < value)
                    left = mid + 1;
                else
                    right = mid - 1;
            }

            return false;
        }

        [TestCaseSource(nameof(GetTestData))]
        public void Test1(int[] sortedArray, int value, bool expectedFound)
        {
            Assert.That(BinarySearch.Calculate1(sortedArray, value), Is.EqualTo(expectedFound));
        }

        private static IEnumerable<TestCaseData> GetTestData()
        {
            yield return new TestCaseData(new [] {1, 2, 4}, 2, true);
            yield return new TestCaseData(new [] {1, 2, 2, 4}, 2, true);
            yield return new TestCaseData(new [] {1, 2, 3}, 3, true);
            yield return new TestCaseData(new [] {1, 2, 4}, 1, true);
            yield return new TestCaseData(new [] {1, 2, 4}, 3, false);
            yield return new TestCaseData(new [] {1, 2, 5, 8}, 1, true);
            yield return new TestCaseData(new [] {1, 2, 5, 8}, 8, true);
            yield return new TestCaseData(new [] {1, 2, 5, 8}, 6, false);
            yield return new TestCaseData(new [] {1, 2, 5, 8}, 3, false);
        }
    }
}