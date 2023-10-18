namespace SearchAndSort;

using System;
using System.Linq;
using NUnit.Framework;

public class MergeSort
{
    public static T[] Sort<T>(T[] input) where T : IComparable
    {
        Sort(input, 0, input.Length - 1);

        return input;
    }

    private static void Sort<T>(T[] input, int left, int right) where T : IComparable
    {
        if (left < right)
        {
            var middle = left + (right - left) / 2;

            Sort(input, left, middle);
            Sort(input, middle + 1, right);

            Merge(input, left, middle, right);
        }
    }

    private static void Merge<T>(T[] input, int left, int middle, int right) where T : IComparable
    {
        var leftArray = new T[middle - left + 1];
        var rightArray = new T[right - middle];

        for (var i = 0; i < leftArray.Length; i++)
        {
            leftArray[i] = input[left + i];
        }

        for (var j = 0; j < rightArray.Length; j++)
        {
            rightArray[j] = input[middle + 1 + j];
        }

        var current = left;
        var leftPointer = 0;
        var rightPointer = 0;

        while (leftPointer < leftArray.Length && rightPointer < rightArray.Length)
        {
            if (leftArray[leftPointer].CompareTo(rightArray[rightPointer]) <= 0)
            {
                input[current++] = leftArray[leftPointer++];
            }
            else
            {
                input[current++] = rightArray[rightPointer++];
            }
        }

        while (leftPointer < leftArray.Length)
        {
            input[current++] = leftArray[leftPointer++];
        }

        while (rightPointer < rightArray.Length)
        {
            input[current++] = rightArray[rightPointer++];
        }
    }
}

[TestFixture]
public class MergeSortTests
{
    [Test]
    public void Test1()
    {
        var array = new int[] {2, 3, 4, 1, 5, 6};
        var expectedResult = array.OrderBy(a => a);

        MergeSort.Sort(array);

        foreach (var a in array)
        {
            Console.WriteLine(a);
        }

        Console.WriteLine("====");
        Assert.True(array.SequenceEqual(expectedResult));
    }

    [Test]
    public void Test2()
    {
        var array = new int[] {2, 3, 4, 1, 10, 3120, 1, 11, 5, 6};
        var expectedResult = array.OrderBy(a => a);

        MergeSort.Sort(array);

        Assert.True(array.SequenceEqual(expectedResult));
    }
}