namespace HackerRank;

using System;
using System.Collections.Generic;

public class FormingMagicSquare
{
    public static int Calculate(int[][] s)
    {
        var possiblePermutations = new List<int[,]>
        {
            new[,] {{8, 1, 6}, {3, 5, 7}, {4, 9, 2}},
            new[,] {{6, 1, 8}, {7, 5, 3}, {2, 9, 4}},
            new[,] {{4, 9, 2}, {3, 5, 7}, {8, 1, 6}},
            new[,] {{2, 9, 4}, {7, 5, 3}, {6, 1, 8}},
            new[,] {{8, 3, 4}, {1, 5, 9}, {6, 7, 2}},
            new[,] {{4, 3, 8}, {9, 5, 1}, {2, 7, 6}},
            new[,] {{6, 7, 2}, {1, 5, 9}, {8, 3, 4}},
            new[,] {{2, 7, 6}, {9, 5, 1}, {4, 3, 8}}
        };

        var minCost = int.MaxValue;
        foreach (var permutation in possiblePermutations)
        {
            var cost = 0;
            for (var i = 0; i < 3; i++)
            for (var j = 0; j < 3; j++)
                cost += Math.Abs(permutation[i, j] - s[i][j]);

            minCost = Math.Min(cost, minCost);
        }

        return minCost;
    }
}