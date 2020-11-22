namespace HackerRank
{
    internal class MaximumPalindromes
    {
        internal static void Initialize(string s)
        {
            // This function is called once before all queries.

        }

        internal static int AnswerQuery(int l, int r)
        {
            // Return the answer for this query modulo 1000000007.
            return 0;
        }

        private static bool IsPerlinedome(string input)
        {
            int left = 0;
            int right = input.Length - 1;
            while (left <= right)
            {
                if (input[left] != input[right]) return false;
                left++;
                right++;
            }
            return true;
        }
    }
}