namespace C1;

using System.Collections.Generic;
using NUnit.Framework;

[TestFixture]
internal class ZeroMatrix
{
    private static int[,] Calculate1(int[,] input)
    {
        if (input == null)
            return null;

        var isColumnZero = new bool[input.GetLength(0)];
        var isRowZero = new bool[input.GetLength(1)];

        for (var i = 0; i < input.GetLength(0); i++)
        for (var j = 0; j < input.GetLength(1); j++)
            if (input[i, j] == 0)
            {
                isColumnZero[i] = true;
                isRowZero[j] = true;
            }

        for (var i = 0; i < isRowZero.Length; i++)
        for (var j = 0; j < isColumnZero.Length; j++)
            if (isRowZero[i])
                input[j, i] = 0;

        for (var i = 0; i < isColumnZero.Length; i++)
        for (var j = 0; j < isRowZero.Length; j++)
            if (isColumnZero[i])
                input[i, j] = 0;

        return input;
    }

    [TestCaseSource(nameof(GetTestData))]
    public int[,] ZeroMatrixTest(int[,] input)
    {
        var result = Calculate1(input);
        return result;
    }

    private static IEnumerable<TestCaseData> GetTestData()
    {
        yield return new TestCaseData(
            new[,]
            {
                {1, 2},
                {0, 1},
                {3, 1}
            }).Returns(
            new[,]
            {
                {0, 2},
                {0, 0},
                {0, 1}
            });

        yield return new TestCaseData(
            new[,]
            {
                {1, 0},
                {0, 1},
                {3, 1}
            }).Returns(
            new[,]
            {
                {0, 0},
                {0, 0},
                {0, 0}
            });

        yield return new TestCaseData(
            new[,]
            {
                {1, 0}
            }).Returns(
            new[,]
            {
                {0, 0}
            });

        yield return new TestCaseData(
            new[,]
            {
                {1},
                {0},
                {3}
            }).Returns(
            new[,]
            {
                {0},
                {0},
                {0}
            });
        yield return new TestCaseData(
            new[,]
            {
                {1}
            }).Returns(
            new[,]
            {
                {1}
            });
        yield return new TestCaseData(
            new[,]
            {
                {0}
            }).Returns(
            new[,]
            {
                {0}
            });
        yield return new TestCaseData(
            new[,]
            {
                {0, 1},
                {1, 1},
                {3, 1}
            }).Returns(
            new[,]
            {
                {0, 0},
                {0, 1},
                {0, 1}
            });
    }
}