namespace C8
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using NUnit.Framework;

    public class PowerSet
    {
        public static IList<IList<char>> CalculateWithDp(char[] set)
        {
            Debug.Assert(set != null);

            var results = new List<IList<char>>()
            {
                new List<char>()
            };

            for (int i = 0; i < set.Length; i++)
            {
                var temp = results.ToList();
                foreach (var t in temp)
                {
                    var newSet = t.ToList();
                    newSet.Add(set[i]);
                    results.Add(newSet);
                }
            }

            return results;
        }

        public static IList<IList<char>> CalculateWithBackTrack(char[] set)
        {
            Debug.Assert(set != null);

            var result = new List<IList<char>>();
            // {
            //     new List<char>()
            // };

            var temp = new List<char>();

            PowerSet.CalculateAux(result, temp, set, 0);

            return result;
        }

        private static void CalculateAux(IList<IList<char>> result, IList<char> temp, char[] set, int index)
        {
            result.Add(temp.Select(x => x).ToList());
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
        [TestCaseSource(nameof(GetDpTestData))]
        public void CalculateWithDp_Test(char[] set, IList<IList<char>> expectedResult)
        {
            var result = PowerSet.CalculateWithDp(set);

            AssertEqual(set, expectedResult, result);
        }

        [TestCaseSource(nameof(GetBackTrackTestData))]
        public void CalculateWithBackTrack_Test(char[] set, IList<IList<char>> expectedResult)
        {
            var result = PowerSet.CalculateWithBackTrack(set);

            AssertEqual(set, expectedResult, result);
        }

        private static void AssertEqual(char[] set, IList<IList<char>> expectedResult, IList<IList<char>> result)
        {
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

        public static IEnumerable<TestCaseData> GetBackTrackTestData()
        {
            yield return new TestCaseData(Array.Empty<char>(), new List<IList<char>> {new List<char>()});

            yield return new TestCaseData(
                new[] {'1', '2', '3'},
                new List<IList<char>>
                {
                    new List<char> { },
                    new List<char> {'1'},
                    new List<char> {'1', '2'},
                    new List<char> {'1', '2', '3'},
                    new List<char> {'1', '3'},
                    new List<char> {'2'},
                    new List<char> {'2', '3'},
                    new List<char> {'3'}
                });
        }

        public static IEnumerable<TestCaseData> GetDpTestData()
        {
            yield return new TestCaseData(Array.Empty<char>(), new List<IList<char>> {new List<char>()});

            yield return new TestCaseData(
                new[] {'1', '2', '3'},
                new List<IList<char>>
                {
                    new List<char> { },
                    new List<char> {'1'},
                    new List<char> {'2'},
                    new List<char> {'1', '2'},
                    new List<char> {'3'},
                    new List<char> {'1', '3'},
                    new List<char> {'2', '3'},
                    new List<char> {'1', '2', '3'}
                });

            yield return new TestCaseData(
                new[] {'1', '2', '3', '4'},
                new List<IList<char>>
                {
                    new List<char> { },
                    new List<char> {'1'},
                    new List<char> {'2'},
                    new List<char> {'1', '2'},
                    new List<char> {'3'},
                    new List<char> {'1', '3'},
                    new List<char> {'2', '3'},
                    new List<char> {'1', '2', '3'},
                    new List<char> {'4'},
                    new List<char> {'1', '4'},
                    new List<char> {'2', '4'},
                    new List<char> {'1', '2', '4'},
                    new List<char> {'3', '4'},
                    new List<char> {'1', '3', '4'},
                    new List<char> {'2', '3', '4'},
                    new List<char> {'1', '2', '3', '4'}
                });
        }
    }
}