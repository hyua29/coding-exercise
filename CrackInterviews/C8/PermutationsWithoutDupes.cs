namespace C8
{
    using System.Collections.Generic;
    using System;
    using System.Diagnostics;
    using System.Linq;
    using NUnit.Framework;

    /// <summary>
    /// Checkout this link for a general solution - https://leetcode.com/problems/permutations/solutions/18239/a-general-approach-to-backtracking-questions-in-java-subsets-permutations-combination-sum-palindrome-partioning/
    /// </summary>
    public class PermutationsWithoutDupes
    {
        public static IList<IList<char>> Calculate(char[] array)
        {
            Debug.Assert(array != null);

            if (array.Length == 0) return new List<IList<char>>();

            var results = new List<IList<char>>();

            Aux(array, results, new HashSet<char>());
            return results;
        }

        private static void Aux(char[] array, IList<IList<char>> results, HashSet<char> buffer)
        {
            if (buffer.Count == array.Length)
            {
                results.Add(buffer.ToList());
            }

            foreach (var c in array)
            {
                if (!buffer.Contains(c))
                {
                    buffer.Add(c);
                    Aux(array, results, buffer);
                    buffer.Remove(c);
                }
            }
        }
    }

    public class PermutationsWithoutDupesTests
    {
        [TestCaseSource(nameof(GetTestData))]
        public void CalculateTests(char[] array, IList<IList<char>> expectedResult)
        {
            var result = PermutationsWithoutDupes.Calculate(array);

            Assert.That(result.Count, Is.EqualTo(expectedResult.Count));

            for (int i = 0; i < result.Count; i++)
            {
                Assert.That(result[i].Count, Is.EqualTo(expectedResult[i].Count));
                for (int j = 0; j < result[i].Count; j++)
                {
                    Assert.That(result[i][j], Is.EqualTo(expectedResult[i][j]));
                }
            }
        }

        private static IEnumerable<TestCaseData> GetTestData()
        {
            yield return new TestCaseData(Array.Empty<char>(), new List<IList<char>>());
            yield return new TestCaseData(new char[] {'1'}, new List<IList<char>>() {new List<char> {'1'}});
            yield return new TestCaseData(new char[] {'1', '2'},
                new List<IList<char>>() {new List<char> {'1', '2'}, new List<char> {'2', '1'}});
            yield return new TestCaseData(new char[] {'1', '2', '3'}, new List<IList<char>>()
            {
                new List<char> {'1', '2', '3'},
                new List<char> {'1', '3', '2'},
                new List<char> {'2', '1', '3'},
                new List<char> {'2', '3', '1'},
                new List<char> {'3', '1', '2'},
                new List<char> {'3', '2', '1'},
            });
        }
    }
}