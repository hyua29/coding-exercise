using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace C8
{
    public class PermutationsWithDupes
    {
        public static IList<IList<string>> Calculate(string[] array)
        {
            if (array == null) return null;

            var result = new List<IList<string>>();

            if (array.Length == 0) return result;

            var tempResult = new List<string>();
            var dict = BuildDictionary(array);
            CalculateAux(dict, array.Length, result, tempResult);
            return result;
        }

        private static Dictionary<string, int> BuildDictionary(string[] array)
        {
            var dict = new Dictionary<string, int>();
            foreach (var s in array)
            {
                if (dict.ContainsKey(s)) dict[s]++;
                else dict[s] = 1;
            }

            return dict;
        }

        private static void CalculateAux(Dictionary<string, int> dict, int remaining, IList<IList<string>> result, IList<string> tempResult)
        {
            if (remaining == 0)
            {
                result.Add(new List<string>(tempResult));
                return;
            }

            var keys = dict.Keys.ToArray();
            foreach (var key in keys)
            {
                int count = dict[key];
                if (count > 0)
                {
                    dict[key] = count - 1;
                    tempResult.Add(key);
                    CalculateAux(dict, remaining - 1, result, tempResult);
                    tempResult.RemoveAt(tempResult.Count - 1);
                    dict[key] = count;
                }
            }
        }
    }

    public class PermutationsWithDupesTests
    {
        [TestCaseSource(nameof(GetTestData))]
        public void CalculateTests(string[] array, IList<IList<string>> expectedResult)
        {
            if (array == null)
            {
                Assert.That(PermutationsWithDupes.Calculate(array), Is.EqualTo(expectedResult));
                return;
            };

            var result = PermutationsWithDupes.Calculate(array);

            Assert.That(result.Count, Is.EqualTo(expectedResult.Count));

            for (int i = 0; i < result.Count; i++)
            {
                Assert.That(result[i].Count, Is.EqualTo(expectedResult[i].Count));
                for (int j = 0; j < result[i].Count; j++)
                {
                    Console.WriteLine(result[i][j]);
                    Assert.That(result[i][j], Is.EqualTo(expectedResult[i][j]));
                }
            }
        }

        private static IEnumerable<TestCaseData> GetTestData()
        {
            yield return new TestCaseData(null, null);
            yield return new TestCaseData(new string[0], new List<IList<string>>());
            yield return new TestCaseData(new string[] { "1" }, new List<IList<string>>() { new List<string> { "1" } });
            yield return new TestCaseData(new string[] { "1", "2" }, new List<IList<string>>() { new List<string> { "1", "2" }, new List<string> { "2", "1" } });
            yield return new TestCaseData(new string[] { "1", "2", "3" }, new List<IList<string>>()
            {
                new List<string> { "1", "2", "3" },
                new List<string> { "1", "3", "2" },
                new List<string> { "2", "1", "3" },
                new List<string> { "2", "3", "1" },
                new List<string> { "3", "1", "2" },
                new List<string> { "3", "2", "1" },
            });
            yield return new TestCaseData(new string[] { "2", "2" }, new List<IList<string>>() { new List<string> { "2", "2" } });
            yield return new TestCaseData(new string[] { "1", "2", "2" }, new List<IList<string>>() { new List<string> { "1", "2", "2" }, new List<string> { "2", "1", "2" }, new List<string> { "2", "2", "1" } });
        }
    }
}