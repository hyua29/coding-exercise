using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace C8
{
    public class PowerSet
    {
        public static IList<HashSet<string>> Calculate(string[] set)
        {
            if (set == null) return null;

            var result = new List<HashSet<string>>();
            
            if (set.Length == 0) return result;
            var temp = new HashSet<string>();
            
            PowerSet.CalculateAux(result, temp, set, 0);

            return result;
        }

        private static void CalculateAux(IList<HashSet<string>> result, HashSet<string> temp, string[] set, int index)
        {
            result.Add(temp.Select(x => x).ToHashSet());
            for (int i = index; i < set.Length; i++)
            {
                temp.Add(set[i]);
                PowerSet.CalculateAux(result, temp, set, i + 1);
                temp.Remove(set[i]);
            }
        }
    }

    [TestFixture]
    public class PowerSetTests
    {
        [TestCaseSource(nameof(GetTestData))]
        public void CalculateTest(string[] set, IList<HashSet<string>> expectedResult)
        {
            var result = PowerSet.Calculate(set);

            if (set == null)
            {
                Assert.That(result, Is.EqualTo(expectedResult));
                return;
            }

            Assert.That(result.Count, Is.EqualTo(expectedResult.Count));
            for (int i = 0; i < result.Count; i++)
            {
                Assert.That(result[i].Count, Is.EqualTo(expectedResult[i].Count));
                foreach (var item in result[i])
                {
                    Assert.That(expectedResult[i].Contains(item), Is.EqualTo(true));
                }
            }
        }

        public static IEnumerable<TestCaseData> GetTestData()
        {
            yield return new TestCaseData(null, null);

            yield return new TestCaseData(new string[0], new List<HashSet<string>>());
            
            yield return new TestCaseData(
                new[] { "1", "2", "3" },
                new List<HashSet<string>>
                {
                    new HashSet<string> {},
                    new HashSet<string> { "1" },
                    new HashSet<string> { "1", "2" },
                    new HashSet<string> { "1", "2", "3" },
                    new HashSet<string> { "1", "3" },
                    new HashSet<string> { "2" },
                    new HashSet<string> { "2", "3" },
                    new HashSet<string> { "3" }
                });
        }
    }
}