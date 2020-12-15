using System;
using System.Collections.Generic;

namespace HackerRank
{
    public class QueueUsingTwoStacks
    {
        public static void Calculate(String[] args)
        {
            Stack<int> aStack = new Stack<int>();
            Stack<int> bStack = new Stack<int>();

            // Number of queries
            int q = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < q; i++)
            {
                string[] line_temp = Console.ReadLine().Split(' ');
                int[] line = Array.ConvertAll(line_temp, Int32.Parse);

                if (bStack.Count == 0)
                {
                    while (aStack.Count != 0)
                        bStack.Push(aStack.Pop());
                }

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
}