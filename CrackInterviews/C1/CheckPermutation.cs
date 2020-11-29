using NUnit.Framework;

namespace C1
{
    [TestFixture]
    internal class CheckPermutation
    {
        private static bool Calculate1(string input1, string input2)
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

            foreach (var i in input1) buffer[i]++;

            foreach (var i in input2)
            {
                buffer[i]--;
                if (buffer[i] < 0)
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
            Assert.True(Calculate1(input1, input2));
        }

        [TestCase("asdfghjkl;'", "'as;dlsfkgjh")]
        [TestCase("qazwasxedc!@#", "!@#eqwdasczx")]
        [TestCase("xzc", null)]
        [TestCase(null, "as")]
        public void CheckPermutationFailedTest(string input1, string input2)
        {
            Assert.False(Calculate1(input1, input2));
        }
    }
}