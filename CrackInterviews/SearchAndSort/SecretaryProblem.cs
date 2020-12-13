using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Searching
{
    public class SecretaryProblem
    {
        public static double Calculate(int choiceCount)
        {
            if (choiceCount <= 0) throw new ArgumentException($"{nameof(choiceCount)} Must be greater than 0");

            var permutations = GetPermutations(choiceCount);
            int optimumSampleSize = -1;
            double optimumProbability = double.MinValue;
            for (int sampleSize = 0; sampleSize < choiceCount; sampleSize++)
            {
                Console.WriteLine($"Current sample size is: {sampleSize}");
                // var sampleMaxList = new int[permutations.Count];
                var picks = new int[permutations.Count];
                for (int i = 0; i < permutations.Count; i++)
                {
                    var sampleMax = int.MinValue;
                    if (sampleSize > 0)
                    {
                        for (int j = 0; j < sampleSize; j++)
                        {
                            if (sampleMax < permutations[i][j]) sampleMax = permutations[i][j];
                        }
                    }

                    for (int j = sampleSize; j < choiceCount; j++)
                    {
                        // Pick the first one that is greater than sample max
                        if (permutations[i][j] > sampleMax)
                        {
                            picks[i] = permutations[i][j];
                            break;
                        }
                    }
                }

                // for (int i = 0; i < picks.Length; i++)
                // {
                //     Console.WriteLine($"iteration {i} has picked {picks[i]}");
                // }

                double currentOptimumProbability = (double) picks.Count(x => x == choiceCount) / picks.Length;
                Console.WriteLine($"{picks.Count(x => x == choiceCount)} / {picks.Length}");
                Console.WriteLine($"Probability of the current optimum pick is {currentOptimumProbability:F3}");
                if (currentOptimumProbability > optimumProbability)
                {
                    optimumProbability = currentOptimumProbability;
                    optimumSampleSize = sampleSize;
                }
            }

            Console.WriteLine("====");
            Console.WriteLine($"Final optimum pick is {optimumProbability} when sample size is {optimumSampleSize}");
            return optimumProbability;
        }

        public static IList<IList<int>> GetPermutations(int choiceCount)
        {
            if (choiceCount < 0) throw new ArgumentException($"{nameof(choiceCount)} cannot of smaller than 0");

            var permutations = new List<IList<int>>();
            if (choiceCount == 0) return permutations;

            var temp = new HashSet<int>();

            Recursive(permutations, temp, choiceCount);
            return permutations;
        }

        private static void Recursive(IList<IList<int>> result, HashSet<int> temp, int choiceCount)
        {
            if (temp.Count == choiceCount) result.Add(temp.Select(x => x).ToList());

            for (int i = 1; i < choiceCount + 1; i++)
            {
                if (!temp.Contains(i))
                {
                    temp.Add(i);
                    Recursive(result, temp, choiceCount);
                    temp.Remove(i);
                }
            }
        }
    }

    [TestFixture]
    public class SecretaryProblemTests
    {
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        public void GetPermutationsTests(int choiceCount)
        {
            if (choiceCount < 0)
            {
                Assert.Throws<ArgumentException>(() => SecretaryProblem.GetPermutations(choiceCount));
            }
            else if (choiceCount > 0)
            {
                var results = SecretaryProblem.GetPermutations(choiceCount);
                var expectedLength = 1;
                for (int i = 1; i < choiceCount + 1; i++)
                {
                    expectedLength *= i;
                }

                Assert.That(results.Count, Is.EqualTo(expectedLength));
            }
            else
            {
                var results = SecretaryProblem.GetPermutations(choiceCount);
                Assert.That(results.Count, Is.EqualTo(0));
            }
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(3)]
        public void CalculateTests(int choiceCount)
        {
            if (choiceCount <= 0)
            {
                Assert.Throws<ArgumentException>(() => SecretaryProblem.Calculate(choiceCount));
                return;
            }

            var result = SecretaryProblem.Calculate(choiceCount);
            Console.WriteLine(result);
        }

        [Test]
        public void CalculateTests_SampleSizeThree()
        {
            var result = SecretaryProblem.Calculate(3);
            Assert.That(result, Is.EqualTo(0.5).Within(0.005));
        }

        [Test]
        public void CalculateTests_SampleSizeFour()
        {
            var result = SecretaryProblem.Calculate(4);
            Assert.That(result, Is.EqualTo(0.458).Within(0.005));
        }
    }
}