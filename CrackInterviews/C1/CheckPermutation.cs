using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;

namespace C1
{
    [TestFixture]
    class CheckPermutation
    {
        static bool Calculate1(string input1, string input2)
        {
            if (input1 == null && input2 == null)
                return true;
            
            if (input1 == null)
                return false;
            
            if (input2 == null)
                return false;
            
            if (input1.Length != input2.Length)
                return false;
            
            var buffer = new int[128];
            
            foreach (char i in input1)
            {
                buffer[(int)i]++;
            }
            
            foreach (char i in input2)
            {
                buffer[(int)i]--;
                if (buffer[(int)i] < 0)
                    return false;
            }
            
            return true;
        }

        [TestCase("asdfghjkl;'", "'as;dlfkgjh")]
        [TestCase("qazwsxedc!@#", "!@#eqwdasczx")]
        [TestCase("", "")]
        [TestCase(null, null)]
        public void CheckPermutationSuccessfulTest(string input1, string input2)
        {
            Assert.True(CheckPermutation.Calculate1(input1, input2));
        }

        [TestCase("asdfghjkl;'", "'as;dlsfkgjh")]
        [TestCase("qazwasxedc!@#", "!@#eqwdasczx")]
        [TestCase("xzc", null)]
        [TestCase(null, "as")]
        public void CheckPermutationFailedTest(string input1, string input2)
        {
            Assert.False(CheckPermutation.Calculate1(input1, input2));
        }
    }
}
