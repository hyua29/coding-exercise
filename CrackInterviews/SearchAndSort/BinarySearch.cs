namespace Searching;

using System.Collections.Generic;
using NUnit.Framework;

public class BinarySearch
{
    public static bool Calculate1(int[] sortedArray, int value)
    {
        if (sortedArray == null || sortedArray.Length == 0)
            return false;

        var left = 0;
        var right = sortedArray.Length - 1;

        while (left <= right)
        {
            var mid = (left + right) / 2;
            if (sortedArray[mid] == value)
                return true;
            if (sortedArray[mid] < value)
                left = mid + 1;
            else
                right = mid - 1;
        }

        return false;
    }

    [TestCaseSource(nameof(GetTestData))]
    public void Test1(int[] sortedArray, int value, bool expectedFound)
    {
        Assert.That(Calculate1(sortedArray, value), Is.EqualTo(expectedFound));
    }

    private static IEnumerable<TestCaseData> GetTestData()
    {
        yield return new TestCaseData(new[] {1, 2, 4}, 2, true);
        yield return new TestCaseData(new[] {1, 2, 2, 4}, 2, true);
        yield return new TestCaseData(new[] {1, 2, 3}, 3, true);
        yield return new TestCaseData(new[] {1, 2, 4}, 1, true);
        yield return new TestCaseData(new[] {1, 2, 4}, 3, false);
        yield return new TestCaseData(new[] {1, 2, 5, 8}, 1, true);
        yield return new TestCaseData(new[] {1, 2, 5, 8}, 8, true);
        yield return new TestCaseData(new[] {1, 2, 5, 8}, 6, false);
        yield return new TestCaseData(new[] {1, 2, 5, 8}, 3, false);
    }
}