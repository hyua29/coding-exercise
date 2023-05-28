namespace C1;

using System;
using System.Collections.Generic;
using NUnit.Framework;

[TestFixture]
internal class RotateMatrix
{
    private static int[,] Calculate1(int[,] input)
    {
        if (input == null)
            return null;


        for (var y = 0; y < input.GetLength(1) / 2; y++)
        for (var x = 0; x < input.GetLength(0); x++)
        {
            var temp = input[y, x];
            input[y, x] = input[input.GetLength(1) - 1 - y, x];
            input[input.GetLength(1) - 1 - y, x] = temp;
        }

        for (var x = 0; x < input.GetLength(0) / 2; x++)
        for (var y = 0; y < input.GetLength(1); y++)
        {
            var temp = input[y, x];
            input[y, x] = input[y, input.GetLength(0) - 1 - x];
            input[y, input.GetLength(0) - 1 - x] = temp;
        }

        return input;
    }

    [TestCaseSource(nameof(GetTestData))]
    public void ParlindromePermutationSuccessfulTest(int[,] input)
    {
        var results = Calculate1(input);
        for (var y = 0; y < input.GetLength(1); y++)
        for (var x = 0; x < input.GetLength(0); x++)
            Console.WriteLine(results[y, x]);
    }

    private static IEnumerable<TestCaseData> GetTestData()
    {
        yield return new TestCaseData(new[,]
        {
            {1, 2, 3},
            {4, 5, 6},
            {7, 8, 9}
        });
    }
}