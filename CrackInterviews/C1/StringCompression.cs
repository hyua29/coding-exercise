using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;
using System.Text;

namespace C1
{
    [TestFixture]
    class StringCompression
    {       
        static string Calculate1(string input)
        {
            if (string.IsNullOrEmpty(input))
                return "";

            if (input.Length == 1)
                return input;

            int pre = 0;
            int count = 1;
            var result = new StringBuilder();
            for (int curr=1;curr<input.Length;curr++)
            {
                if (input[pre] == input[curr])
                {
                    count++;
                }
                else
                {
                    result.Append(input[pre]);
                    result.Append(count);
                    pre = curr;
                    count = 1;
                }
            }

            result.Append(input[input.Length - 1]);
            result.Append(count);
           
            return result.Length < input.Length ? result.ToString() : input;
        }

        [TestCase(null, "")]
        [TestCase("", "")]
        [TestCase("aabbcc", "aabbcc")]
        [TestCase("aaabbcc", "a3b2c2")]
        [TestCase("aaaabbc", "a4b2c1")]
        public void StringCompressionTest(string input, string result)
        {
            Assert.That(StringCompression.Calculate1(input), Is.EqualTo(result));
        }
    }
}