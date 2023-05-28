namespace HackerRank;

using System;
using System.Collections.Generic;

public class QueueUsingTwoStacks
{
    public static void Calculate(string[] args)
    {
        var aStack = new Stack<int>();
        var bStack = new Stack<int>();

        // Number of queries
        var q = Convert.ToInt32(Console.ReadLine());

        for (var i = 0; i < q; i++)
        {
            var line_temp = Console.ReadLine().Split(' ');
            var line = Array.ConvertAll(line_temp, int.Parse);

            if (bStack.Count == 0)
                while (aStack.Count != 0)
                    bStack.Push(aStack.Pop());

            // Enqueue
            if (line.Length == 2)
            {
                if (bStack.Count == 0)
                    bStack.Push(line[1]);
                else
                    aStack.Push(line[1]);
            }
            else
            {
                // Dequeue
                if (line[0] == 2)
                    bStack.Pop();
                // Print
                else if (line[0] == 3)
                    Console.WriteLine(bStack.Peek());
            }
        }
    }
}