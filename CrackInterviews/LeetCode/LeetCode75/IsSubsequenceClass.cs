namespace LeetCode.LeetCode75;

public class IsSubsequenceClass
{
    public bool IsSubsequence(string s, string t)
    {
        var pointer = 0;
        var matches = 0;
        foreach (var c in s)
        {
            while (pointer < t.Length)
            {
                if (t[pointer] == c)
                {
                    matches++;
                    pointer++;
                    break;
                }
                else
                {
                    pointer++;
                }
            }
        }

        return matches == s.Length;
    }
}