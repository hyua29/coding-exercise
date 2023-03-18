using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace C8;

// TODO: Fix me
public class RobotInGrid
{
    public enum Direction
    {
        Unknown,
        Right,
        Down
    }

    public static Direction[] Calculate(bool[,] input)
    {
        Debug.Assert(input != null);

        var steps = input.GetLength(0) + input.GetLength(1) - 2;
        if (steps <= 0)
        {
            return Array.Empty<Direction>();
        }

        var buffer = new Direction[steps];

        var toExplore = new Stack<(int Y, int X, Direction Direction)>();
        toExplore.Push((0, 0, Direction.Unknown));

        var foundSolution = false;
        while (toExplore.Count != 0)
        {
            var current = toExplore.Pop();
            buffer[current.Y + current.X] = current.Direction;

            if (current.Y == input.GetLength(1) && current.X == input.GetLength(0))
            {
                foundSolution = true;
                break;
            }

            // Go Right
            if (input[current.Y, current.X + 1])
            {
                toExplore.Push((current.Y, current.X + 1, Direction.Right));
            }

            // Go Down
            if (input[current.Y + 1, current.X])
            {
                toExplore.Push((current.Y + 1, current.X, Direction.Down));
            }
        }

        return foundSolution ? buffer : null;
    }
}