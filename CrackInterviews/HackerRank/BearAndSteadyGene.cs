using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace HackerRank
{
    public class BearAndSteadyGene
    {
        public static int Calculate(string gene)
        {
            if (gene == null) return 0;

            var dict = new Dictionary<char, int>()
            {
                {'A', 0},
                {'T', 0},
                {'C', 0},
                {'G', 0}
            };

            foreach (var c in gene)
            {
                dict[c] = dict[c] + 1;
            }

            int factor = gene.Length / 4;
            if (IsBalanced(factor, dict)) return 0;

            var minChange = int.MaxValue;
            var low = 0;
            var high = 0;
            while (high < gene.Length)
            {
                if (!IsBalanced(factor, dict))
                {
                    dict[gene[high]] = dict[gene[high]] - 1;
                    high++;
                }
                else
                {
                    minChange = Math.Min(minChange, high - low);
                    dict[gene[low]] = dict[gene[low]] + 1;
                    low++;
                }
            }

            return minChange;
        }

        public static bool IsBalanced(int factor, IDictionary<char, int> dict)
        {
            return dict['A'] <= factor && dict['T'] <= factor && dict['C'] <= factor && dict['G'] <= factor;
        }
    }

    [TestFixture]
    public class BearAndSteadyGeneTests
    {
        [TestCaseSource(nameof(GetTestData))]
        public void BearAndSteadyGene_Tests(string gene, int expectedResult)
        {
            Assert.That(BearAndSteadyGene.Calculate(gene), Is.EqualTo(expectedResult));
        }

        private static IEnumerable<TestCaseData> GetTestData()
        {
            yield return new TestCaseData("GAAATAAA", 5);
            yield return new TestCaseData("TGATGCCGTCCCCTCAACTTGAGTGCTCCTAATGCGTTGC", 5);
        }
    }

}