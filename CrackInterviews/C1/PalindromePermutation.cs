using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;

namespace C1
{
    [TestFixture]
    class PalindromePermutation
    {       
        static bool Calculate1(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;

            var buffer = new int[128];

            foreach (char i in input)
            {
                buffer[(int) i]++;
            }

            var oddCount = 0;
            foreach (int b in buffer)
            {
                if (b % 2 != 0)
                {
                    oddCount++;
                }
            }

            return oddCount < 1;
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("qweqwewqeqwe")]
        [TestCase("asdfghjkl;''as;ldkfgjh")]
        public void ParlindromePermutationSuccessfulTest(string input)
        {
            Assert.True(PalindromePermutation.Calculate1(input));
        }

        [TestCase("qazwsxedcqazwsxed")]
        [TestCase("1=2-304958671=2-0394857")]
        public void ParlindromePermutationFailedTest(string input)
        {
            Assert.False(PalindromePermutation.Calculate1(input));
        }
    }
}