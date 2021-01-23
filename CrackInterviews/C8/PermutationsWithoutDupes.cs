using System.Collections.Generic;
using System;
using NUnit.Framework;

namespace C8
{
    public class PermutationsWithoutDupes
    {
        public static IList<IList<string>> Calculate(string[] array)
        {
            if (array == null) return null;

            var result = new List<IList<string>>();

            if (array.Length == 0) return result;

            var buffer = new List<string>();
            CalculateAux(result, buffer, array);
            return result;
        }

        private static void CalculateAux(IList<IList<string>> result, List<string> buffer, string[] array)
        {
            if (buffer.Count == array.Length)
            {
                result.Add(new List<string>(buffer));
                return;
            }

            for (int i = 0; i < array.Length; i++)
            {
                if (!buffer.Contains(array[i]))
                {
                    buffer.Add(array[i]);
                    CalculateAux(result, buffer, array);
                    buffer.Remove(array[i]);
                }
            }
        }
    }

    public class PermutationsWithoutDupesTests
    {
        [TestCaseSource(nameof(GetTestData))]
        public void CalculateTests(string[] array, IList<IList<string>> expectedResult)
        {
            if (array == null)
            {
                Assert.That(PermutationsWithoutDupes.Calculate(array), Is.EqualTo(expectedResult));
                return;
            };

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
        }
    }
}