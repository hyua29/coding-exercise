namespace C8
{
    using System.Collections.Generic;
    using NUnit.Framework;

    public class Parens
    {
        public static IList<string> Calculate(int numOfParentheses)
        {
            var result = new List<string>();
            if (numOfParentheses < 0) return null;
            if (numOfParentheses == 0) return result;

            var tempResult = new char[numOfParentheses * 2];
            CalculateAux(result, tempResult, numOfParentheses, numOfParentheses, 0);
            return result;
        }

        private static void CalculateAux(IList<string> result, char[] tempResult, int numOfLeft, int numOfRight,
            int index)
        {
            if (numOfLeft < 0 || numOfLeft > numOfRight) return;
            if (numOfLeft == 0 && numOfRight == 0)
            {
                result.Add(new string(tempResult));
                return;
            }

            tempResult[index] = '(';
            CalculateAux(result, tempResult, numOfLeft - 1, numOfRight, index + 1);
            tempResult[index] = ')';
            CalculateAux(result, tempResult, numOfLeft, numOfRight - 1, index + 1);
        }
    }

    [TestFixture]
    public class ParensTests
    {
        [TestCaseSource(nameof(GetTestData))]
        public void Calculate(int numOfParentheses, IList<string> expectedResult)
        {
            var result = Parens.Calculate(numOfParentheses);
            if (numOfParentheses < 0)
            {
                Assert.Null(result);
            }
            else if (numOfParentheses == 0) Assert.That(result.Count, Is.EqualTo(0));
            else
            {
                Assert.That(result.Count, Is.EqualTo(expectedResult.Count));
                for (int i = 0; i < result.Count; i++)
                {
                    Assert.That(result[i], Is.EqualTo(expectedResult[i]));
                }
            }
        }

        private static IEnumerable<TestCaseData> GetTestData()
        {
            yield return new TestCaseData(null, null);
            yield return new TestCaseData(0, new List<string>());
            yield return new TestCaseData(1, new List<string>() {"()"});
            yield return new TestCaseData(2, new List<string>() {"(())", "()()"});
            yield return new TestCaseData(3, new List<string>() {"((()))", "(()())", "(())()", "()(())", "()()()"});
        }
    }
}