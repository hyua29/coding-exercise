using NUnit.Framework;

namespace C8
{
    public class TripleSteps
    {
        public static int Calculate(int totalSteps)
        {
            var memo = new int[totalSteps + 1];
            memo[0] = 1;

            for (int i = 1; i <= totalSteps; i++)
            {
                var possibleSteps = 0;
                for (int j = 1; j <= 3; j++)
                {
                    if (i - j >= 0)
                    {
                        possibleSteps += memo[i - j];
                    }
                }

                memo[i] = possibleSteps;
            }

            return memo[totalSteps];
        }
    }

    [TestFixture]
    public class TripleStepsTests
    {
        [TestCase(0, 1)]
        [TestCase(3, 4)]
        [TestCase(4, 7)]
        [TestCase(5, 13)]
        [TestCase(6, 24)]
        public void CalculateTest(int totalSteps, int expectedResult)
        {
            Assert.That(TripleSteps.Calculate(totalSteps), Is.EqualTo(expectedResult));
        }
    }
}