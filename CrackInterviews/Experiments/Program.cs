using System;
using System.Collections.Generic;

namespace Experiments
{
    class Program
    {
        static void Main(string[] args)
        {
            IDictionary<string, int> dict = new Dictionary<string, int>();
            dict["b"] = 10;
            Console.WriteLine(dict.ContainsKey("a"));
            Console.WriteLine(dict.ContainsKey("b"));

            Console.WriteLine(dict.Contains(new KeyValuePair<string, int>("b",10)));
        }
    }
}
